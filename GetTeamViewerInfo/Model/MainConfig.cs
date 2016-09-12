using System;
using System.IO;
using GetTeamViewerInfo.Controller;
using Newtonsoft.Json;

namespace GetTeamViewerInfo.Model
{
    [Serializable]
    public class MainConfig
    {
        //公共调用
        public static MainConfig Config;
        private const string FilePath = "gti-config.json";
        private bool _uploadEnable;
        public static event EventHandler UploadEnableChanges;

        //上报TeamViewer信息开关
        public bool UploadEnable
        {
            get { return _uploadEnable; }
            set
            {
                _uploadEnable = value;
                UploadEnableChanges?.Invoke(this,new EventArgs());
            }
        }


        //气泡信息
        public string BubbleInfo { get; set; }

        //所在店名
        public string Addr { get; set; }

        //上传路径
        public string WebApiUri { get; set; }

        //读取配置
        public static MainConfig Load()
        {
            try
            {
                var configContent = File.ReadAllText(FilePath);
                Config = JsonConvert.DeserializeObject<MainConfig>(configContent);
                if (Config.BubbleInfo.Length >= 64)
                    Config.BubbleInfo = Config.BubbleInfo.Substring(0, 63);
                return Config;
            }
            catch (Exception e)
            {
                Config = new MainConfig
                {
                    UploadEnable = false,
                    BubbleInfo = "GetTeamViewerInfo V0.1 Alpha\nPower By Chedone",
                    Addr = "默认配置",
                    WebApiUri= "http://localhost:51407/api/TeamViewerInfo"
                };
                Save();
                LogController.Error(e);
                return Config;
            }
        }


        //存储配置
        public static void Save()
        {
            if(Config==null)
                return;
            try
            {
                using (var sw = new StreamWriter(File.Open(FilePath, FileMode.Create)))
                {
                    var jsonContent = JsonConvert.SerializeObject(Config, Formatting.Indented);
                    sw.Write(jsonContent);
                    sw.Flush();
                }
            }
            catch (Exception e)
            {
                LogController.Error(e);
            }
        }
    }
}
