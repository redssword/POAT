#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <windows.h>
#include <cmath>
#include <vector>
#include <ctime>
#include <stack>

#include "libIHM.h"

ClibIHM::ClibIHM() {

	this->nbDataImg = 0;
	this->dataFromImg.clear();
	this->imgPt = NULL;
}

ClibIHM::ClibIHM(int nbChamps, byte* data, int stride, int nbLig, int nbCol, 
	             int nbChamps_gt, byte* data_gt, int stride_gt, int nbLig_gt, int nbCol_gt)
{
	//// Image input
	this->nbDataImg = nbChamps;
	this->dataFromImg.resize(nbChamps);

	this->imgPt = new CImageCouleur(nbLig, nbCol);
	CImageCouleur out(nbLig, nbCol);

	// on remplit les pixels 
	byte* pixPtr = (byte*)data;
	for (int y = 0; y < nbLig; y++)
	{
		for (int x = 0; x < nbCol; x++)
		{
			this->imgPt->operator()(y, x)[0] = pixPtr[3 * x + 2];
			this->imgPt->operator()(y, x)[1] = pixPtr[3 * x + 1];
			this->imgPt->operator()(y, x)[2] = pixPtr[3 * x ];
		}
		pixPtr += stride; // largeur une seule ligne gestion multiple 32 bits
	}

	//// Image Ground truth
	this->nbDataImg_gt = nbChamps_gt;
	this->dataFromImg_gt.resize(nbChamps_gt);

	this->imgPt_gt = new CImageCouleur(nbLig_gt, nbCol_gt);
	CImageCouleur image_gt(nbLig_gt, nbCol_gt);

	// on remplit les pixels 
	byte* pixPtr_gt = (byte*)data_gt;
	for (int y = 0; y < nbLig_gt; y++)
	{
		for (int x = 0; x < nbCol_gt; x++)
		{
			this->imgPt_gt->operator()(y, x)[0] = pixPtr_gt[3 * x + 2];
			this->imgPt_gt->operator()(y, x)[1] = pixPtr_gt[3 * x + 1];
			this->imgPt_gt->operator()(y, x)[2] = pixPtr_gt[3 * x];
		}
		pixPtr_gt += stride; // largeur une seule ligne gestion multiple 32 bits
	}

	//// SEUILLAGE
	CImageNdg seuil;

	int seuilBas = 128;
	int seuilHaut = 255;

	seuil = this->imgPt->plan().seuillage("automatique",seuilBas,seuilHaut);

	//this->dataFromImg.at(0) = seuilBas;

	for (int i = 0; i < seuil.lireNbPixels(); i++)
	{
		out(i)[0] = (unsigned char)(255*(int)seuil(i));
		out(i)[1] = (unsigned char)(255 * (int)seuil(i));
		out(i)[2] = (unsigned char)(255 * (int)seuil(i));
	}
		

	pixPtr = (byte*)data;
	for (int y = 0; y < nbLig; y++)
	{
		for (int x = 0; x < nbCol; x++)
		{
			pixPtr[3 * x + 2] = out(y, x)[0];
			pixPtr[3 * x + 1] = out(y, x)[1];
			pixPtr[3 * x] = out(y, x)[2];
		}
		pixPtr += stride; // largeur une seule ligne gestion multiple 32 bits
	}

	this->dataFromImg.at(0) = this->imgPt->plan().IOU(this->imgPt->plan(), this->imgPt_gt->plan());
}


ClibIHM::~ClibIHM() {
	
	if (imgPt)
		(*this->imgPt).~CImageCouleur(); 
	this->dataFromImg.clear();
}