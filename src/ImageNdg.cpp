#include <iostream>
#include <fstream>
#include <string>
#include <windows.h>
#include <cmath>
#include <vector>
#include <algorithm>

#include "ImageNdg.h"

#define MAGIC_NUMBER_BMP ('B'+('M'<<8)) // signature bitmap windows

// constructeurs et destructeur
CImageNdg::CImageNdg() {

	this->m_iHauteur  = 0;
	this->m_iLargeur  = 0;
	this->m_bBinaire  = false;
	this->m_sNom      = "vide";
	
	this->m_pucPixel  = NULL;
	this->m_pucPalette = NULL; 
}

CImageNdg::CImageNdg(int hauteur, int largeur, int valeur) {

	this->m_iHauteur = hauteur;
	this->m_iLargeur = largeur;
	this->m_bBinaire	= false; // Image Ndg par défaut, binaire après seuillage
	this->m_sNom      = "inconnu";

	this->m_pucPixel = new unsigned char[hauteur*largeur];
	this->m_pucPalette = new unsigned char[256*4];	
	choixPalette("grise"); // palette grise par défaut, choix utilisateur 
	if (valeur != -1) 
		for (int i=0;i<this->lireNbPixels();i++)
			this->m_pucPixel[i] = valeur;
}

CImageNdg::CImageNdg(const std::string& name) {

	BITMAPFILEHEADER header;
	BITMAPINFOHEADER infoHeader;
	
	std::ifstream f(name.c_str(),std::ios::in | std::ios::binary); 
		if (f.is_open()) {
			f.read((char*)&header,sizeof(BITMAPFILEHEADER));
			if (header.bfType != MAGIC_NUMBER_BMP) 
				throw std::string("ouverture format BMP impossible ..."); 
			else {
				f.read((char*)&infoHeader,sizeof(BITMAPINFOHEADER));
				if (infoHeader.biCompression > 0) 
					throw std::string("Format compresse non supporte...");
				else {
					if (infoHeader.biBitCount == 8) {
						this->m_iHauteur = infoHeader.biHeight;
						this->m_iLargeur = infoHeader.biWidth;
						this->m_bBinaire = false;
						this->m_sNom.assign(name.begin(),name.end()-4);
						this->m_pucPalette = new unsigned char[256*4];	
						this->m_pucPixel = new unsigned char[infoHeader.biHeight * infoHeader.biWidth];

						// gérer multiple de 32 bits via zéros éventuels ignorés
						int complement = (((this->m_iLargeur-1)/4) + 1)*4 - this->m_iLargeur;
						for (int indice=0;indice<4*256;indice++) 
							f.read((char*)&this->m_pucPalette[indice],sizeof(char));

						for (int i= this->m_iHauteur-1; i >= 0; i--) {
							for (int j=0; j<this->m_iLargeur; j++) 
								f.read((char*)&this->m_pucPixel[i*this->m_iLargeur+j],sizeof(char));

							char inutile;
							for (int k=0; k< complement; k++)
								f.read((char*)&inutile,sizeof(char));
						}
					}	
					else {
						// cas d'une image couleur
						this->m_iHauteur = infoHeader.biHeight;
						this->m_iLargeur = infoHeader.biWidth;
						this->m_bBinaire = false;
						this->m_sNom.assign(name.begin(),name.end()-4);
						this->m_pucPalette = new unsigned char[256*4];	
						this->choixPalette("grise"); // palette grise par défaut
						this->m_pucPixel = new unsigned char[infoHeader.biHeight * infoHeader.biWidth];

						// extraction plan luminance
						int complement = (((this->m_iLargeur*3-1)/4) + 1)*4 - this->m_iLargeur*3;
						for (int i= this->m_iHauteur-1; i >= 0; i--) {
							for (int j=0;j<this->m_iLargeur*3;j+=3) {
								unsigned char rouge,vert,bleu;
								f.read((char*)&rouge,sizeof(char));
								f.read((char*)&vert,sizeof(char)); 
								f.read((char*)&bleu,sizeof(char));
								this->m_pucPixel[i*this->m_iLargeur+j/3]=(unsigned char)(((int)rouge+(int)vert+(int)bleu)/3);
							}	

							char inutile;
							for (int k=0; k< complement; k++)
								f.read((char*)&inutile,sizeof(char));
						}
					}
				}
			}
			f.close();
		}
		else
			throw std::string("ERREUR : Image absente (ou pas ici en tout cas) !");
}

CImageNdg::CImageNdg(const CImageNdg& im) {

	this->m_iHauteur = im.lireHauteur();
	this->m_iLargeur = im.lireLargeur();
	this->m_bBinaire = im.lireBinaire(); 
	this->m_sNom     = im.lireNom();
	this->m_pucPixel = NULL; 
	this->m_pucPalette = NULL;

	if (im.m_pucPalette != NULL) {
		this->m_pucPalette = new unsigned char[256*4];
		memcpy(this->m_pucPalette,im.m_pucPalette,4*256);
	}
	if (im.m_pucPixel != NULL) {
		this->m_pucPixel = new unsigned char[im.lireHauteur() * im.lireLargeur()];
		memcpy(this->m_pucPixel,im.m_pucPixel,im.lireNbPixels());
	}
}

CImageNdg::~CImageNdg() {
	if (this->m_pucPixel) {
		delete[] this->m_pucPixel;
		this->m_pucPixel = NULL;
	}

	if (this->m_pucPalette) {
		delete[] this->m_pucPalette;
		this->m_pucPalette = NULL;
	}
}

void CImageNdg::sauvegarde(const std::string& fixe) {
	BITMAPFILEHEADER header;
	BITMAPINFOHEADER infoHeader;

	if (this->m_pucPixel) {
		std::string nomFichier = "../../../data/Res/";
		if (fixe.compare("") == 0)
			nomFichier += this->lireNom() + ".bmp"; // force sauvegarde dans répertoire Res (doit exister)
		else
			nomFichier += fixe;

		std::ofstream f(nomFichier.c_str(),std::ios::binary);
		if (f.is_open()) {

			int complement = (((this->m_iLargeur-1)/4) + 1)*4 - this->m_iLargeur;

			header.bfType = MAGIC_NUMBER_BMP;
			header.bfOffBits = sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER) + 4*256*sizeof(char); // palette
			header.bfSize = header.bfOffBits + (complement+this->lireNbPixels()*sizeof(char));
			header.bfReserved1 = 0;
			header.bfReserved2 = 0;
			f.write((char*)&header,sizeof(BITMAPFILEHEADER));
			
			infoHeader.biHeight = this->m_iHauteur;
			infoHeader.biWidth = this->m_iLargeur;
			infoHeader.biCompression = 0;
			infoHeader.biClrUsed = 0;
			infoHeader.biBitCount = 8;
			infoHeader.biSizeImage = 0; //pas de compression;
			infoHeader.biClrUsed = 256;
			infoHeader.biClrImportant = 256;
			infoHeader.biHeight = this->m_iHauteur;
			infoHeader.biWidth = this->m_iLargeur;
			infoHeader.biPlanes = 1;
			infoHeader.biSize = sizeof(infoHeader);
			infoHeader.biSizeImage = this->lireNbPixels();
			f.write((char*)&infoHeader,sizeof(BITMAPINFOHEADER));

			// on remplit la palette
			for (int indice=0;indice<4*256;indice ++) 
				f.write((char*)&this->m_pucPalette[indice],sizeof(char)); 

			for (int i= this->m_iHauteur-1; i >= 0; i--) {
				for (int j=0;j<m_iLargeur;j++)    
					f.write((char*)&this->m_pucPixel[i*m_iLargeur+j],sizeof(char));
					
				// gérer multiple de 32 bits
				char inutile;
				for (int k=0; k< complement; k++)
					f.write((char*)&inutile,sizeof(char)); 
				}
		f.close();
		}
		else
			throw std::string("Impossible de creer le fichier de sauvegarde !");
	}
	else
		throw std::string("Pas de donnee a sauvegarder !");
}

CImageNdg& CImageNdg::operator=(const CImageNdg& im) {

	if (&im == this)
		return *this;

	this->m_iHauteur = im.lireHauteur();
	this->m_iLargeur = im.lireLargeur();
	this->m_bBinaire = im.lireBinaire(); 
	this->m_sNom     = im.lireNom();

	if (this->m_pucPixel) 
		delete[] this->m_pucPixel;
	this->m_pucPixel = new unsigned char[this->m_iHauteur * this->m_iLargeur];

	if (this->m_pucPalette)
		delete[] this->m_pucPalette;
	this->m_pucPalette = new unsigned char[256*4];

	if (im.m_pucPalette != NULL)
		memcpy(this->m_pucPalette,im.m_pucPalette,4*256);
	if (im.m_pucPixel != NULL)
		memcpy(this->m_pucPixel,im.m_pucPixel,im.lireNbPixels());

return *this;
}

// fonctionnalités histogramme 

std::vector<unsigned long> CImageNdg::histogramme(bool enregistrementCSV, int pas) {

	std::vector<unsigned long> h;

	h.resize(256/pas,0);
	for (int i=0;i<this->lireNbPixels();i++) 
		h[this->operator()(i)/pas] += 1L;

	if (enregistrementCSV) {
	 std::string fichier = "../Res/" + this->lireNom() + ".csv";
		std::ofstream f (fichier.c_str());

		if (!f.is_open())
			std::cout << "Impossible d'ouvrir le fichier en ecriture !" << std::endl;
		else {
			for (int i=0;i<(int)h.size();i++)
				f << h[i] << std::endl;
		}
		f.close();
	}

	return h;
}

// signatures globales

MOMENTS CImageNdg::signatures(const std::vector<unsigned long>& h) {

	MOMENTS globales;
	
	// min
	int i=0;
	while ((i < (int)h.size()) && (h[i] == 0))
		i++;
	globales.min = i;
		
	// max
	i=h.size()-1;
	while ((i > 0) && (h[i] == 0))
		i--;
	globales.max = i;

	// mediane

	int moitPop = this->lireNbPixels()/2;

	i=globales.min;
	int somme = h[i];
	while (somme < moitPop) {
		i += 1;
		if (i < (int)h.size())
			somme += h[i];
	}
	globales.mediane = i;

	// moyenne et écart-type
	double moy=0,sigma=0;
	for (i=globales.min;i<=globales.max;i++) {
		moy += ((double)h[i])*i;
		sigma += ((double)h[i])*i*i;
	}
	moy /= (double)this->lireNbPixels();
	sigma = sqrt(sigma/(double)this->lireNbPixels() - (moy*moy));
	globales.moyenne = moy;
	globales.ecartType = sigma;

	return globales;
}

MOMENTS CImageNdg::signatures() {
	
	MOMENTS globales;
	std::vector<unsigned long> hist;
	hist=this->histogramme();

	globales = this->signatures(hist);
	return globales;
}

// opérations ensemblistes images binaires
CImageNdg CImageNdg::operation(const CImageNdg& im, const std::string& methode) {

	if ((&im == this) || !(this->lireBinaire() && im.lireBinaire())) {
		return *this;
	}

	CImageNdg out(this->lireHauteur(), this->lireLargeur());
	out.m_bBinaire = this->lireBinaire(); 
	out.m_sNom     = this->lireNom()+"Op";
	out.choixPalette("binaire"); // palette binaire par défaut pour img binaire

	if (methode.compare("et") == 0) {
		for (int i=0;i<this->lireNbPixels();i++)
			out(i) = this->operator()(i) && im(i);
	}
	else
	{
		if (methode.compare("ou") == 0) 
		{
			for (int i = 0; i < this->lireNbPixels(); i++)
				out(i) = this->operator()(i) || im(i);
		}
		else
		{
			if (methode.compare("-") == 0)
			{
				for (int i = 0; i < this->lireNbPixels(); i++)
				{
					if (this->operator()(i) - im(i) >= 0)
					{
						out(i) = this->operator()(i) - im(i);
					}
					else
					{
						out(i) = 0;
					}
					
				}
					
			}
		}
	}
		

return out;
}


// seuillage
CImageNdg CImageNdg::seuillage(const std::string& methode, int& seuilBas, int& seuilHaut) {
	
	if (!this->m_bBinaire) {
		CImageNdg out(this->lireHauteur(),this->lireLargeur());
		out.m_sNom     = this->lireNom()+"S";
		out.choixPalette("binaire"); // palette binaire par défaut
		out.m_bBinaire = true;

		// création lut pour optimisation calcul
		std::vector<int> lut;
		lut.resize(256);

		// recherche valeur seuil
		// cas "manuel" -> seuil reste celui passé en paramètre

		if (methode.compare("automatique") == 0) 
		{
			std::vector<unsigned long> hist = this->histogramme();
			std::vector<unsigned long> histC; // histogramme cumulé
			histC.resize(256,0);
			histC[0] = hist[0];
			for (int i=1;i<(int)hist.size();i++) 
				histC[i] = histC[i-1]+hist[i];

			MOMENTS globales = this->signatures(hist);
			int min = globales.min,
				max = globales.max;

			// f(s)
			std::vector<double> tab;
			tab.resize(256,0);
		
			double M1,M2;
		
			// initialisation
			M1 = (double)min;
			double nb=0;
			M2=0;
			for (int i=min+1;i<=max;i++) {
				M2 += (double)hist[i]*i;
				nb += (double)hist[i];
			}
			if (nb > 0)
				M2 /= nb;
			tab[min] = fabs(min - (M1 + M2)/2);
		
			// parcours
			for (int i=min+1;i<=max;i++) {
				M1 = ( (double)histC[i-1]*M1 + hist[i]*i ) / histC[i];
				M2 = ( (double)(histC[max]-histC[i-1])*M2 - hist[i]*i) / (histC[max]-histC[i]);
				tab[i] = fabs(i - (M1 + M2)/2);
			}

			// recherche s
			seuilBas = min;
			seuilHaut = 255;
			for (int i=min+1;i<=max;i++)
				if (tab[i] < tab[seuilBas])
					seuilBas = i;
		}

		// fin recherche valeur seuil 

		// génération lut
		for (int i = 0; i < seuilBas; i++)
			lut[i] =  0; 
		for (int i = seuilBas; i <= seuilHaut; i++)
			lut[i] = 1;
		for (int i = seuilHaut+1; i <= 255; i++)
			lut[i] = 0;

		// création image seuillée
		std::cout << "Seuillage des pixels entre " << seuilBas << " et " << seuilHaut << std::endl;
		for (int i=0; i < out.lireNbPixels(); i++) 
			out(i) = lut[this->operator ()(i)]; 

		return out;
		}
	else {
		std::cout << "Seuillage image binaire impossible" << std::endl;
		return (*this);
	}
}

// transformation

CImageNdg CImageNdg::transformation(const std::string& methode,int vMinOut, int vMaxOut) {

	CImageNdg out(this->lireHauteur(),this->lireLargeur());
	out.m_sNom     = this->lireNom()+"T";
	out.choixPalette(this->lirePalette()); // conservation de la palette
	out.m_bBinaire = this->m_bBinaire; // conservation du type

	if (methode.compare("complement") == 0) {
		if (!this->m_bBinaire) {
			// ndg -> 255-ndg
			// création lut pour optimisation calcul
			std::vector<int> lut;
			lut.resize(256);

			for (int i=0; i < 256; i++)
				lut[i] = (int)(255 - i);
			for (int i=0; i < out.lireNbPixels(); i++) 
				out(i) = lut[this->operator()(i)]; 
		}
		else {
			// 0 -> 1 et 1 -> 0
			for (int i=0; i < out.lireNbPixels(); i++) 
				out(i) = !this->operator()(i);
		}
	}
	else
		if (methode.compare("expansion") == 0) {
			std::vector<unsigned long> h = this->histogramme(false); 
			// recherche min et max image
			int min = 0,max = 255;
			int i=0;
			while (h[i] == 0)
				i++;
			min = i;
			i = 255;
			while (h[i] == 0)
				i--;
			max = i;
	
			if (max > min) {	
				double a=(double)(vMaxOut-vMinOut)/(double)(max-min);
				double b=(double)vMinOut-a*min;

				std::vector<int> lut;
				lut.resize(256);

				for (int i=min; i<=max; i++)
					lut[i] = (int)(a*i+b);

				std::cout << "Expansion dynamique [" << min << " - " << max << "] vers [" << vMinOut << " - " << vMaxOut << "]" << std::endl;
				for (int i=0; i < out.lireNbPixels(); i++) 
					out(i) = lut[this->operator()(i)]; 
			}
			else 
				for (i=0;i<this->lireNbPixels();i++)
					out(i) = this->operator()(i);
		}
		else
			if (methode.compare("egalisation") == 0) {
					std::vector<unsigned long> h = this->histogramme(false); 
					std::vector<unsigned long> hC = h;
					for (int i=1;i<(int)h.size();i++)
						hC[i] = hC[i-1] + h[i];
					
					// recherche min et max image
					int min = 0,max = 255;
					int i=0;
					while (h[i] == 0)
						i++;
					min = i;
					i = 255;
					while (h[i] == 0)
						i--;
					max = i;

					std::vector<int> lut;
					lut.resize(256);

					for (int i=min; i<=max; i++)
						lut[i] = (int)( ((double)hC[i] / (double)this->lireNbPixels() )*(double)255 );

					std::cout << "Egalisation histogramme sur [" << min << " - " << max << "] vers [0 - 255]" << std::endl;
					for (int i=0; i < out.lireNbPixels(); i++) 
						out(i) = lut[this->operator()(i)]; 
			}

	return out;
}

// morphologie
		
CImageNdg CImageNdg::morphologie(const std::string& methode, const std::string& eltStructurant) {
		
	CImageNdg out(this->lireHauteur(),this->lireLargeur());
	out.m_sNom     = this->lireNom()+"M";
	out.choixPalette(this->lirePalette()); // conservation de la palette
	out.m_bBinaire = this->m_bBinaire; // conservation du type
		
	if (methode.compare("erosion") == 0) {	
		CImageNdg agrandie(this->lireHauteur()+2,this->lireLargeur()+2);

		// gestion des bords
		if (this->lireBinaire()) {
			int pix;

			for (pix=0;pix<agrandie.lireLargeur();pix++) {
				agrandie(0,pix) = 1;
				agrandie(this->lireHauteur()-1,pix) = 1;
			}
			for (pix=1;pix<agrandie.lireHauteur()-1;pix++) {
				agrandie(pix,0) = 1;
				agrandie(pix,this->lireLargeur()-1) = 1;
			}
		}
		else {
			int pix;

			for (pix=0;pix<agrandie.lireLargeur();pix++) {
				agrandie(0,pix) = 255;
				agrandie(this->lireHauteur()-1,pix) = 255;
			}
			for (pix=1;pix<agrandie.lireHauteur()-1;pix++) {
				agrandie(pix,0) = 255;
				agrandie(pix,this->lireLargeur()-1) = 255;
			}
		}

		// gestion du coeur
		for (int i=0;i<this->lireHauteur();i++)
			for (int j=0;j<this->lireLargeur();j++) {
				agrandie(i+1,j+1)=this->operator()(i,j);
			}

		if (eltStructurant.compare("V4") == 0) {
			for (int i=1;i<agrandie.lireHauteur()-1;i++)
				for (int j=1;j<agrandie.lireLargeur()-1;j++) {
					int minH = min(agrandie(i,j-1),agrandie(i,j+1));
					int minV = min(agrandie(i-1,j),agrandie(i+1,j));
					int minV4 = min(minH,minV);
					out(i-1,j-1)=min(minV4,agrandie(i,j));
				}
		}
		else {
			if (eltStructurant.compare("V8") == 0) {
			for (int i=1;i<agrandie.lireHauteur()-1;i++)
				for (int j=1;j<agrandie.lireLargeur()-1;j++) {
					int minH = min(agrandie(i,j-1),agrandie(i,j+1));
					int minV = min(agrandie(i-1,j),agrandie(i+1,j));
					int minV4 = min(minH,minV);
					int minD1 = min(agrandie(i-1,j-1),agrandie(i+1,j+1));
					int minD2 = min(agrandie(i-1,j+1),agrandie(i+1,j-1));
					int minD = min(minD1,minD2);
					int minV8 = min(minV4,minD);
					out(i-1,j-1)=min(minV8,agrandie(i,j));
				}
			}
		}
	}
	else {
		if (methode.compare("dilatation") == 0) {	
			CImageNdg agrandie(this->lireHauteur()+2,this->lireLargeur()+2);

			// gestion des bords
			int pix;

			for (pix=0;pix<agrandie.lireLargeur();pix++) {
				agrandie(0,pix) = 0;
				agrandie(agrandie.lireHauteur()-1,pix) = 0;
			}
			for (pix=1;pix<agrandie.lireHauteur()-1;pix++) {
				agrandie(pix,0) = 0;
				agrandie(pix,agrandie.lireLargeur()-1) = 0;
			}

			// gestion du coeur
			for (int i=0;i<this->lireHauteur();i++)
				for (int j=0;j<this->lireLargeur();j++) {
					agrandie(i+1,j+1)=this->operator()(i,j);
				}

			if (eltStructurant.compare("V4") == 0) {
				for (int i=1;i<agrandie.lireHauteur()-1;i++)
					for (int j=1;j<agrandie.lireLargeur()-1;j++) {
						int maxH = max(agrandie(i,j-1),agrandie(i,j+1));
						int maxV = max(agrandie(i-1,j),agrandie(i+1,j));
						int maxV4 = max(maxH,maxV);
						out(i-1,j-1)=max(maxV4,agrandie(i,j));
					}
			}
			else {
				if (eltStructurant.compare("V8") == 0) {
				for (int i=1;i<agrandie.lireHauteur()-1;i++)
					for (int j=1;j<agrandie.lireLargeur()-1;j++) {
						int maxH = max(agrandie(i,j-1),agrandie(i,j+1));
						int maxV = max(agrandie(i-1,j),agrandie(i+1,j));
						int maxV4 = max(maxH,maxV);
						int maxD1 = max(agrandie(i-1,j-1),agrandie(i+1,j+1));
						int maxD2 = max(agrandie(i-1,j+1),agrandie(i+1,j-1));
						int maxD = max(maxD1,maxD2);
						int maxV8 = max(maxV4,maxD);
						out(i-1,j-1)=max(maxV8,agrandie(i,j));
					}
				}
			}
		}
	}

	return out;
}

CImageNdg CImageNdg::tophat(const std::string& methode, const std::string& eltStructurant)
{
	CImageNdg out(this->lireHauteur(), this->lireLargeur());
	out.m_sNom = this->lireNom() + "M";
	out.choixPalette(this->lirePalette()); // conservation de la palette
	out.m_bBinaire = this->m_bBinaire; // conservation du type
	if (methode.compare("white") == 0)
	{
		out = this->morphologie("erosion", eltStructurant).morphologie("dilatation", eltStructurant);
		out = this->operation(out, "-");
	}
	else
	{
		if (methode.compare("black") == 0)
		{
			out = this->morphologie("dilatation", eltStructurant).morphologie("erosion", eltStructurant);
			out = out.operation(*this, "-");
		}
	}
	return out;
}

// filtrage

CImageNdg CImageNdg::filtrage(const std::string& methode, int Ni, int Nj) {
		
	CImageNdg out(this->lireHauteur(),this->lireLargeur());
	out.m_sNom     = this->lireNom()+"F";
	out.choixPalette(this->lirePalette()); // conservation de la palette
	out.m_bBinaire = this->m_bBinaire; // conservation du type
		
	if (methode.compare("moyennage") == 0) {	
		int nbBordsi = Ni/2;
		int nbBordsj = Nj/2;

		for (int i=0;i<this->lireHauteur();i++)
			for (int j=0;j<this->lireLargeur();j++) {
				// gestion des bords
				int dk=max(0,i-nbBordsi);
				int fk=min(i+nbBordsi,this->lireHauteur()-1);
				int dl=max(0,j-nbBordsj);
				int fl=min(j+nbBordsj,this->lireLargeur()-1);

				float somme=0;
				float moy=0;
				for (int k=dk;k<=fk;k++)
					for (int l=dl;l<=fl;l++) {
						moy += (float)this->operator()(k,l);
						somme += 1;
					}
				out(i,j)=(int)(moy/somme);
			}
	}
	else
		if (methode.compare("median") == 0) {
			int nbBordsi = Ni / 2;
			int nbBordsj = Nj / 2;

			std::vector<int> voisinage;

			for (int i = 0; i<this->lireHauteur(); i++)
				for (int j = 0; j<this->lireLargeur(); j++) {
					// gestion des bords
					int dk = max(0, i - nbBordsi);
					int fk = min(i + nbBordsi, this->lireHauteur() - 1);
					int dl = max(0, j - nbBordsj);
					int fl = min(j + nbBordsj, this->lireLargeur() - 1);

					voisinage.resize((fk - dk + 1)*(fl - dl + 1));
					int indMed = (fk - dk + 1)*(fl - dl + 1) / 2;

					// empilement 
					int indice = 0;
					for (int k = dk; k <= fk; k++)
						for (int l = dl; l <= fl; l++) {
							voisinage.at(indice) = (int)this->operator()(k, l);
							indice++;
						}

					// tri croissant
					std::sort(voisinage.begin(), voisinage.end());

					out(i, j) = voisinage.at(indMed);

					voisinage.clear();
				}
		}

	return out;
}


double CImageNdg::IOU(CImageNdg Image1, CImageNdg Image2)
{
	//Lexique local
	int inter_area = 0;
	int union_area = 0;
	float iou;

	// Parcours de chaque pixel
	for (int i = 0; i < Image1.lireHauteur(); i++) {
		for (int j = 0; j < Image1.lireLargeur(); j++) {
			if (Image1(i, j) == 255 && Image2(i, j) == 255)
			{
				inter_area++;
			}
			if (Image1(i, j) == 255 || Image2(i, j) == 255) {
				union_area++;
			}
		}
	}

	iou = round(100*(float)(inter_area) / union_area);
	return iou;
}

/*
double CImageNdg::hausdorffDistance(CImageNdg Image1, CImageNdg Image2)
{
	int nbComp_img1 = 0, nbComp_img2 = 0;//Segmenter img1 : algo  (stocker)
	CImageClasse im_seg(Image1.seuillage("otsu"), "V8");
	CImageNdg img_label1 = im_seg.toNdg("expension");
	//Segmenter img2 : truth (stocker)
	CImageClasse im_seg(Image1.seuillage("otsu"), "V8");
	CImageNdg img_label1 = im_seg.toNdg("expension");
	// Compter nombre d'élément dans img2 : truth
	// le total d'éleemnt à trouver est le nombre d'élément de img2 : truth
	// calcul les centre de gravité des élements de img2 et de img1
	std::vector<SIGNATURE_Ndg> sig_img1(img_label1, false);
	SIGNATURE_COMPOSANTE_CONNEXE* sig_img1 = (SIGNATURE_COMPOSANTE_CONNEXE*)calloc(nbComp_img1 + 1, sizeof(SIGNATURE_COMPOSANTE_CONNEXE));
	SIGNATURE_COMPOSANTE_CONNEXE* sig_img2 = (SIGNATURE_COMPOSANTE_CONNEXE*)calloc(nbComp_img2 + 1, sizeof(SIGNATURE_COMPOSANTE_CONNEXE));
	sig_img1 = signaturesImage(labelimg1, nbComp_img1);
	sig_img2 = signaturesImage(labelimg2, nbComp_img2);
	// Pour chaque élement dans img2
	//		trouver le centre de gravité dans img1 le plus proche de l'élément dans img2
	//		Associer l'élément de img2 avec l'élément de img1 le plus proche
	double minDistCentre = INFINITY;
	int* tab_index2to1 = (int*)malloc(nbComp_img2 * sizeof(int));
	int x_distCentre;
	int y_distCentre;
	float euclDist_Centre;
	double d_hausdorff = 0;
	double MAX_DISTANCE = 0;

	for (int k = 0; k < nbComp_img2; k++)
	{
		for (int l = 0; l < nbComp_img1; l++)
		{
			x_distCentre = abs((sig_img1[l].CG.abscisse) - (sig_img2[k].CG.abscisse));
			y_distCentre = abs((sig_img1[l].CG.ordonnee) - (sig_img2[k].CG.ordonnee));
			euclDist_Centre = sqrt(x_distCentre * x_distCentre + y_distCentre * y_distCentre);
			if (euclDist_Centre < minDistCentre)
			{
				minDistCentre = euclDist_Centre;
				tab_index2to1[k] = l; //object 'k' from img2 <=> object 'l' from img1
			}
		}
		//printf("tab_index2to1[%d] : %d \n", k, tab_index2to1[k]);


		double maxDistAB = 0.0;  // Distance maximale entre un point de A et le plus proche point de B
		double maxDistBA = 0.0;  // Distance maximale entre un point de B et le plus proche point de A

		for (int y1 = 0; y1 < labelimg1.Nblig; y1++)
		{
			for (int x1 = 0; x1 < labelimg1.Nbcol; x1++)
			{
				if (labelimg1.pixel[y1][x1] == tab_index2to1[k] + 1) // object 'l'
				{  // Point de l'ensemble A trouvé
					double minDistB = INFINITY;  // Distance minimale entre un point de A et B

					for (int y2 = 0; y2 < labelimg2.Nblig; y2++)
					{
						for (int x2 = 0; x2 < labelimg2.Nbcol; x2++)
						{
							if (labelimg2.pixel[y2][x2] == k + 1) // object 'k'
							{  // Point de l'ensemble B trouvé
								double dist = euclideanDistance(x1, y1, x2, y2);
								if (dist < minDistB)
								{
									minDistB = dist;
								}
							}
						}
					}

					if (minDistB > maxDistAB)
					{
						maxDistAB = minDistB;
					}
				}
			}
		}

		for (int y2 = 0; y2 < labelimg1.Nblig; y2++)
		{
			for (int x2 = 0; x2 < labelimg1.Nbcol; x2++)
			{
				if (labelimg2.pixel[y2][x2] == k + 1) //object 'k'
				{  // Point de l'ensemble B trouvé
					double minDistA = INFINITY;  // Distance minimale entre un point de B et A

					for (int y1 = 0; y1 < labelimg1.Nblig; y1++)
					{
						for (int x1 = 0; x1 < labelimg1.Nbcol; x1++)
						{
							if (labelimg1.pixel[y1][x1] == tab_index2to1[k] + 1) //object 'l'
							{  // Point de l'ensemble A trouvé
								double dist = euclideanDistance(x2, y2, x1, y1);
								if (dist < minDistA)
								{
									minDistA = dist;
								}
							}
						}
					}

					if (minDistA > maxDistBA)
					{
						maxDistBA = minDistA;
					}
				}
			}
		}
		d_hausdorff += fmax(maxDistAB, maxDistBA);
	}
	d_hausdorff /= nbComp_img2;

	MAX_DISTANCE = sqrt(img1.Nbcol * img1.Nbcol + img1.Nblig * img1.Nblig);
	if (d_hausdorff <= 0.0)
	{
		return 1.0;
	}
	else if (d_hausdorff >= MAX_DISTANCE)
	{
		return 0;
	}
	else
	{
		return 1.0 - (d_hausdorff / MAX_DISTANCE);
	}

}
*/


