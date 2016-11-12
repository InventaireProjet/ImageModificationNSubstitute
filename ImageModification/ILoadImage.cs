using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    public interface ILoadImage
    {
       Bitmap loadImage(string uri);
    }
}
