using System.Windows.Forms;
using GetTeamViewerInfo.Controller;
using GetTeamViewerInfo.Properties;

namespace GetTeamViewerInfo.View
{
    public class MainMenuView
    {
        private UploadController _upload;
        private NotifyIcon _notify;
        private ContextMenu _menu;

        public MainMenuView(UploadController controller)
        {
            _upload = controller;
            _notify = new NotifyIcon {Icon = Resources.icon_32x32, Visible = true};
            _menu = new ContextMenu() {MenuItems =
            {
                new MenuItem("自动上报TeamViewer信息"),
                new MenuItem("获取TeamViewer信息列表"),
                new MenuItem("-"),
                new MenuItem("退出")
            }};
            _notify.ContextMenu = _menu;
        }
    }
}
