using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageEdgeDetection;

 namespace ImageModificationTest
{
    [TestClass()]
    public class BusinessLayerTests
    {

        [TestMethod]
        public void CalledWithAnExistingUser()
        {

        }


        [TestMethod]
        public void TestSizeImageKirsch()
        {

            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.KirschFilter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }

        [TestMethod]
        //Tests that the image output corresponds to what is expected in terms of color by testing all the pixels
        public void TestColorPixelKirsch()
        {
            Bitmap imgTest = Resource1.InTheArmyKirsch;
            Bitmap imgResult = ExtBitmap.KirschFilter(Resource1.InTheArmyOriginal);
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
        public void TestEmptyImageKirsch()
        {
            Bitmap imgNull = null;

            Assert.IsNull(ExtBitmap.KirschFilter(imgNull));

        }
    }
}