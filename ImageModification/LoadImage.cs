using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    // I/O LAYER
    class LoadImage:ILoadImage
    {

        //Implementation of the interface ILoadImage, only streamReader
        public Bitmap loadImage(string uri)
        {
            StreamReader streamReader = new StreamReader(uri);
            Bitmap image = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            streamReader.Close();
            return image;
        }

    }
}
