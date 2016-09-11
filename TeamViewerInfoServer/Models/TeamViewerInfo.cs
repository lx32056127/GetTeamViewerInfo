using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TeamViewerInfoServer.Controller;

namespace TeamViewerInfoServer.Models
{
    public class TeamViewerCommand
    {
        private const string FilePath = "/upload.json";

        public static List<TeamViewerInfo> Load()
        {
            try
            {
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath("/") + FilePath))
                    return null;
                var configContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/") + FilePath);
                var tviList = JsonConvert.DeserializeObject<List<TeamViewerInfo>>(configContent);
                return tviList;
            }
            catch (Exception e)
            {
                LogController.Error(e);
                return null;
            }
        }

        public static void Add(string id,string pwd,string addr,string ipaddr)
        {
            var tviList = Load();
            if (tviList == null)
                tviList = new List<TeamViewerInfo>();
            tviList.Add(new TeamViewerInfo()
            {
                TeamViewerID=id,
                TeamViewerPwd=pwd,
                TeamViewOnAddress=addr,
                IpAddress = ipaddr,
                LastUploadTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            using (var sw = new StreamWriter(File.Open(System.Web.HttpContext.Current.Server.MapPath("/") + FilePath, FileMode.Create)))
            {
                var jsonContent = JsonConvert.SerializeObject(tviList, Formatting.Indented);
                sw.Write(jsonContent);
                sw.Flush();
                LogController.Info("Add TeamViewerInfo Complete.");
            }
        }
    }

    [Serializable]
    public class TeamViewerInfo
    {
        //TeamViewerID
        public string TeamViewerID { get; set; }

        //TeamViewer密码
        public string TeamViewerPwd { get; set; }

        //所属店铺
        public string TeamViewOnAddress { get; set; }

        //来源IP
        public string IpAddress { get; set; }

        //最后一次上传信息的时间
        public string LastUploadTime { get; set; }
    }
}