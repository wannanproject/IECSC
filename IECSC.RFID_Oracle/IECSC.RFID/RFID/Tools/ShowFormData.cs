using RFID.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID.Tools
{
    public class ShowFormData
    {
        private static ShowFormData _instance;
        public static ShowFormData Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(ShowFormData))
                    {
                        if (_instance == null)
                        {
                            _instance = new ShowFormData();
                        }
                    }
                }
                return _instance;
            }
        }
        private ShowFormData(){}

        public delegate void AppDataEventHandler(object sender, AppDataEventArgs e);
        public event AppDataEventHandler OnAppDtoData;

        
        public void ShowFormInfo(ShowInfoData data)
        {
            if (OnAppDtoData == null)
            {
                return;
            }
            var eventArgs = new AppDataEventArgs();
            eventArgs.AppData = data;
            OnAppDtoData(this, eventArgs);
        }



    }
}
