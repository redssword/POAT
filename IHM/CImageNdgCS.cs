using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IHM
{
    internal class CImageNdgCS
    {

        private IntPtr m_instance;

        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CImageNdg(string param);

        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DestCImageNdg(IntPtr instance);

        public CImageNdgCS(string param)
        {
            m_instance = CImageNdg(param);
            if (m_instance == IntPtr.Zero)
            {
                throw new Exception("Impossible de créer l'instance de CImageNdg.");
            }
        }

        ~CImageNdgCS()
        {
            DestCImageNdg(m_instance);
        }

        public int MaMethode()
        {
            return MaClasse_MaMethode(m_instance);
        }

        [DllImport("Traitement.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int MaClasse_MaMethode(IntPtr instance);
    }
}
