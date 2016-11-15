using System;
using System.Windows.Forms;
using GetTeamViewerInfo.Controller;
using GetTeamViewerInfo.Model;
using GetTeamViewerInfo.Properties;
using Microsoft.Win32;

namespace GetTeamViewerInfo.View
{
    public class MainMenuView
    {
        private static UploadController _upload;
        private static NotifyIcon _notify;
        private static ContextMenu _menu;
        private static readonly MenuItem _autoUpload = new MenuItem("自动上报TeamViewer信息");
        private static readonly MenuItem _getTeamviewerInfo = new MenuItem("获取TeamViewer信息列表");
        private static readonly MenuItem _exit= new MenuItem("退出");
        private static readonly MenuItem _autostart = new MenuItem("开机自启动");
        private static System.Threading.Timer _logTimeTimer;
        private static ShowInfo si = new ShowInfo();

        public MainMenuView(UploadController controller)
        {
            //初始化配置
            _logTimeTimer = new System.Threading.Timer(CheckLogDate, null, 1000, 1000);

            //读取配置
            MainConfig.Load();
            MainConfig.UploadEnableChanges += SetEnableUpload;
            MainConfig.Config.UploadEnable = MainConfig.Config.UploadEnable;
            _upload = controller;
            LogController.Info("Config Load Complete");

            //初始化菜单
            _menu = new ContextMenu()
            {
                MenuItems =
            {
                _autoUpload,
                _getTeamviewerInfo,
                new MenuItem("-"),
                _autostart,
                _exit
            }
            };
            _getTeamviewerInfo.Click+=OpenGetData;
            _autostart.Click += _autostart_Click;
            //初始化托盘
            _notify = new NotifyIcon
            {
                Icon = Resources.icon_32x32,
                Visible = true,
                Text = MainConfig.Config.BubbleInfo,
                ContextMenu = _menu
            };
            LogController.Info("Menu Load Complete");

            //事件绑定
            _exit.Click += ExitApplication;
            _autoUpload.Click += UploadClick;

            LogController.Info("Application Initialization Complete");
        }

        private void _autostart_Click(object sender, EventArgs e)
        {
            _autostart.Checked = !_autostart.Checked;
            bool x = _autostart.Checked;
            if (x) //设置开机自启动  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("GTIStart", path);
                rk2.Close();
                rk.Close();
            }
            else //取消开机自启动  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("GTIStart", false);
                rk2.Close();
                rk.Close();
            }
        }

        private void OpenGetData(object sender,EventArgs e)
        {
            if (!si.Visible)
                si = new ShowInfo();
            si.Show();
        }

        //检查日志时间
        private void CheckLogDate(object o)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            if (date != LogController.NowDate)
                LogController.OpenLogFile();
        }

        //自动上传选项点击事件
        private static void UploadClick(object sender, EventArgs e)
        {
            MainConfig.Config.UploadEnable = !MainConfig.Config.UploadEnable;
        }

        //退出按钮点击事件
        private static void ExitApplication(object sender, EventArgs e)
        {
            LogController.Info("Application Exit...");
            //程序结束
            Application.Exit();
        }

        private static void SetEnableUpload(object sender, EventArgs e)
        {
            if (MainConfig.Config == null) return;
            _autoUpload.Checked = MainConfig.Config.UploadEnable;
            LogController.Info("Auto Upload Teamviewer Start:" + MainConfig.Config.UploadEnable);
            MainConfig.Save();
        }
    }
}
