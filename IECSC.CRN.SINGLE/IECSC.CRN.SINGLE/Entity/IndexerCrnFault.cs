using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.CRN.SINGLE
{
    public class IndexerCrnFault
    {
        private Hashtable FaultInfo = new Hashtable();

        /// <summary>
        /// 根据报警码获取报警信息
        /// </summary>
        public string this[string faultNo]
        {
            get
            {
                return FaultInfo[faultNo].ToString();
            }
            set
            {
                if(FaultInfo == null)
                {
                    FaultInfo = new Hashtable();
                }
                FaultInfo.Add(faultNo, value);
            }
        }
    }
}
