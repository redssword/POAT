#pragma once

#include "ImageClasse.h"
#include "ImageNdg.h"
#include "ImageCouleur.h"
#include "ImageDouble.h"

#include <windows.h>

class ClibIHM {

	///////////////////////////////////////
private:
	///////////////////////////////////////

	// data nécessaires à l'IHM donc fonction de l'application ciblée
	int						nbDataImg; // nb champs Texte de l'IHM
	std::vector<double>		dataFromImg; // champs Texte de l'IHM
	CImageCouleur*          imgPt;       // 

	//GT
	int						nbDataImg_gt; // nb champs Texte de l'IHM
	std::vector<double>		dataFromImg_gt; // champs Texte de l'IHM
	CImageCouleur*          imgPt_gt;       // 

	///////////////////////////////////////
public:
	///////////////////////////////////////

	// constructeurs
	_declspec(dllexport) ClibIHM(); // par défaut

	_declspec(dllexport) ClibIHM(bool sc, int nbChamps, byte* data, int stride, int nbLig, int nbCol,
		                         int nbChamps_gt, byte* data_gt, int stride_gt, int nbLig_gt, int nbCol_gt); // par image format bmp C#

	_declspec(dllexport) ~ClibIHM();

	// get et set 

	_declspec(dllexport) int lireNbChamps() const {
		return nbDataImg;
	}

	_declspec(dllexport) double lireChamp(int i) const {
		return dataFromImg.at(i);
	}

	_declspec(dllexport) CImageCouleur* imgData() const {
		return imgPt;
	}

};

extern "C" _declspec(dllexport) ClibIHM* objetLib()
{
	ClibIHM* pImg = new ClibIHM();
	return pImg;
}

extern "C" _declspec(dllexport) ClibIHM* objetLibDataImg(bool sc, int nbChamps, byte * data, int stride, int nbLig, int nbCol,
	                                                     int nbChamps_gt, byte * data_gt, int stride_gt, int nbLig_gt, int nbCol_gt)
{
	ClibIHM* pImg = new ClibIHM(sc, nbChamps,data,stride,nbLig,nbCol,
		                        nbChamps_gt,data_gt,stride_gt,nbLig_gt,nbCol_gt);
	return pImg;
}

extern "C" _declspec(dllexport) double valeurChamp(ClibIHM* pImg, int i)
{
	return pImg->lireChamp(i);
}
