using MirasMentalNotes.Resources;
using System.Text.Json;

namespace MirasMentalNotes.Settings
{
    public class FileConfig
    {
        public string? ContentDirectory { get; set; }

        public static FileConfig LoadFromFile() 
        {
            if (!File.Exists(AppSettings.FileConfigPath)) 
                throw new FileNotFoundException(Messages.FileConfigNotFound);

            var configString = File.ReadAllText(AppSettings.FileConfigPath);
            var config = JsonSerializer.Deserialize<FileConfig>(configString);

            if (config == null) throw new JsonException(Messages.FileConfigBadFormat);

            return config;
        }

        public void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var configString = JsonSerializer.Serialize(this, options);

            File.WriteAllText(AppSettings.FileConfigPath, configString);
        }
    }
}
