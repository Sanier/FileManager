using SFM.ViewModel.Helper;
using SFM.ViewModel.Interfaces;

namespace SFM.Tests
{
    public class FileHelperTests
    {
        private IFileHelper _fileHelper;

        public FileHelperTests()
        {
            _fileHelper = new FileHelper();
        }

        [Fact]
        public void PathExists_ReturnsTrue_WhenDirectoryExists()
        {
            // Arrange
            var path = "C:\\Program Files";

            // Act
            var result = _fileHelper.PathExists(path);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void PathExists_ReturnsFalse_WhenDirectoryDoesNotExist()
        {
            // Arrange
            var path = "C:\\nonexistentdirectory";

            // Act
            var result = _fileHelper.PathExists(path);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetFilesAndFolders_ReturnsEmptyList_WhenDirectoryDoesNotExist()
        {
            // Arrange
            var path = "C:\\nonexistentdirectory";

            // Act
            var result = _fileHelper.GetFilesAndFolders(path, true);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetFilesAndFolders_ReturnsFilesAndFolders_WhenDirectoryExists()
        {
            // Arrange
            var path = "C:\\Program Files";

            // Act
            var result = _fileHelper.GetFilesAndFolders(path, true);

            // Assert
            Assert.NotEmpty(result);
        }

    }
}
