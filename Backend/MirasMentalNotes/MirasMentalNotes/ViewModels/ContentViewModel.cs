using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Models
{
    public record ContentViewModel
    {
        public string? FileName { get; set; }
        public string? Content { get; set; }
    
        public static ContentViewModel? FromFileName(string fileName)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory;
            var filePath = Path.Combine(contentDir, fileName);

            if (!File.Exists(filePath)) return null;

            var contentFile = new ContentViewModel();
            contentFile.FileName = fileName;
            contentFile.Content = File.ReadAllText(filePath);

            return contentFile;
        }
    }
}
