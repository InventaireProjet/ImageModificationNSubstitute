using System.Drawing;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    //Interface covering relation between presentation and business layers
    public interface IClicBouton
    {
        //Management of click on load Image
        Bitmap ImageOpening(OpenFileDialog ofd, PictureBox previewBox);

        //Management of click on save Image
        Bitmap ImageSaving(SaveFileDialog sfd);
    }
}
