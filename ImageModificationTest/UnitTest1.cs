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
        public void TestMethod1()
        {
            Bitmap imgOrigin = Resource1.testBeforeFilter;

            Bitmap imgResult = ExtBitmap.ZenFilter(imgOrigin);

            Assert.AreEqual(imgOrigin.Size, imgResult.Size);

        }
    }
}
