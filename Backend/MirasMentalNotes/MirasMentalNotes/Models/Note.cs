using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Models
{
    public record Note
    {
        public string? File { get; set; }
        public string? Content { get; set; }
    
        public static Note? FromFileName(string fileName)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory;
            var filePath = Path.Combine(contentDir, fileName);

            if (!System.IO.File.Exists(filePath)) return null;

            var contentFile = new Note();
            contentFile.File = fileName;
            contentFile.Content = System.IO.File.ReadAllText(filePath);

            return contentFile;
        }
    }
}
