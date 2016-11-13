using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageEdgeDetection
{
    // I/O LAYER
    class SaveImage : ISaveImage
    {
        //Implementation of the interface ISaveImage, only streamWriter
        public void saveImage(string uri, ImageFormat imgFormat, Bitmap savedImage)
        {
            StreamWriter streamWriter = new StreamWriter(uri, false);
            savedImage.Save(streamWriter.BaseStream, imgFormat);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
