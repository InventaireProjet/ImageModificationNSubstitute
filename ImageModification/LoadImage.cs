﻿using System.Drawing;
using System.IO;

namespace ImageEdgeDetection
{
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
