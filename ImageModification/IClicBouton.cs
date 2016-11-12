using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
   public interface IClicBouton
    {
        Bitmap ImageOpening(OpenFileDialog ofd, PictureBox previewBox);

        Bitmap ImageSaving(SaveFileDialog sfd);
    }
}
