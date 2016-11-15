using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    public class BusinessLayer : IClickButton
    {
        private readonly ILoadImage loader;
        private readonly ISaveImage saver;
        private Bitmap originalBitmap = null;
        //Image to be saved at the end of the process
        private Bitmap resultBitmap = null;

        //Constructor charging the two I/O interfaces
        public BusinessLayer(ILoadImage loader, ISaveImage saver)
        {
            this.loader = loader;
            this.saver = saver;
        }


        //Application of edge detection (mainly original method)
        public Bitmap ApplyEdgeDetection()
        {
                        
            //Apply the edgeDetection 
            resultBitmap = originalBitmap.KirschFilter();

            return resultBitmap;
        }

        //Manage image loading and returns the preview, called by a presentation layer
        public Bitmap ImageOpening(OpenFileDialog openFileDialog, PictureBox previewBox)
        {
            originalBitmap = null;

            //I/O part called through the interface managing the loading
            try
            {
                originalBitmap = loader.loadImage(openFileDialog.FileName);
            }
            catch (ArgumentException)
            {
                return null;
            }
                        
            //EdgeDetection is applied and stored in the previewBox.Image variable
            previewBox.Image = ApplyEdgeDetection();


            return (Bitmap)previewBox.Image;
        }


        //Manage image saving and returns the image saved, called by a presentation layer
        public void ImageSaving(SaveFileDialog saveFileDialog)
        {
            //EdgeDetection is applied, the result is the image to save 
            Bitmap savedImage = ApplyEdgeDetection();

            //Setting the image format
            string fileExtension = Path.GetExtension(saveFileDialog.FileName).ToUpper();
            ImageFormat imgFormat = ImageFormat.Png;

            if (fileExtension == ".BMP")
            {
                imgFormat = ImageFormat.Bmp;
            }
            else if (fileExtension == ".JPG")
            {
                imgFormat = ImageFormat.Jpeg;
            }

            //I/O part called through the interface managing the saving
            try
            {
                saver.saveImage(saveFileDialog.FileName, imgFormat, savedImage);
            }
            catch
            {
                saveFileDialog.FileName = "empty";
            }

        }
    }
}
