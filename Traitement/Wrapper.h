#pragma once

#include "ImageClasse.h"
#include "ImageNdg.h"
#include "ImageCouleur.h"
#include "ImageDouble.h"

#include <windows.h>

class Wrapper {

	///////////////////////////////////////
private:
	///////////////////////////////////////

	// data n�cessaires � l'IHM donc fonction de l'application cibl�e
	CImageNdg* imgPt;       // 

	///////////////////////////////////////
public:
	///////////////////////////////////////

	// constructeurs
	_declspec(dllexport) Wrapper(); // par d�faut

	_declspec(dllexport) Wrapper(int nbChamps, byte* data, int stride, int nbLig, int nbCol);

	_declspec(dllexport) ~Wrapper();

	// get et set 

	_declspec(dllexport) CImageNdg* imgData() const {
		return imgPt;
	}

};

extern "C" _declspec(dllexport) Wrapper* objetLib()
{	
	Wrapper* pImg = new Wrapper();
	return pImg;
}
