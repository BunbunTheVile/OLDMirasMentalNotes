namespace MirasMentalNotes.Settings
{
    public static class AppSettings
    {
        public const string FileConfigPath = "Settings/fileConfig.json";
        public static FileConfig FileConfig { get; set; } = new FileConfig
        {
            ContentDirectory = "DefaultContent"
        };

        public static void Initialize()
        {
            FileConfig = FileConfig.LoadFromFile();
            if (!Directory.Exists(FileConfig.ContentDirectory))
            {
                Directory.CreateDirectory(FileConfig.ContentDirectory);
            }
        }
    }
}
