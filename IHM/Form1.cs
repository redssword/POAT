using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Traitement;

namespace IHM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btImg_Click(object sender, EventArgs e)
        {
            if (ofdImg.ShowDialog() == DialogResult.OK)
            {
                var bmp = new Bitmap(ofdImg.FileName);
                pBImgRef.Image = bmp;

                var strings = ofdImg.FileName.Split('\\');
                var GroungTruthPath = strings[0] + "\\";
                for (int i = 1;  i < strings.Length-2; i++)
                {
                    GroungTruthPath = Path.Combine(GroungTruthPath, strings[i]);
                }

                GroungTruthPath = Path.Combine(GroungTruthPath, "Ground truth - png");
                var extesion = strings[strings.Length - 1].Split('.');
                var newextention = extesion[0] + ".png";
                GroungTruthPath = Path.Combine(GroungTruthPath, newextention);

                var GroundTruth = new Bitmap (GroungTruthPath);
                pBVerite.Image = GroundTruth;

                

            }
        }

        private void btnTraitement_Click(object sender, EventArgs e)
        {
            
            pbRes.Image = pBImgRef.Image;
            CImageNdgCS obj = new CImageNdgCS();
            obj.CreerCImageNdgCs(ofdImg.FileName);
            //Comprendre les parametres à envoyer à la dll
        }
    }
}
