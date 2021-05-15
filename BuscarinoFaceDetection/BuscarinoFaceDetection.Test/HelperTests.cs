using System.IO.Abstractions;
using BuscarinoFaceDetection.Services;
using Moq;
using Xunit;

namespace BuscarinoFaceDetection.Test
{
    public class HelperTests
    {
        readonly Mock<IFileSystem> _fileSystem;

        public HelperTests()
        {
            _fileSystem = new Mock<IFileSystem>();
            _fileSystem.Setup(f => f.Directory.CreateDirectory(It.IsAny<string>())).Verifiable();
            _fileSystem.Setup(f => f.File.Delete(It.IsAny<string>())).Verifiable();
            _fileSystem.Setup(f => f.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
        }

        [Fact]
        public void GetImageFilePaths_PathInvalid()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>())).Returns(false);
            var helper = new HelperServices(_fileSystem.Object);
            var result = helper.GetImageFilePaths(@"c:\temp");

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(new object[] {new[] { "file1.jpg", "file2.jpg" }})]
        [InlineData(new object[] { new[] { "file1.jpg"} })]
        public void GetImageFilePaths_PathValid(string[] fileArray)
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>())).Returns(true);
            _fileSystem.Setup(f => f.Directory.GetFiles(It.IsAny<string>())).Returns((string _) =>
                fileArray);

            var helper = new HelperServices(_fileSystem.Object);
            var result = helper.GetImageFilePaths(@"c:\temp");

            Assert.NotNull(result);
            Assert.Equal(fileArray.Length, result.Length);
            foreach (var file in fileArray)
            {
                Assert.Contains(file, result);
                Assert.Contains(file, result);
            }
        }

        [Fact]
        public void VerifyDirectory_DoesNotExist()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>())).Returns(false);
            var helper = new HelperServices(_fileSystem.Object);
            var filePath = @"c:\temp";
            var result = helper.VerifyDirectory(filePath);

            Assert.NotEmpty(result);
            Assert.Equal(filePath, result);
        }

        [Fact]
        public void VerifyDirectory_DoesExist()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>())).Returns(true);
            var helper = new HelperServices(_fileSystem.Object);
            var filePath = @"c:\temp";
            var result = helper.VerifyDirectory(filePath);

            Assert.NotEmpty(result);
            Assert.Equal(filePath, result);
        }

       
    }
}
