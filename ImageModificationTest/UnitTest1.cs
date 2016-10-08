using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEdgeDetection;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace ImageModificationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSizeImage()
        {

            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.SwapFilter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }

         [TestMethod]
        public void TestColorPixel()
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
        public void TestEmptyImage()
        {
            Bitmap imgNull = null; 

            Assert.IsNull(ExtBitmap.SwapFilter(imgNull));

        }

        [TestMethod]
        public void TestWrongFormat()
        {
            Image corruptImage = Image.FromFile("C:\\Users\\Chacha\\Downloads\\corruptImg.jpg");

            Bitmap bitmapImage = new Bitmap(corruptImage);

            Assert.IsNull(ExtBitmap.SwapFilter(bitmapImage));

        }
    }
}
