using System.Drawing;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    //Interface covering image saving
    public interface ISaveImage
    {
        Bitmap saveImage(string uri, ImageFormat imgFormat, Bitmap savedImage);
    }
}
