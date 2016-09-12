using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetTeamViewerInfo.Model
{
    public class TeamViewerInfo
    {
        public static event EventHandler IdChange;
        public static event EventHandler PwdChange;

        private string _id;
        public string id { get { return _id; } set { _id = value; IdChange(this, new EventArgs()); } }

        private string _pwd;
        public string pwd { get { return _pwd; } set { _pwd = value; PwdChange(this, new EventArgs()); } }
    }
}
