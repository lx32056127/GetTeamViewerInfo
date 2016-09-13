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
                    if (MainConfig.Config != null)
                    {
                        byte[] requestData = _webClient.DownloadData(MainConfig.Config.WebApiGetUri);
                        var responseJson = Encoding.UTF8.GetString(requestData);
                        tvilist = JsonConvert.DeserializeObject<List<TeamViewerInfoVM>>(responseJson);
                    }
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
        public string tvid { get; set; }

        //TeamViewer密码
        public string tvpwd { get; set; }

        //所属店铺
        public string addr { get; set; }

        //来源IP
        public string ip { get; set; }

        //最后一次上传信息的时间
        public string last_upload_time { get; set; }
    }
}
