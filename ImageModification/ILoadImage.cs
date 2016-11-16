using System.Drawing;

namespace ImageEdgeDetection
{
    //Interface covering image loading
    public interface ILoadImage
    {
        Bitmap loadImage(string uri);
    }
}
