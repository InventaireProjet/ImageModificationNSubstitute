using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    //Interface covering image saving
    public interface ISaveImage
    {
        Bitmap saveImage(string uri, ImageFormat imgFormat, Bitmap savedImage);
    }
}
