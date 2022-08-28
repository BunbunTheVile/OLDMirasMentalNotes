using MirasMentalNotes.Models;
using MirasMentalNotes.Resources;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Helpers
{
    public static class NoteHelper
    {
        public static string fullPath(string noteName) =>
            Path.Combine(AppSettings.FileConfig.ContentDirectory, noteName) + ".note";

        public static bool WriteNoteToFile(Note note)
        {
            var path = fullPath(note.Name!);
            if (!File.Exists(path)) return false;

            var text = "";
            text += GetTagText(note);
            text += "\n\n";
            text += note.Content!;

            File.WriteAllText(path, text);

            return true;
        }

        private static string GetTagText(Note note)
        {
            var tagText = StringConstants.tagsStart;
            note.Tags.ForEach(x => tagText += $"{x} ");
            tagText = tagText.Trim();
            tagText += StringConstants.tagsEnd;
            return tagText;
        }
    }
}
