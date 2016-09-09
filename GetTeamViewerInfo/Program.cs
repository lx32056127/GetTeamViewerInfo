using System;
using System.Windows.Forms;
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
            UploadController controller = new UploadController();
            MainMenuView main = new MainMenuView(controller);
            Application.Run();
        }
    }
}
