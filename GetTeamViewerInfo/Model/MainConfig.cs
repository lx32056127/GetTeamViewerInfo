using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetTeamViewerInfo.Model
{
    [Serializable]
    public class MainConfig
    {
        //上报TeamViewer信息开关
        public Boolean UploadEnable { get; set; }

        //气泡信息
        public string BubbleInfo { get; set; }
    }
}
