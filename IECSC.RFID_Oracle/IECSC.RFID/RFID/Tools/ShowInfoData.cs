using RFID.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID.Tools
{
    public class ShowInfoData
    {
        /// <summary>
        /// 要显示在界面上的信息
        /// </summary>
        public ShowInfoData(string stringInfo, ShowInfoType infoType )
        {
            StringInfo = stringInfo;
            InfoType = infoType;
        }

        public ShowInfoData(ShowInfoType infoType)
        {
            InfoType = infoType;
        }

        /// <summary>
        /// 显示在界面的文字信息
        /// </summary>
        public string StringInfo { get; set; }

        /// <summary>
        /// 显示信息类型
        /// </summary>
        public ShowInfoType InfoType { get; set; }
    }


}

