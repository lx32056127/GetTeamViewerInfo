using System;
using System.Windows.Forms;
using GetTeamViewerInfo.Commands;
using GetTeamViewerInfo.Controller;
using GetTeamViewerInfo.View;

namespace GetTeamViewerInfo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Command.ReleaseMemory(true);
            LogController.OpenLogFile();
            var controller = new UploadController();
            var menuView = new MainMenuView(controller);
            LogController.Info("Application Starting...");
            Application.Run();
        }
    }
}
