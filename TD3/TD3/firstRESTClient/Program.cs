using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetContractList()
        {
            string responseBody = "";

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=4146c0769e10f0daee66cb07d63d068810d1d0f3");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return responseBody;
        }

        public static async Task<string> GetListOfStations(string contract) {
            string responseBody = "";

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations?contract=" + contract + "&apiKey=4146c0769e10f0daee66cb07d63d068810d1d0f3");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return responseBody;
        }

        public static async Task<string> GetStationInformation(int station, string contract)
        {
            string responseBody = "";

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations/" + station + "?contract=" + contract + "&apiKey=4146c0769e10f0daee66cb07d63d068810d1d0f3");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return responseBody;
        }

        public static async Task<string> GetClosestStation(string contract, Double lati, Double longi)
        {
            Task<string> responseBody = GetListOfStations("bruxelles");

            //TODO trouver la station la plus proche  https://docs.microsoft.com/fr-fr/dotnet/api/system.device.location.geocoordinate?view=netframework-4.8

            return await responseBody;
        }

        static async Task Main(string[] args)
        {

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }

            HttpListener listener = new HttpListener();

            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                Console.WriteLine($"Received request for {request.Url}");
                Console.WriteLine(documentContents);

                HttpListenerResponse response = context.Response;

                //Task<string> res = GetContractList();
                //Task<string> res = GetListOfStations("bruxelles");
                Task<string> res = GetStationInformation(122, "bruxelles");
                string res1 = await res;

                string responseString = "<HTML><BODY>" + res1 + "</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
            }
        }
    }
}
