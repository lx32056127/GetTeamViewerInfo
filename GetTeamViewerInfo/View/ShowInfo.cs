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
using static System.Windows.Forms.ListViewItem;

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
            List<TeamViewerInfoVM> tviList = OnServerInfo.Load();
            dgvData.DataSource = tviList;
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell dgvc = ((DataGridView)sender).SelectedCells[0];
            Clipboard.SetDataObject(dgvc.Value);
        }
    }
}
