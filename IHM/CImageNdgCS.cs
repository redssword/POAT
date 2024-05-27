/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Traitement
{
    public class CImageNdgCS
    {

        public IntPtr m_instance;

        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreerCImageNdg(string param);

        public IntPtr CreerCImageNdgCs(string param)
        {
            m_instance = CreerCImageNdg(param); 
            return m_instance;
        }


        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DetruireCImageNdg(IntPtr instance);

        public CImageNdgCS()
        {
            m_instance = IntPtr.Zero; //CreerCImageNdg(param);
            //if (m_instance == IntPtr.Zero)
            //{
            //    throw new Exception("Impossible de créer l'instance de CImageNdg.");
            //}
        }

        ~CImageNdgCS()
        {
            DetruireCImageNdg(m_instance);
        }

        public int MaMethode()
        {
            return MaClasse_MaMethode(m_instance);
        }

        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int MaClasse_MaMethode(IntPtr instance);
    }
}*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Drawing;

namespace Traitement
{
	public class CImageNdgCS
	{
		// on crée une classe C# avec pointeur sur l'objet C++
		// puis des static extern exportées de chaque méthode utile de la classe C++
		public IntPtr ClPtr;
		public int score;

		public CImageNdgCS()
		{
			ClPtr = IntPtr.Zero;
		}

		~CImageNdgCS()
		{
			if (ClPtr != IntPtr.Zero)
				ClPtr = IntPtr.Zero;
		}


		// va-et-vient avec constructeur C#/C++
		// obligatoire dans toute nouvelle classe propre à l'application

		[DllImport("libImage.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr objetLib();

		public IntPtr objetLibPtr()
		{
			ClPtr = objetLib();
			return ClPtr;
		}

		[DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr objetLibDataImg(bool sc, int nbChamps, IntPtr data, int stride, int nbLig, int nbCol, 
			                                        int nbChamps_gt, IntPtr data_gt, int stride_gt, int nbLig_gt, int nbCol_gt);
		
		public IntPtr objetLibDataImgPtr(bool sc, int nbChamps, IntPtr data, int stride, int nbLig, int nbCol,
                                         int nbChamps_gt, IntPtr data_gt, int stride_gt, int nbLig_gt, int nbCol_gt)
        {
			ClPtr = objetLibDataImg(sc, nbChamps, data, stride, nbLig, nbCol, 
				                    nbChamps_gt, data_gt, stride_gt, nbLig_gt, nbCol_gt);
			return ClPtr;
		}

		[DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern double valeurChamp(IntPtr pImg, int i);

		public double objetLibValeurChamp(int i)
		{
			return valeurChamp(ClPtr, i);
		}
	}
}

