using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SFM.Model;
using SFM.ViewModel.Command;
using SFM.ViewModel.Interfaces;

namespace SFM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<string> _disks;
        private ObservableCollection<FileOrFolder> _filesAndFolders;
        private FileOrFolder _selectedFileOrFolder;
        private readonly IFileHelper _fileHelper;
        private string _currentPath;
        private string _selectedDisk;
        private string _info;
        

        public ObservableCollection<string> Disks
        {
            get { return _disks; }
            set
            {
                _disks = value;
                OnPropertyChanged("Disks");
            }
        }

        public ObservableCollection<FileOrFolder> FilesAndFolders
        {
            get { return _filesAndFolders; }
            set
            {
                _filesAndFolders = value;
                OnPropertyChanged("FilesAndFolders");
            }
        }

        public FileOrFolder SelectedFileOrFolder
        {
            get { return _selectedFileOrFolder; }
            set
            {
                _selectedFileOrFolder = value;
                OnPropertyChanged("SelectedFileOrFolder");
                OpenInfo();
            }
        }

        public string CurrentPath
        {
            get { return _currentPath; }
            set
            {
                _currentPath = value;
                OnPropertyChanged("CurrentPath");
            }
        }

        public string SelectedDisk
        { 
            get { return _selectedDisk; }
            set
            {
                _selectedDisk = value;
                OnPropertyChanged("SelectedDisk");
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }

        public ICommand UpdateCommand { get; }

        #endregion Properties

        #region Public Method

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            UpdateCommand = new RelayCommand(UpdateFilesAndFolders);
            OnStartUpConfiguration();
        }

        #endregion Public Method

        #region Private Method

        private void OnStartUpConfiguration()
        {
            FilesAndFolders = new ObservableCollection<FileOrFolder>();
            Disks = new ObservableCollection<string>(DriveInfo.GetDrives().Select(drive => drive.Name));

            //Default value
            SelectedDisk = Disks[0];
        }

        private void UpdateFilesAndFolders()
        {
            string path = Path.Combine(SelectedDisk, CurrentPath ?? string.Empty);

            if (_fileHelper.PathExists(path))
            {
                FilesAndFolders.Clear();
                List<FileOrFolder> filesAndFolders = _fileHelper.GetFilesAndFolders(path, true);

                foreach (var item in filesAndFolders)
                    FilesAndFolders.Add(item);
            }
            else
                MessageBox.Show("There is no such way");
        }

        private void OpenInfo()
        {
            if (SelectedFileOrFolder is null)
                return;

            StringBuilder sb = new StringBuilder();

            if (SelectedFileOrFolder.IsDirectory)
            {
                List<FileOrFolder> filesAndFolders = _fileHelper.GetFilesAndFolders(SelectedFileOrFolder.Path, false);

                foreach (var item in filesAndFolders)
                    sb.AppendLine(item.Name);

                Info = sb.ToString();
                return;
            }

            FileInfo fileInfo = new FileInfo(SelectedFileOrFolder.Path);
            sb.Append("Name: ").Append(SelectedFileOrFolder.Name).AppendLine();
            sb.Append("Extension: ").Append(fileInfo.Extension).AppendLine();
            sb.Append("Size: ").Append(fileInfo.Length / 1024).Append(" KB");

            Info = sb.ToString();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Private Method
    }
}
