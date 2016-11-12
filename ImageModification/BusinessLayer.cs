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
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        //Image to be saved at the end of the process
        private Bitmap resultBitmap = null;
      
        //Application of edge detection (mainly original method)
        private Bitmap ApplyEdgeDetection(bool preview)
        {
            Bitmap imageForEdgeDetection = null;

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
            resultBitmap = imageForEdgeDetection.KirschFilter();

           
            return resultBitmap;
        }

        //Manage image loading and returns the preview
        public Bitmap ImageOpening(OpenFileDialog ofd, PictureBox previewBox)
        {
            //If ok is clicked in the presentation layer, loading starts
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 originalBitmap=null;

                //I/O part called through the interface managing the loading
                ILoadImage loader = new LoadImage();
                originalBitmap = loader.loadImage(ofd.FileName);

                //Creation of the preview bitmap
                previewBitmap = originalBitmap.CopyToSquareCanvas(previewBox.Width);


                //EdgeDetection is applied and stored in the previewBox.Image variable
                previewBox.Image = ApplyEdgeDetection(true);
               

                return (Bitmap)previewBox.Image;
            }

            return null;
        }

        //Manage image saving and returns the image saved
        public Bitmap ImageSaving(SaveFileDialog sfd)
        {
            //EdgeDetection is applied, the result is the image to save 
            Bitmap savedImage =  ApplyEdgeDetection(false);

            //If ok is clicked in the presentation layer, saving starts
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

                //I/O part called through the interface managing the saving
                ISaveImage saver = new SaveImage();
                saver.saveImage(sfd.FileName, imgFormat, savedImage);


                return savedImage;
            }
            return null;
        }
    }
}
