#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <windows.h>
#include <cmath>
#include <vector>
#include <ctime>
#include <stack>

#include "Wrapper.h"

Wrapper::Wrapper() {
	this->imgPt = NULL;
}

Wrapper::Wrapper(int nbChamps, byte* data, int stride, int nbLig, int nbCol)
{
	this->imgPt = new CImageNdg(nbLig, nbCol);
	CImageNdg out(nbLig, nbCol);

	// on remplit les pixels 
	byte* pixPtr = (byte*)data;
	for (int y = 0; y < nbLig; y++)
	{
		for (int x = 0; x < nbCol; x++)
		{
			this->imgPt->operator()(y, x) = pixPtr[ 1 * x + 0];
		}
		pixPtr += stride; // largeur une seule ligne gestion multiple 32 bits
	}
}

Wrapper::~Wrapper() {

	if (imgPt)
		(*this->imgPt).~CImageNdg();

}