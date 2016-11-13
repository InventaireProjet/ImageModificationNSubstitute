﻿using System.Drawing;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    //Interface covering relation between presentation and business layers
    public interface IClickButton
    {
        //Management of click on load Image
        Bitmap ImageOpening(OpenFileDialog ofd, PictureBox previewBox);

        //Management of click on save Image
        void ImageSaving(SaveFileDialog sfd);
    }
}
