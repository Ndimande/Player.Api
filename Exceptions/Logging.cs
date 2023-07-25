
using System;
using System.IO;

namespace Player.Api.Exceptions
{
    public static class Logging
    {
        public static void WriteErrorLog(string message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt",true);
                Log(message, sw);
                sw.Flush();
                sw.Close();
            }
            catch { 
                throw; 
            }
        }


        public static void Log(string logMessage, StreamWriter sWriter)
        {
            try
            {
                sWriter.Write("\r\nLog Entry : ");
                sWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                sWriter.WriteLine("  :");
                sWriter.WriteLine("  :{0}", logMessage);
                sWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }

    }
}
