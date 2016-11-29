using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playlists.NET.Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void MakeAbsolutePath_Equal()
        {
            string folderPath = @"D:\Muzyka\Vanessa Mee";
            string filePath = "Contradanza.mp3";
            string expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            string path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\";
            filePath = "Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee";
            filePath = @".\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\";
            filePath = @".\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee";
            filePath = @"..\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\";
            filePath = @"..\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka";
            filePath = @".\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\";
            filePath = @"Muzyka\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);
        }

        [TestMethod]
        public void MakeAbsolutePath_NotEqual()
        {
            string folderPath = @"D:\Muzyka\Vanessa Mee";
            string filePath = @"\Contradanza.mp3";
            string expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            string path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreNotEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\";
            filePath = @"\Contradanza.mp3";
            expectedPath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeAbsolutePath(folderPath, filePath);
            Assert.AreNotEqual(path, expectedPath);
        }
    }
}
