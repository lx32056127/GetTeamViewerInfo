using GetTeamViewerInfo.Model;
using GetTeamViewerInfo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetTeamViewerInfo.View
{
    public partial class ShowInfo : Form
    {
        public ShowInfo()
        {
            InitializeComponent();
            this.Icon = Resources.icon_64x64;
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lvData.Items.Clear();
            List<TeamViewerInfoVM> tviList = OnServerInfo.Load();
            foreach(TeamViewerInfoVM tvi in tviList)
            {
                ListViewItem lvi = new ListViewItem(tvi.TeamViewerID);
                lvi.SubItems.Add(tvi.TeamViewerPwd);
                lvi.SubItems.Add(tvi.TeamViewOnAddress);
                lvData.Items.Add(lvi);
            }
        }
    }
}
