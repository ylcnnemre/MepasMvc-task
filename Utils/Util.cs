using MepasTask.Abstract;
using System.Diagnostics;

namespace MepasTask.Utils
{
    public class Util:IUtil
    {

        public bool IsExcelOpen()  // Excelin  açık olup olmadığını kontrol eden metod
        {
            Process[] processes = Process.GetProcessesByName("EXCEL");

            if (processes.Length == 0)
            {
                return true;
            }
            return false;

        }
    }
}
