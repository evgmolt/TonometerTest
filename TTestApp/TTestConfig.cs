using Newtonsoft.Json;

namespace TTestApp
{
    internal class TTestConfig
    {
        private const string _configFileName = "ttconfig.json";
        public string DataDir { get; set; }
        public int DataFileNum { get; set; }
        public string Prefix { get; set; }
        public bool Maximized { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public int SmoothWindowSize { get; set; }
        public int MedianWindowSize { get; set; }
        public decimal CoeffLeft { get; set; }
        public decimal CoeffRight { get; set; }
        public TTestConfig()
        {
            DataDir = Directory.GetCurrentDirectory() + @"\Data\";
            if (!Directory.Exists(DataDir))
            {
                Directory.CreateDirectory(DataDir);
            }
            DataFileNum = 0;
            Prefix = "A";
            Maximized = false;
            WindowWidth = 800;
            WindowHeight = 500;
            SmoothWindowSize = 10;
            MedianWindowSize = 10;
            CoeffLeft = 0.4m;
            CoeffRight = 0.8m;
        }

        public static TTestConfig GetConfig()
        {
            JsonSerializer serializer = new();
            try
            {
                using (StreamReader sr = new(_configFileName))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return (TTestConfig)serializer.Deserialize(reader, typeof(TTestConfig));
                }
            }
            catch (Exception)
            {
                TTestConfig tTestConfig = new();
                SaveConfig(tTestConfig);
                return tTestConfig;
            }
        }

        public static void SaveConfig(TTestConfig cfg)
        {
            StreamWriter sw = new(_configFileName);
            JsonWriter writer = new JsonTextWriter(sw);
            JsonSerializer serializer = new();
            serializer.Serialize(writer, cfg);
            writer.Close();
            sw.Close();         
        }
    }
}
