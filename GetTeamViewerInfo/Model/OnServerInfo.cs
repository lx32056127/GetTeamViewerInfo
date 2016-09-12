using GetTeamViewerInfo.Commands;
using GetTeamViewerInfo.Controller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetTeamViewerInfo.Model
{

    public class OnServerInfo
    {
        public static List<TeamViewerInfoVM> Load()
        {
            List<TeamViewerInfoVM> tvilist = new List<TeamViewerInfoVM>();
            try
            {
                using (CookieWebClient _webClient = new CookieWebClient())
                {
                    byte[] requestData = _webClient.DownloadData(MainConfig.Config.WebApiUri);
                    var responseJson = Encoding.UTF8.GetString(requestData);
                    tvilist = JsonConvert.DeserializeObject<List<TeamViewerInfoVM>>(responseJson);
                }
                LogController.Info("Load Api Data Complete...");
            }
            catch(Exception e)
            {
                LogController.Error(e);
            }
            return tvilist;
        }
    }

    public class TeamViewerInfoVM
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
