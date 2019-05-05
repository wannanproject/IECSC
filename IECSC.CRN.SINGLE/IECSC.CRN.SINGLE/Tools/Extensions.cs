using System.Windows.Forms;
using System.Reflection;

namespace IECSC.CRN.SINGLE
{
    public static class Extensions
    {
        /// <summary>
        /// 设置DataGridView的双缓冲
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="enable"></param>
        public static void SetDoubleBuffered(this DataGridView dgv,bool enable)
        {
            var type = dgv.GetType();
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, enable, null);
        }
    }
}
