using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    internal class TTestConfig
    {
        private const string _configFileName = "ttconfig.json";
        public string DataDir { get; set; }
        public bool FilterOn { get; set; }
        public bool Maximized { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public int SmoothWindowSize { get; set; }
        public int MedianWindowSize { get; set; }


        public TTestConfig()
        {
            DataDir = Directory.GetCurrentDirectory() + @"\Data\";
            FilterOn = false;
            Maximized = false;
            WindowWidth = 800;
            WindowHeight = 500;
            SmoothWindowSize = 10;
            MedianWindowSize = 10;
        }

        public static TTestConfig GetConfig()
        {
            TTestConfig tTestConfig;
            JsonSerializer serializer = new();
            try
            {
                using (StreamReader sr = new(_configFileName))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    tTestConfig = (TTestConfig)serializer.Deserialize(reader, typeof(TTestConfig));
                    return tTestConfig;
                }
            }
            catch (Exception)
            {
                tTestConfig = new TTestConfig();
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
