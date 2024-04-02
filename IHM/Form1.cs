using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

                CImageNdgCS cImageNdgCS = new CImageNdgCS(ofdImg.FileName);

                int i = 0;

            }
        }
    }
}
