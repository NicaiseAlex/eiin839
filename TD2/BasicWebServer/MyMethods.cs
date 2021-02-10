using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BasicServerHTTPlistener
{
    class MyMethods
    {
        public static string myMethod(string param1, string param2) {
            Console.WriteLine(param1);
            Console.WriteLine(param2);

            string responseString = "<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>";
            return responseString;
        }
    }
}
