using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Models
{
    public record Note
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    
        public static Note? FromNoteName(string noteName)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory;
            var filePath = Path.Combine(contentDir, noteName) + ".note";

            if (!File.Exists(filePath)) return null;

            var contentFile = new Note();
            contentFile.Name = noteName;
            contentFile.Content = File.ReadAllText(filePath);

            return contentFile;
        }
    }
}
