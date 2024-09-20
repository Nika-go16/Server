using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Timers;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:51369/");

            httpListener.Start();
            TimeSpan timeout = TimeSpan.FromMilliseconds(1000);
            while (true)
            {
                var requestContext = httpListener.GetContext();
                requestContext.Response.StatusCode = 200;
                var stream = requestContext.Response.OutputStream;
                string massage;
                DateTime per1 = DateTime.Now;
                Console.WriteLine(per1);
                string per2 = per1.ToString("ddMMyyyyHHssmm");
                int[] b = new int[per2.Length];
                int chet = 0, nechet = 0;
                for (int i = 0; i < per2.Length; i++)
                {
                    b[i] = (int)char.GetNumericValue(per2[i]);
                    if (b[i] % 2 == 0)
                    {
                        chet++;
                    }
                    else
                    {
                        nechet++;
                    }
                }
                if (chet == nechet)
                {
                    massage = "Равно";
                }
                else
                {
                    if (chet > nechet)
                    {
                        massage = "Чет";
                    }
                    else
                    {
                        massage = "Нечет";
                    }
                }
                while (DateTime.Now - per1 < timeout)
                {
                    var bytes = Encoding.UTF8.GetBytes(massage);
                    stream.Write(bytes, 0, bytes.Length);
                    requestContext.Response.Close();
                }
            }  
            httpListener.Stop();
            httpListener.Close();

        }
        

    }
}
