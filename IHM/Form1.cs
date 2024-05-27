using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Traitement;
using static System.Windows.Forms.LinkLabel;

namespace IHM
{
	public partial class Form1 : Form
	{
		private Thread threadaff;
		private Thread threadIn;
		private Thread threadSc;
		private Thread threadRes;
		private object objtolock;

		private List<string> FilesImg;
		private List<string> FilesGT;

		private List<string> FilesImgSc;
		private List<string> FilesGTSc;
		private List<string> FilesImgIn;
		private List<string> FilesGTIn;
		private List<string> resSc;
		private List<string> resIn;


		private AutoResetEvent EV_Aff;
		private AutoResetEvent EV_In;
		private AutoResetEvent EV_Sc;

		private delegate void affResultat(PictureBox Pbx, Bitmap bmp);
		private void AfficherResultat(PictureBox Pb, Bitmap btmp)
		{
			if (Pb.InvokeRequired)
			{
				affResultat d;
				d = new affResultat(AfficherResultat);
				this.Invoke(d, new object[] { Pb, btmp });
			}
			else Pb.Image = btmp;
		}


		public Form1()
		{
			InitializeComponent();
			EV_Sc = new AutoResetEvent(false);
			EV_In = new AutoResetEvent(false);

			threadaff = new Thread(new ThreadStart(Affichage));
			threadIn = new Thread(new ThreadStart(TraitmentImgIn));
			threadSc = new Thread(new ThreadStart(TraitmentImgSc));
			threadRes = new Thread(new ThreadStart(ResCsv));

			threadRes.Start();
			threadaff.Start();

			objtolock = new object();
			FilesImg = new List<string>();
			FilesGT = new List<string>();
			FilesImgSc = new List<string>();
			FilesGTSc = new List<string>();
			FilesImgIn = new List<string>();
			FilesGTIn = new List<string>();
			resSc = new List<string>();
			resIn = new List<string>();

			EV_Aff = new AutoResetEvent(true);
		}
		double score;

		private void btnTraitement_Click(object sender, EventArgs e)
		{
			int max = FilesImg.Count();
			if (max == 1)
			{
				CImageNdgCS Img = new CImageNdgCS();
				var bmp = new Bitmap(FilesImg[0]);
				unsafe
				{
					BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
					// 1 champ texte retour C++, le seuil auto
					bmp.UnlockBits(bmpData);
					pbRes.Image = bmp;
				}

				FilesImg.Clear();
				FilesImgIn.Clear();
				FilesImgSc.Clear();
				FilesGT.Clear();
				FilesGTIn.Clear();
				FilesGTSc.Clear();
			}
			else if (max > 1)
			{
				threadIn.Start();
				threadSc.Start();
			}
		}

		private void imageUniqueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ofdImg.ShowDialog() == DialogResult.OK)
			{
				if (ofdImg.FileName.EndsWith(".bmp"))
				{
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
					FilesImg.Add(ofdImg.FileName);
					FilesGT.Add(GroungTruthPath);
					if (newextention.StartsWith("Sc"))
					{
						FilesImgSc.Add(ofdImg.FileName);
						FilesGTSc.Add(GroungTruthPath);
					}
					else
					{
						FilesImgIn.Add(ofdImg.FileName);
						FilesGTIn.Add(GroungTruthPath);
					}

					EV_Aff.Set();
				}
			}
		}

		private void dossierDimageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FBDDir.ShowDialog() == DialogResult.OK)
			{
				if (FBDDir.SelectedPath.EndsWith(" - bmp"))
				{
					var infoDirImg = new DirectoryInfo(FBDDir.SelectedPath);
					foreach (var fileimg in infoDirImg.GetFiles())
					{
						var strings = fileimg.FullName.Split('\\');
						var GroungTruthPath = strings[0] + "\\";
						for (int i = 1; i < strings.Length - 2; i++)
						{
							GroungTruthPath = Path.Combine(GroungTruthPath, strings[i]);
						}

						GroungTruthPath = Path.Combine(GroungTruthPath, "Ground truth - png");
						var extesion = strings[strings.Length - 1].Split('.');
						var newextention = extesion[0] + ".png";
						GroungTruthPath = Path.Combine(GroungTruthPath, newextention);
						FilesImg.Add(fileimg.FullName);
						FilesGT.Add(GroungTruthPath);

						if (newextention.StartsWith("Sc"))
						{
							FilesImgSc.Add(fileimg.FullName);
							FilesGTSc.Add(GroungTruthPath);
						}
						else
						{
							FilesImgIn.Add(fileimg.FullName);
							FilesGTIn.Add(GroungTruthPath);
						}

					}

					EV_Aff.Set();
				}
			}
		}

		private void Affichage()
		{
			while (true)
			{
				while (!EV_Aff.WaitOne()) ;

				for (int i = 0; i < FilesImg.Count(); i++)
				{
					Bitmap bmpIm = new Bitmap(FilesImg[i]);
					Bitmap bmpGt = new Bitmap(FilesGT[i]);

					AfficherResultat(pBImgRef, bmpIm);
					AfficherResultat(pBVerite, bmpGt);
					Thread.Sleep(50);
				}

			}
		}

		private void TraitmentImgSc()
		{
			for (int i = 0; i < FilesImgSc.Count(); i++)
			{
				string res = $"Sc_{i + 1}; IOU; Hausdorf; moyenne des 2";
				CImageNdgCS Img = new CImageNdgCS();
				var bmp = new Bitmap(FilesImgSc[i]);
				unsafe
				{
					BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
					bmp.UnlockBits(bmpData);
					AfficherResultat(pbRes, bmp);
				}
				resSc.Add(res);
			}
			EV_Sc.Set();
			FilesImgSc.Clear();
			FilesGTSc.Clear();
		}

		private void TraitmentImgIn()
		{
			for (int i = 0; i < FilesImgIn.Count(); i++)
			{
				string res = $"In_{i + 1}; IOU; Hausdorf; moyenne des 2";
				CImageNdgCS Img = new CImageNdgCS();
				var bmp = new Bitmap(FilesImgIn[i]);
				unsafe
				{
					BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
					bmp.UnlockBits(bmpData);
					AfficherResultat(pbRes, bmp);
				}
				resIn.Add(res);
			}
			EV_In.Set();
			FilesImgIn.Clear();
			FilesGTIn.Clear();
		}

		private void ResCsv()
		{

			while (!EV_In.WaitOne() && !EV_Sc.WaitOne()) ;
			string csvSc = "ResultatSc.csv";
			File.WriteAllLines(csvSc, resSc);
			string csvIn = "ResultatIn.csv";
			File.WriteAllLines(csvIn, resIn);
			resIn.Clear();
			resSc.Clear();
		}
	}
}
