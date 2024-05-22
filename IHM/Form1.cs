using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Traitement;
using static System.Net.Mime.MediaTypeNames;

namespace IHM
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnTraitement_Click(object sender, EventArgs e)
		{
			CImageNdgCS Img = new CImageNdgCS();
			var bmp = new Bitmap(pBImgRef.Image);
			unsafe
			{
				BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
				// 1 champ texte retour C++, le seuil auto
				bmp.UnlockBits(bmpData);
				pbRes.Image = bmp;
			}
		}

		private void imageUniqueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ofdImg.ShowDialog() == DialogResult.OK)
			{
				if (ofdImg.FileName.EndsWith(".bmp"))
				{
					var bmp = new Bitmap(ofdImg.FileName);
					pBImgRef.Image = bmp;
					var strings = ofdImg.FileName.Split('\\');
					var GroungTruthPath = strings[0] + "\\";
					for (int i = 1; i < strings.Length - 2; i++)
					{
						GroungTruthPath = Path.Combine(GroungTruthPath, strings[i]);
					}

					GroungTruthPath = Path.Combine(GroungTruthPath, "Ground truth - png");
					var extesion = strings[strings.Length - 1].Split('.');
					var newextention = extesion[0] + ".png";
					GroungTruthPath = Path.Combine(GroungTruthPath, newextention);

					var GroundTruth = new Bitmap(GroungTruthPath);
					pBVerite.Image = GroundTruth;
				}
			}
		}

		private void dossierDimageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FBDDir.ShowDialog() == DialogResult.OK)
			{

			}
		}
	}
}
