using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    public interface ISaveImage
    {
        Bitmap saveImage(string uri);
    }
}
