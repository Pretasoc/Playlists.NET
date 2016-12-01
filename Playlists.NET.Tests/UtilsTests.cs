using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaylistsNET.Tests
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

        [TestMethod]
        public void MakeRelativePath_Equal()
        {
            string folderPath = @"D:\Muzyka\Vanessa Mee";
            string filePath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            string expectedPath = @"Contradanza.mp3";
            string path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\";
            filePath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka";
            filePath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"Vanessa Mee\Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\Folder1";
            filePath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"..\Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\Folder1\Folder2";
            filePath = @"D:\Muzyka\Vanessa Mee\Contradanza.mp3";
            expectedPath = @"..\..\Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee";
            filePath = @"D:\Muzyka\Other\Contradanza.mp3";
            expectedPath = @"..\Other\Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);

            folderPath = @"D:\Muzyka\Vanessa Mee\Folder1";
            filePath = @"D:\Muzyka\Other1\Other2\Contradanza.mp3";
            expectedPath = @"..\..\Other1\Other2\Contradanza.mp3";
            path = Utils.Utils.MakeRelativePath(folderPath, filePath);
            Assert.AreEqual(path, expectedPath);
        }
    }
}
