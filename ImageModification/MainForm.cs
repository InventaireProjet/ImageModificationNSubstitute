/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
 * It was further modified by Zappellaz Nancy & Mabillard Julien
*/
using System;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {

        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        //Image to be saved at the end of the process
        private Bitmap resultBitmap = null;

        public MainForm()
        {
            InitializeComponent();


            // Saving a new image is invisible when no image is loaded
            btnSaveNewImage.Visible = false;


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

                //EdgeDetection is applied by default
                ApplyEdgeDetection(true);

                //Since there is an image, it is possible to save it
                btnSaveNewImage.Visible = true;

            }
        }


        //Button for saving image (original method)
        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyEdgeDetection(false);

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
                    //Line added to avoid an error : http://stackoverflow.com/questions/15571022/how-to-find-reason-for-generic-gdi-error-when-saving-an-image
                    Bitmap savedImage = new Bitmap(resultBitmap);
                    savedImage.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }



        //Application of edge detection (mainly original method)
        private void ApplyEdgeDetection(bool preview)
        {


            Bitmap imageForEdgeDetection = null;
            Bitmap bitmapResultEdge = null;

            if (preview == true)
            {
                //If it is for the preview, we work on the preview image
                imageForEdgeDetection = previewBitmap;
            }
            else
            {
                //If the image is going to be saved, it is the original that is modified
                imageForEdgeDetection = originalBitmap;
            }

            //Apply the edgeDetection 
            bitmapResultEdge = imageForEdgeDetection.KirschFilter();



            if (bitmapResultEdge != null)
            {
                //If it is a preview the result is shown in the application
                if (preview == true)
                {
                    picPreview.Image = bitmapResultEdge;
                }
                //If not, it means that it will be saved as the final result
                else
                {
                    resultBitmap = bitmapResultEdge;

                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
