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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace IHM
{
	public partial class Form1 : Form
	{
		private int ImAff;
		private Thread threadIn;
		private Thread threadSc;
		private Thread threadRes;

		private List<string> FilesImg;
		private List<string> FilesGT;

		private List<string> FilesImgSc;
		private List<string> FilesGTSc;
		private List<string> FilesImgIn;
		private List<string> FilesGTIn;
		private List<string> resSc;
		private List<string> resIn;


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

			threadIn = new Thread(new ThreadStart(TraitmentImgIn));
			threadSc = new Thread(new ThreadStart(TraitmentImgSc));
			threadRes = new Thread(new ThreadStart(ResCsv));

			threadRes.Start();

			FilesImg = new List<string>();
			FilesGT = new List<string>();
			FilesImgSc = new List<string>();
			FilesGTSc = new List<string>();
			FilesImgIn = new List<string>();
			FilesGTIn = new List<string>();
			resSc = new List<string>();
			resIn = new List<string>();

			ImAff = 0;
			TimerAff.Start();

		}

		private void btnTraitement_Click(object sender, EventArgs e)
		{
			threadIn.Start();
			threadSc.Start();
		}

		private void imageUniqueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ofdImg.ShowDialog() == DialogResult.OK)
			{
				if (ofdImg.FileName.EndsWith(".bmp"))
				{
					FilesImg.Clear();
					FilesImgSc.Clear();
					FilesGTSc.Clear();
					FilesImgIn.Clear();
					FilesGTIn.Clear();

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
				}
			}
		}

		private void dossierDimageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FBDDir.ShowDialog() == DialogResult.OK)
			{
				if (FBDDir.SelectedPath.EndsWith(" - bmp"))
				{
					FilesImg.Clear();
					FilesImgSc.Clear();
					FilesGTSc.Clear();
					FilesImgIn.Clear();
					FilesGTIn.Clear();

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
				}
				resSc.Add(res);
				Thread.Sleep(10);
			}
			EV_Sc.Set();
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
				}
				resIn.Add(res);
				Thread.Sleep(10);
			}
			EV_In.Set();
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

		private void TimerAff_Tick(object sender, EventArgs e)
		{
			TimerAff.Stop();
			if (ImAff == 300)
				ImAff = 0;

			if (FilesImgIn.Count > 0)
			{
				var bmpIm = new Bitmap(FilesImgSc[ImAff]);
				var bmpGt = new Bitmap(FilesGTSc[ImAff]);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpIm.LockBits(new Rectangle(0, 0, bmpIm.Width, bmpIm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmpIm.Height, bmpIm.Width);
					bmpIm.UnlockBits(bmpData);
				}

				AfficherResultat(pBImgRefSc, bmpIm);
				AfficherResultat(pBVeriteSc, bmpGt);
			}

			if (FilesImgIn.Count >0)
			{
				var bmpImIn = new Bitmap(FilesImgIn[ImAff]);
				var bmpGtIn = new Bitmap(FilesGTIn[ImAff]);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpImIn.LockBits(new Rectangle(0, 0, bmpImIn.Width, bmpImIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmpImIn.Height, bmpImIn.Width);
					bmpImIn.UnlockBits(bmpData);
				}

				AfficherResultat(pBImgRefIn, bmpImIn);
				AfficherResultat(pBVeriteIn, bmpGtIn);
			}
			ImAff++;

			TimerAff.Start();
		}

		private void TbAff_ValueChanged(object sender, EventArgs e)
		{
			TimerAff.Interval = TbAff.Value;
		}
	}
}
