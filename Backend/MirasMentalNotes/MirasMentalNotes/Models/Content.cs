using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Models
{
    public record Content
    {
        public string? FileName { get; set; }
        public string? FileContent { get; set; }
    
        public static Content? FromFileName(string fileName)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory;
            var filePath = Path.Combine(contentDir, fileName);

            if (!File.Exists(filePath)) return null;

            var contentFile = new Content();
            contentFile.FileName = fileName;
            contentFile.FileContent = File.ReadAllText(filePath);

            return contentFile;
        }
    }
}
