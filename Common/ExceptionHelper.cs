using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ExceptionHelper
    {
        private static Queue<Exception> ExceptionQueues = new Queue<Exception>();
        private static Queue<string> ExceptionInfo = new Queue<string>();
        private static LogHelper logHelper = new LogHelper();

        public static void Enqueue(string info, Exception ex)
        {
            ExceptionInfo.Enqueue(info);
            ExceptionQueues.Enqueue(ex);
        }
        public static Exception Dequeue(out string info)
        {
            if (ExceptionQueues.Count>0)
            {
                info = ExceptionInfo.Dequeue();
                return ExceptionQueues.Dequeue();
            }
            info = "";
            return null;
        }

        public static void WriteLog()
        {
            while (true)
            {
                if (ExceptionQueues.Count>0)
                {
                    string info = "";
                    Exception ex = Dequeue(out info);
                    logHelper.Debug(info, ex);
                }
                else
                {
                    Thread.Sleep(1000 * 5);//线程休眠5秒钟，防止cpu空转
                }

            }
        }
    }
}
