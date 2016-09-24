/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap previewModifiedEdge = null;
        private Bitmap originalModifiedEdge = null;
        private Bitmap resultBitmap = null;
        private bool edge = true;

        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
            cmbApplyFilter.Visible = false;
            btnSaveNewImage.Visible = false;
            btnResetAll.Visible = false;
            btnApplyFilter.Visible = false;

        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;

                ApplyEdgeDetection(true);
                btnApplyFilter.Visible = true;
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {

            ApplyEdgeDetection(false);
            cmbEdgeDetection.Visible = false;
            cmbApplyFilter.Visible = true;
            btnApplyFilter.Visible = false;
            btnResetAll.Visible = true;
            btnOpenOriginal.Visible = false;
            btnSaveNewImage.Visible = true;
            cmbApplyFilter.SelectedIndex = 0;
            edge = false;
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {

            ApplyEdgeDetection(true);
            cmbEdgeDetection.Visible = true;
            cmbApplyFilter.Visible = false;
            btnApplyFilter.Visible = true;
            btnResetAll.Visible = false;
            btnOpenOriginal.Visible = true;
            btnSaveNewImage.Visible = false;
            edge = true;
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        private void ApplyEdgeDetection(bool preview)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = previewBitmap;
            Bitmap bitmapResult = null;

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {
                String edgeDetectionSelected = cmbEdgeDetection.SelectedItem.ToString();


                switch (edgeDetectionSelected)
                {
                    case "None":

                        bitmapResult = selectedSource;
                        break;

                    case "Laplacian 3x3":

                        bitmapResult = selectedSource.Laplacian3x3Filter(false);
                        break;
                    case "Laplacian 3x3 Grayscale":

                        bitmapResult = selectedSource.Laplacian3x3Filter(true);
                        break;
                    case "Laplacian 5x5":

                        bitmapResult = selectedSource.Laplacian5x5Filter(false);
                        break;
                    case "Laplacian 5x5 Grayscale":

                        bitmapResult = selectedSource.Laplacian5x5Filter(true);
                        break;
                    case "Laplacian of Gaussian":

                        bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                        break;
                    case "Laplacian 3x3 of Gaussian 3x3":

                        bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 1":

                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                        break;
                    case "Laplacian 5x5 of Gaussian 3x3":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 1":

                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                        break;
                    case "Sobel 3x3":

                        bitmapResult = selectedSource.Sobel3x3Filter(false);
                        break;
                    case "Sobel 3x3 Grayscale":

                        bitmapResult = selectedSource.Sobel3x3Filter();
                        break;
                    case "Prewitt":
                        bitmapResult = selectedSource.PrewittFilter(false);

                        break;
                    case "Prewitt Grayscale":
                        bitmapResult = selectedSource.PrewittFilter();
                        break;
                    case "Kirsch":
                        bitmapResult = selectedSource.KirschFilter(false);
                        break;
                    case "Kirsch Grayscale":
                        bitmapResult = selectedSource.KirschFilter();
                        break;

                }

            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                    previewModifiedEdge = (Bitmap) picPreview.Image;
                }
            }
        }

        //Test filter apply
        private void ApplyFilter(bool preview)
        {
            Bitmap imageToFilter = null;
            Bitmap bitmapResultFilter = null;

            if (cmbApplyFilter.SelectedIndex == -1)
            {
                return;
            }


            if (preview == true)
            {
                imageToFilter = previewModifiedEdge;
            }
            else
            {
                imageToFilter = resultBitmap;
            }


            String filterSelected = cmbApplyFilter.SelectedItem.ToString();

            switch (filterSelected)
            {
                case "None":
                    bitmapResultFilter = imageToFilter;
                    break;
                case "Night Filter":
                    bitmapResultFilter = imageToFilter.NightFilter();
                    break;
                case "Hell Filter":

                    bitmapResultFilter = imageToFilter.HellFilter();
                    break;
                case "Miami Filter":

                    bitmapResultFilter = imageToFilter.MiamiFilter();
                    break;
                case "Zen Filter":

                    bitmapResultFilter = imageToFilter.ZenFilter();
                    break;
                case "Black and White":

                    bitmapResultFilter = imageToFilter.BlackNWhite();
                    break;
                case "Swap Filter":

                    bitmapResultFilter = imageToFilter.SwapFilter();
                    break;
                case "Crazy Filter":
                    bitmapResultFilter = imageToFilter.CrazyFilter();
                    break;
                case "Mega Filter Green":
                    //TODO Changes
                    bitmapResultFilter = imageToFilter.Laplacian3x3OfGaussian5x5Filter2();
                    break;
                case "Mega Filter Orange":
                    bitmapResultFilter = imageToFilter.Laplacian5x5OfGaussian3x3Filter();
                    break;
                case "Mega Filter Pink":
                    bitmapResultFilter = imageToFilter.Laplacian5x5OfGaussian5x5Filter1();
                    break;
                case "Mega Filter Custom":

                    bitmapResultFilter = imageToFilter.Laplacian5x5OfGaussian5x5Filter2();
                    break;
                case "Rainbow Filter":
                    bitmapResultFilter = imageToFilter.Sobel3x3Filter(false);
                    break;
            }


            if (bitmapResultFilter != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResultFilter;
                }
                else
                {
                    resultBitmap = bitmapResultFilter;
                }
            }
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            if (edge == true)
            {
                ApplyEdgeDetection(true);
            }
            else
            {
                ApplyFilter(true);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
