namespace MirasMentalNotes.Settings
{
    public static class Settings
    {
        public const string FileConfigPath = "Settings/fileConfig.json";
        public static FileConfig FileConfig { get; set; } = new FileConfig();

        public static void Initialize()
        {
            Settings.FileConfig = FileConfig.LoadFromFile();
        }
    }
}
