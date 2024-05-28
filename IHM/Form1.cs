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
using Label = System.Windows.Forms.Label;

namespace IHM
{
	public partial class Form1 : Form
	{
		private double score;
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

		private delegate void affImage(PictureBox Pbx, Bitmap bmp);
		private void AfficherResultat(PictureBox Pb, Bitmap btmp)
		{
			if (Pb.InvokeRequired)
			{
				affImage d;
				d = new affImage(AfficherResultat);
				this.Invoke(d, new object[] { Pb, btmp });
			}
			else Pb.Image = btmp;
		}

		private delegate void affScore(Label lbl, string res);
		private void AfficherScore(Label lbl, string res)
		{
			if (lbl.InvokeRequired)
			{
				affScore d;
				d = new affScore(AfficherScore);
				this.Invoke(d, new object[] { lbl, res });
			}
			else lbl.Text = res;
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
					TimerAff.Start();
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
					TimerAff.Start();
				}
			}
		}

		private void TraitmentImgSc()
		{
			for (int i = 0; i < FilesImgSc.Count(); i++)
			{
				string res = $"Sc_{i + 1};";
				var bmpImSc = new Bitmap(FilesImgSc[i]);
				var bmpGtIn = new Bitmap(FilesGTSc[i]);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpImSc.LockBits(new Rectangle(0, 0, bmpImSc.Width, bmpImSc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGtIn.LockBits(new Rectangle(0, 0, bmpGtIn.Width, bmpGtIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(true, 1, bmpData.Scan0, bmpData.Stride, bmpImSc.Height, bmpImSc.Width,
												 1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpImSc.UnlockBits(bmpData);
					bmpGtIn.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				res += score + "; Hausdorf; moyenne des 2 ";
				resSc.Add(res);
			}
			EV_Sc.Set();
		}

		private void TraitmentImgIn()
		{
			for (int i = 0; i < FilesImgIn.Count(); i++)
			{
				string res = $"In_{i + 1};";
				var bmpImSc = new Bitmap(FilesImgIn[i]);
				var bmpGtIn = new Bitmap(FilesGTIn[i]);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpImSc.LockBits(new Rectangle(0, 0, bmpImSc.Width, bmpImSc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGtIn.LockBits(new Rectangle(0, 0, bmpGtIn.Width, bmpGtIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(false, 1, bmpData.Scan0, bmpData.Stride, bmpImSc.Height, bmpImSc.Width,
												 1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpImSc.UnlockBits(bmpData);
					bmpGtIn.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				res += score + "; Hausdorf; moyenne des 2 ";
				resIn.Add(res);
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

			if (FilesImgSc.Count > 1)
			{
				var ImgOrigine = new Bitmap(FilesImgSc[ImAff]);
				var bmpIm = new Bitmap(FilesImgSc[ImAff]);
				var bmpGt = new Bitmap(FilesGTSc[ImAff]);

				AfficherResultat(pBImgRefSc, ImgOrigine);
				AfficherResultat(pBVeriteSc, bmpGt);
				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpIm.LockBits(new Rectangle(0, 0, bmpIm.Width, bmpIm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGt.LockBits(new Rectangle(0, 0, bmpGt.Width, bmpGt.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(true, 1, bmpData.Scan0, bmpData.Stride, bmpIm.Height, bmpIm.Width,
										         1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpIm.UnlockBits(bmpData);
					bmpGt.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				AfficherResultat(pbResSc, bmpIm);
				AfficherScore(lbResIOUSc, score.ToString());
			}
			
			else if (FilesImgSc.Count == 1)
			{
				var ImgOrigine = new Bitmap(FilesImgSc[0]);
				var bmpIm = new Bitmap(FilesImgSc[0]);
				var bmpGt = new Bitmap(FilesGTSc[0]);

				AfficherResultat(pBImgRefSc, ImgOrigine);
				AfficherResultat(pBVeriteSc, bmpGt);
				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpIm.LockBits(new Rectangle(0, 0, bmpIm.Width, bmpIm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGt.LockBits(new Rectangle(0, 0, bmpGt.Width, bmpGt.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(true, 1, bmpData.Scan0, bmpData.Stride, bmpIm.Height, bmpIm.Width,
												 1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpIm.UnlockBits(bmpData);
					bmpGt.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				AfficherResultat(pbResSc, bmpIm);
				AfficherScore(lbResIOUSc, score.ToString());
			}

			if (FilesImgIn.Count > 1)
			{
				var bmgOrigine = new Bitmap(FilesImgIn[ImAff]);
				var bmpImIn = new Bitmap(FilesImgIn[ImAff]);
				var bmpGtIn = new Bitmap(FilesGTIn[ImAff]);

				AfficherResultat(pBImgRefIn, bmgOrigine);
				AfficherResultat(pBVeriteIn, bmpGtIn);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpImIn.LockBits(new Rectangle(0, 0, bmpImIn.Width, bmpImIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGtIn.LockBits(new Rectangle(0, 0, bmpGtIn.Width, bmpGtIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(false, 1, bmpData.Scan0, bmpData.Stride, bmpImIn.Height, bmpImIn.Width,
												 1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpImIn.UnlockBits(bmpData);
					bmpGtIn.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				AfficherResultat(pbResIN, bmpImIn);
				AfficherScore(lbResIOUIn, score.ToString());

			}

			else if (FilesImgIn.Count == 1)
			{
				var bmgOrigine = new Bitmap(FilesImgIn[0]);
				var bmpImIn = new Bitmap(FilesImgIn[0]);
				var bmpGtIn = new Bitmap(FilesGTIn[0]);

				AfficherResultat(pBImgRefIn, bmgOrigine);
				AfficherResultat(pBVeriteIn, bmpGtIn);

				CImageNdgCS Img = new CImageNdgCS();
				unsafe
				{
					BitmapData bmpData = bmpImIn.LockBits(new Rectangle(0, 0, bmpImIn.Width, bmpImIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					BitmapData bmpdataGt = bmpGtIn.LockBits(new Rectangle(0, 0, bmpGtIn.Width, bmpGtIn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

					Img.objetLibDataImgPtr(false, 1, bmpData.Scan0, bmpData.Stride, bmpImIn.Height, bmpImIn.Width,
												 1, bmpdataGt.Scan0, bmpdataGt.Stride, bmpdataGt.Height, bmpdataGt.Width);
					bmpImIn.UnlockBits(bmpData);
					bmpGtIn.UnlockBits(bmpdataGt);
					score = Img.objetLibValeurChamp(0);
				}
				AfficherResultat(pbResIN, bmpImIn);
				AfficherScore(lbResIOUIn, score.ToString());
			}

			ImAff++;

			TimerAff.Start();
		}

		private void TbAff_ValueChanged(object sender, EventArgs e)
		{
			TimerAff.Interval = TbAff.Value;
		}

		private void démarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TimerAff.Start();
		}

		private void arréterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TimerAff.Stop();
		}
	}
}
