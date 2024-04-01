using System.IO;
using System.Windows;
using SFM.Model;
using SFM.ViewModel.Interfaces;

namespace SFM.ViewModel.Helper
{
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// Checking for path availability
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Return bool depending on the condition</returns>
        public bool PathExists(string path)
        {
            try
            {
                return Directory.Exists(path);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error: Insufficient rights to access path " + path);
                return false;
            }
        }

        /// <summary>
        /// Retrieves a list of files and folders from the specified path.
        /// </summary>
        /// <param name="path">The path to the directory from which to retrieve files and folders.</param>
        /// <param name="withExtension">If true, file names will include the extension. If false, file names will be without the extension.</param>
        /// <returns>Returns a list of FileOrFolder objects representing the files and folders in the specified directory. If an access error occurs, it returns an empty list.</returns>
        public List<FileOrFolder> GetFilesAndFolders(string path, bool withExtension)
        {
            try
            {
                if (!PathExists(path))
                {
                    MessageBox.Show("There is no such way");
                    return [];
                }
                    
                List<FileOrFolder> filesAndFolders = new List<FileOrFolder>();
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                    filesAndFolders.Add(new FileOrFolder 
                    {   Name = directory.Name, 
                        Path = directory.FullName, 
                        IsDirectory = true 
                    });

                foreach (FileInfo file in directoryInfo.GetFiles())
                    filesAndFolders.Add(new FileOrFolder
                    {
                        Name = withExtension ? Path.GetFileNameWithoutExtension(file.Name) : file.Name,
                        Path = file.FullName,
                        IsDirectory = false
                    });

                return filesAndFolders;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error: Insufficient rights to access path " + path);
                return [];
            }
        }
    }
}
