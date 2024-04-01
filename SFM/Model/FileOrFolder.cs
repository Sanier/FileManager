namespace SFM.Model
{
    public class FileOrFolder
    {
        public required string Name { get; set; }

        public required string Path { get; set; }

        public bool IsDirectory { get; set; }
    }
}
