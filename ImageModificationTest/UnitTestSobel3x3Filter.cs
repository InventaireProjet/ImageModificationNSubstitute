using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageEdgeDetection;

namespace ImageModificationTest
{
    [TestClass]
    public class UnitTestSobel3x3Filter
    {
        [TestMethod]
        public void TestSizeImageSobel()
        {

            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.Sobel3x3Filter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }

        [TestMethod]
        //Tests that the image output corresponds to what is expected in terms of color by testing all the pixels
        public void TestColorPixelSobel()
        {
            Bitmap imgTest = Resource1.InTheArmySobel;
            Bitmap imgResult = ExtBitmap.Sobel3x3Filter(Resource1.InTheArmyOriginal, false);
            for (int i = 0; i < imgTest.Width; i++)
            {
                for (int j = 0; j < imgTest.Height; j++)
                {
                    Color pixelRef = imgTest.GetPixel(i, j);
                    Color pixelRes = imgResult.GetPixel(i, j);

                    Assert.AreEqual(pixelRef, pixelRes);
                }

            }

        }


        [TestMethod]
        //Tests that the method returns null if a null image is used
        public void TestEmptyImageSobel()
        {
            Bitmap imgNull = null;

            Assert.IsNull(ExtBitmap.Sobel3x3Filter(imgNull));

        }

    }
}
