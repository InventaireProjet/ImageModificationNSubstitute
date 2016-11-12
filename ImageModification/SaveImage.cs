using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    class SaveImage : ISaveImage
    {
        public Bitmap saveImage(string uri)
        {
            Bitmap savedImage = null;

            string fileExtension = Path.GetExtension(uri).ToUpper();
            ImageFormat imgFormat = ImageFormat.Png;

            if (fileExtension == "BMP")
            {
                imgFormat = ImageFormat.Bmp;
            }
            else if (fileExtension == "JPG")
            {
                imgFormat = ImageFormat.Jpeg;
            }

            StreamWriter streamWriter = new StreamWriter(uri, false);
            //Line added to avoid an error : http://stackoverflow.com/questions/15571022/how-to-find-reason-for-generic-gdi-error-when-saving-an-image
           // Bitmap savedImage = new Bitmap(resultBitmap);
            savedImage.Save(streamWriter.BaseStream, imgFormat);
            streamWriter.Flush();
            streamWriter.Close();
            return savedImage;
        }
    }
}
