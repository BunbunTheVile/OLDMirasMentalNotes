using System.Text.Json;

namespace MirasMentalNotes.Settings
{
    public class FileConfig
    {
        public string? ContentDirectory { get; set; }

        public static FileConfig? LoadFromFile() 
        {
            if (!File.Exists(Settings.FileConfigPath)) return null;

            var configString = File.ReadAllText(Settings.FileConfigPath);
            return JsonSerializer.Deserialize<FileConfig>(configString);
        }

        public void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var configString = JsonSerializer.Serialize(this, options);

            File.WriteAllText(Settings.FileConfigPath, configString);
        }
    }
}
