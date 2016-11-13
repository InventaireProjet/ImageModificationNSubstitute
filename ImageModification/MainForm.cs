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
    //It corresponds to the PRESENTATION LAYER
    public partial class MainForm : Form
    {
        //Initializing the IClickButton interface to communicate with the business layer
        IClickButton clicker = new BusinessLayer(new LoadImage(), new SaveImage());

        public MainForm()
        {
            InitializeComponent();

            // Saving a new image is invisible when no image is loaded
            btnSaveNewImage.Visible = false;
        }


        /*Button for loading image, only the visual components are managed here,
         * the business part is delegated to the business layer */
        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            //Dialog box display
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image file.";
            openFileDialog.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            openFileDialog.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            /*If ok is clicked, the business layer is called, 
             which sends back the image to put in the preview box*/
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (clicker.ImageOpening(openFileDialog, picPreview) != null)
                {
                    picPreview.Image = clicker.ImageOpening(openFileDialog, picPreview);

                    //Since there is an image, it is possible to save it
                    btnSaveNewImage.Visible = true;
                }
            }
        }


        /*Button for saving image , only the visual components are managed here,
         * the business part is delegated to the business layer*/
        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            //Dialog box display
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Specify a file name and file path";
            saveFileDialog.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            saveFileDialog.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            //The rest is done by the business layer, if ok is clicked
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                clicker.ImageSaving(saveFileDialog);
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
