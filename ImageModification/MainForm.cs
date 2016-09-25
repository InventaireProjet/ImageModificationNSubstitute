/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
 * It was further modified by Zappellaz Nancy & Mabillard Julien
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
        //Variable used to store the image preview after its modification by an edge filter
        private Bitmap previewModifiedEdge = null;
        private Bitmap resultBitmap = null;
        //Boolean used to distinguish if the user is applying edge filters (edge = true) or color filters, used by the ValueChangedEventHandler
        private bool edge = true;

        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
            // Elements that appear only for the second filters are made invisible
            cmbApplyFilter.Visible = false;
            btnSaveNewImage.Visible = false;
            btnGoBack.Visible = false;
            btnApplyFilter.Visible = false;

        }

        //Button for loading image 
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
                //Since there is an image, it is possible to go to the filters, so the corresponding button appears
                btnApplyFilter.Visible = true;
            }
        }

        //Button to go to the color filters
        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            //EdgeDetection applied is saved and passed further
            ApplyEdgeDetection(false);

            //We change the elements usable by making them visible or invisible
            cmbEdgeDetection.Visible = false;
            cmbApplyFilter.Visible = true;
            btnApplyFilter.Visible = false;
            btnGoBack.Visible = true;
            btnOpenOriginal.Visible = false;
            btnSaveNewImage.Visible = true;
            cmbApplyFilter.SelectedIndex = 0;
            //For the ValueChangedEventHandler
            edge = false;
        }

        //Button to go back to edge detection from filters
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            //We change the elements usable by making them visible or invisible
            ApplyEdgeDetection(true);
            cmbEdgeDetection.Visible = true;
            cmbApplyFilter.Visible = false;
            btnApplyFilter.Visible = true;
            btnGoBack.Visible = false;
            btnOpenOriginal.Visible = true;
            btnSaveNewImage.Visible = false;
            edge = true;
        }

        //Button for saving image (original method)
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

        //Application of edge detection (mainly original method)
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
                    //Used to store the preview in the filter phase, to avoid loss of preview quality
                    previewModifiedEdge = (Bitmap)picPreview.Image;
                }
            }
        }

        //Second part dedicated to the filters
        private void ApplyFilter(bool preview)
        {
            Bitmap imageToFilter = null;
            Bitmap bitmapResultFilter = null;

            if (cmbApplyFilter.SelectedIndex == -1)
            {
                return;
            }

            //If the image is previewed we work on the preview image
            if (preview == true)
            {
                imageToFilter = previewModifiedEdge;
            }
            //Else we work on the original image that was (or not) modified by edge detection
            else
            {
                imageToFilter = resultBitmap;
            }

            //The filter to apply is selected from the dropdownlist
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
                    //TODO CHANGES FROME HERE TO THE END OF THE SWITCH CASE !!
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
                //If it is a preview the result is shown in the application
                if (preview == true)
                {
                    picPreview.Image = bitmapResultFilter;
                }
                //If not, it means that it will be saved as the final result
                else
                {
                    resultBitmap = bitmapResultFilter;
                }
            }
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            //The two parts of the application are distinguished by the boolean edge that determines if it is EdgeDetection or Filter that is activated
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
