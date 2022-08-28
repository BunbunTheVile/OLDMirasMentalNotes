using MirasMentalNotes.Resources;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Models
{
    public record Note
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public void WriteToFile()
        {

        }

        public static Note? FromNoteName(string noteName)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory;
            var filePath = Path.Combine(contentDir, noteName) + ".note";

            if (!File.Exists(filePath)) return null;

            var note = new Note();
            note.Name = noteName;

            var text = File.ReadAllText(filePath);
            note.SetTags(text);
            note.SetContent(text);

            return note;
        }

        private void SetTags(string text)
        {
            var tagsStart = StringConstants.tagsStart;
            var tagsEnd = StringConstants.tagsEnd;

            if (text.Contains(tagsStart) && text.Contains(tagsEnd))
            {
                var tagText = text.Split(tagsStart)[1].Split(tagsEnd)[0];
                var tags = tagText.Split(" ");
                this.Tags = tags.ToList();
            }
        }

        private void SetContent(string text)
        {
            var tagsStart = StringConstants.tagsStart;
            var tagsEnd = StringConstants.tagsEnd;

            if (text.Contains(tagsStart) && text.Contains(tagsEnd))
            {
                var startIndex = text.IndexOf(tagsStart);
                var endIndex = text.IndexOf(tagsEnd);
                var tagText = text.Substring(startIndex, endIndex - startIndex + tagsEnd.Length);
                Console.WriteLine(tagText);
                text = text.Replace(tagText, "");
            }

            this.Content = text.Trim();
        }
    }
}
