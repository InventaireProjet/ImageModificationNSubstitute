/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
 * It was further modified by Zappellaz Nancy & Mabillard Julien
*/
using System;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;


namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {

        IClicBouton clicker = new BusinessLayer();

        public MainForm()
        {
            InitializeComponent();


            // Saving a new image is invisible when no image is loaded
            btnSaveNewImage.Visible = false;
        }


        //Button for loading image 
        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            picPreview.Image = clicker.ImageOpening(ofd, picPreview);

            //Since there is an image, it is possible to save it
            btnSaveNewImage.Visible = true;

        }


        //Button for saving image (original method)
        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            clicker.ImageSaving(sfd);
               
        }





        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
