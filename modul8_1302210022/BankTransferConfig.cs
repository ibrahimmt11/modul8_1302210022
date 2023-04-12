using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace modul8_1302210022
{
    public class BankTransferConfig
    {
        public Config config { get; set; }
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public string configFilename = "bank_transfer_config.json";

        public BankTransferConfig()
        {
            try
            {
                ReadConfig();
            }
            catch
            {
                SetDefault();
                WrittenConfig();
            }
        }

        private Config ReadConfig()
        {
            string jsonFromFile = File.ReadAllText(path + '/' + configFilename);
            config = JsonSerializer.Deserialize<Config>(jsonFromFile);
            return config;
        }

        private void WrittenConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(config, options);
            string fullpath = path + "/" + configFilename;
            File.WriteAllText(fullpath, jsonString);
        }

        public void SetDefault()
        {
            config = new Config("en", 25000000, 6500, 15000, ["RTO (real-time)", "SKN", "RTGS", "BI FAST"], "yes", "ya");
        }

        public void UbahBahasa()
        {
            config.lang = config.lang == "en" ? "id" : "en";
        }
        
        public void Fee()
        {
            int uang = 0;
            if (uang <= 25000000)
            {
                Console.WriteLine("Transfer fee: " + config.low_fee);
            }
            else
            {
                Console.WriteLine("Transfer fee: " + config.high_fee);
            }
        }
    }



    public class Config
    {
        public string lang { get; set; }
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
        public string methods { get; set; }
        public string en { get; set; }
        public string id { get; set; }

        public Config() { }

        public Config(string lang, int threshold, int low_fee, int high_fee, string method, string en, string id)
        {
            this.lang = lang;
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
            this.methods = method;
            this.en = en;
            this.id = id;
        }
    }
}
