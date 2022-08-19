namespace MirasMentalNotes.Settings
{
    public static class AppSettings
    {
        public const string FileConfigPath = "Settings/fileConfig.json";
        public static FileConfig FileConfig { get; set; } = new FileConfig();

        public static void Initialize()
        {
            AppSettings.FileConfig = FileConfig.LoadFromFile();
        }
    }
}
