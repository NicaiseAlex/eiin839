using System;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace BasicServerHTTPlistener
{
    public class MyMethods
    {
        private Uri url;
        private static int nbrReload = 0;

        public MyMethods(Uri url)
        {
            this.url = url;
        }

        public string myMethod() {

            System.Collections.Specialized.NameValueCollection parameters = HttpUtility.ParseQueryString(this.url.Query);
            string responseString = "<HTML><BODY> <h1>Question 4</h1> <br/> ";

            foreach(string param in parameters.AllKeys) {
                string[] values = parameters.AllKeys;
                responseString += param + " ";
            }

            responseString += "<br/></HTML></BODY>";
            return responseString;
        }

        public static string myMethodExe()
        {
            //
            // Set up the process with the ProcessStartInfo class.
            // https://www.dotnetperls.com/process
            //
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\tacti\Documents\GitHub\eiin839\TD2\ExecMethode\bin\Debug\ExecMethode.exe"; // Specify exe name.
            start.Arguments = "Arg1Exe Arg2Exe"; // Specify arguments.
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            //
            // Start the process.
            //
            using (Process process = Process.Start(start))
            {
                //
                // Read in all the text from the process with the StreamReader.
                //
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string incrReload()
        {
            nbrReload++;
            return "<h1> Reload number = " + (nbrReload/2+1) + "</h1>";
        }
    }

    
}
