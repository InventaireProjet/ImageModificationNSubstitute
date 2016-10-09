using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEdgeDetection;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace ImageModificationTest
{
    [TestClass]
    public class UnitTestSwapFilter
    {
        [TestMethod]
        public void TestSizeImageSwap()
        {

            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.SwapFilter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }

         [TestMethod]
         //Tests that the image output corresponds to what is expected in terms of color by testing all the pixels
        public void TestColorPixelSwap()
        {
            Bitmap imgTest = Resource1.filterTest;
            Bitmap imgResult = ExtBitmap.SwapFilter(Resource1.testBeforeFilter);
            for (int i = 0; i< imgTest.Width; i++)
            {
                for (int j = 0; j< imgTest.Height; j++)
                {
                    Color pixelRef = imgTest.GetPixel(i, j);
                    Color pixelRes = imgResult.GetPixel(i, j);

                    Assert.AreEqual(pixelRef, pixelRes);
                }
              
            }

        }


        [TestMethod]
        //Tests that the method returns null if a null image is used
        public void TestEmptyImageSwap()
        {
            Bitmap imgNull = null; 

            Assert.IsNull(ExtBitmap.SwapFilter(imgNull));

        }

        [TestMethod]
        public void TestWrongFormatSwap()
        {
            Image corruptImage = Image.FromFile("C:\\Users\\uadmin\\Desktop\\serveimage.jpg");

            Bitmap bitmapImage = new Bitmap(corruptImage);

            Assert.IsNull(ExtBitmap.SwapFilter(bitmapImage));

        }
    }
}
