using System;
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
}
