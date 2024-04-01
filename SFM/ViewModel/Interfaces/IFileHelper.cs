using SFM.Model;

namespace SFM.ViewModel.Interfaces
{
    public interface IFileHelper
    {
        bool PathExists(string path);

        List<FileOrFolder> GetFilesAndFolders(string path, bool withExtension);
    }
}
