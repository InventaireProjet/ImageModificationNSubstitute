using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageEdgeDetection;
using NSubstitute;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageModificationTest
{
    [TestClass()]
    public class BusinessLayerTests
    {
        //Fake OpenFileDialog to test the business layer
        public static OpenFileDialog CreateOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "fake";
            return openFileDialog;
        }


        //Fake SaveFileDialog to test the business layer
        public static SaveFileDialog CreateSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "fake.bmp";
            return saveFileDialog;
        }


        //Fake PictureBox to test the business layer
        public static PictureBox CreatePictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            //Width
            pictureBox.Width = 159;
            return pictureBox;
        }


        //Test the ImageOpening method that return Bitmap class with the help of a substitute interface
        [TestMethod]
        public void ImageOpeningTest()
        {
            //Create substitutes for the Interfaces
            var loaderSubstitute = Substitute.For<ILoadImage>();
            var saverSubstitute = Substitute.For<ISaveImage>();

            //Create the BusinnessLayer class with the substitutes
            BusinessLayer testBusinessLayer = new BusinessLayer(loaderSubstitute, saverSubstitute);

            //Because the image opening modify the image, we have to use the original and modified images
            Bitmap originalImage = Resource1.InTheArmyOriginal;
            Bitmap modifiedImage = Resource1.InTheArmyKirsch;

            //Fake presentation layer elements are used
            OpenFileDialog fakeOpenFileDialog = CreateOpenFileDialog();
            PictureBox fakePictureBox = CreatePictureBox();

            //Substitute always send the originalImage when the loadImage from the interface is called
            loaderSubstitute.loadImage(Arg.Any<string>()).Returns(originalImage);

            //Execution of ImageOpening
            Bitmap loadedImage = testBusinessLayer.ImageOpening(fakeOpenFileDialog, fakePictureBox);

            //Test that the result is correct
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    Color pixelLoadedImage = modifiedImage.GetPixel(i, j);
                    Color pixelOriginalImage = loadedImage.GetPixel(i, j);

                    Assert.AreEqual(pixelLoadedImage, pixelOriginalImage);
                }
            }
        }


        //Test the ImageOpening method with a corrupted image, it should returns null
        [TestMethod]
        public void ImageOpeningCorruptedTest()
        {
            //Create substitutes for the Interfaces
            var loaderSubstitute = Substitute.For<ILoadImage>();
            var saverSubstitute = Substitute.For<ISaveImage>();

            //Create the BusinnessLayer class with the substitutes
            BusinessLayer testBusinessLayer = new BusinessLayer(loaderSubstitute, saverSubstitute);


            //Fake presentation layer elements are used
            OpenFileDialog fakeOpenFileDialog = CreateOpenFileDialog();
            PictureBox fakePictureBox = CreatePictureBox();

            //Substitute simulates an argument exception
            loaderSubstitute.When(x => x.loadImage(Arg.Any<string>()))
                            .Do(x => { throw new ArgumentException("Image corrupted"); });

            //Execution of ImageOpening
            Bitmap loadedImage = testBusinessLayer.ImageOpening(fakeOpenFileDialog, fakePictureBox);

            Assert.IsNull(loadedImage);
        }


        //Test the exception raising in ImageSaving
        [TestMethod]
        public void ImageSavingFailedTest()
        {
            //Create substitutes for the Interfaces
            var loaderSubstitute = Substitute.For<ILoadImage>();
            var saverSubstitute = Substitute.For<ISaveImage>();

            //Create the BusinnessLayer class with the substitutes
            BusinessLayer testBusinessLayer = new BusinessLayer(loaderSubstitute, saverSubstitute);

            //Fake presentation layer element is used, it is useful to test the result
            SaveFileDialog fakeSaveFileDialog = CreateSaveFileDialog();

            //Interface substitute throw an exception
            saverSubstitute.When(x => x.saveImage(Arg.Any<string>(), Arg.Any<ImageFormat>(), Arg.Any<Bitmap>()))
                            .Do(x => { throw new Exception("Failed"); });

            //Execution of the method
            testBusinessLayer.ImageSaving(fakeSaveFileDialog);

            //Test that the exception has been thrown through the fakeSaveFileDialog FileName
            Assert.AreEqual(fakeSaveFileDialog.FileName, "empty");
        }


        //Test the normal process of ImageSaving
        [TestMethod]
        public void ImageSavingOKTest()
        {
            //Create substitutes for the Interfaces
            var loaderSubstitute = Substitute.For<ILoadImage>();
            var saverSubstitute = Substitute.For<ISaveImage>();

            //Create the BusinnessLayer class with the substitutes
            BusinessLayer testBusinessLayer = new BusinessLayer(loaderSubstitute, saverSubstitute);

            //Fake presentation layer element is used, it is useful to test the result
            SaveFileDialog fakeSaveFileDialog = CreateSaveFileDialog();

            //Change the file name to cover all the code
            fakeSaveFileDialog.FileName = "fake.jpg";

            //Activation of the substitute
            saverSubstitute.saveImage(Arg.Any<string>(), Arg.Any<ImageFormat>(), Arg.Any<Bitmap>());


            //Execution of the method
            testBusinessLayer.ImageSaving(fakeSaveFileDialog);


            //Test that the fakeSaveFileDialog FileName is correct, no exception thrown 
            Assert.AreEqual(fakeSaveFileDialog.FileName, "fake.jpg");
        }


        //Small internal test in the business layer
        [TestMethod]
        public void TestSizeImageKirsch()
        {

            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.KirschFilter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }

    

        //Small internal test in the business layer       
        [TestMethod]
        //Tests that the method returns null if a null image is used
        public void TestEmptyImageKirsch()
        {
            Bitmap imgNull = null;

            Assert.IsNull(ExtBitmap.KirschFilter(imgNull));
        }
    }
}