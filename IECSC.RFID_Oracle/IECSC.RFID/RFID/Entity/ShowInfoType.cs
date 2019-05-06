using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID.Entity
{
    public enum ShowInfoType
    {
        //显示日志
        logInfo = 0,
        //socket连接状态
        status = 1,
        //登陆时间
        logintime = 2,
        //背景色
        backcolor = 3,
        //Socket连接
        socketconn = 4
    }
}
