using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    class BusinessLayer:IClicBouton
    {

        private readonly ILoadImage loader;
        private readonly ISaveImage saver;
        private readonly IClicBouton clicker;
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        //Image to be saved at the end of the process
        private Bitmap resultBitmap = null;



        private Bitmap LoadIOImage(string uri)
        {
            ILoadImage loader = new LoadImage();
            return loader.loadImage(uri);
        }

        private Bitmap SaveIOImage(string uri)
        {
            ISaveImage saver = new SaveImage();
            return saver.saveImage(uri);
        }

      



        //Application of edge detection (mainly original method)
        private Bitmap ApplyEdgeDetection(bool preview)
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
                    resultBitmap = bitmapResultEdge;

                }
            }

            return resultBitmap;
        }

        public Bitmap ImageOpening(OpenFileDialog ofd, PictureBox previewBox)
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                originalBitmap = LoadIOImage(ofd.FileName);


                previewBitmap = originalBitmap.CopyToSquareCanvas(previewBox.Width);


                //EdgeDetection is applied by default
                previewBox.Image = ApplyEdgeDetection(true);



                return (Bitmap)previewBox.Image;
            }

            return null;
        }

        public Bitmap ImageSaving(SaveFileDialog sfd)
        {
           Bitmap savedImage =  ApplyEdgeDetection(false);

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
                //Bitmap savedImage = new Bitmap(resultBitmap);
                savedImage.Save(streamWriter.BaseStream, imgFormat);
                streamWriter.Flush();
                streamWriter.Close();

                return savedImage;
            }
            return null;
        }
    }
}
