using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHouseClient
{
    public class Sensor
    {
        public  String id { get; set; }

        public String houseId { get; set; }

        public String measurement { get; set; }

        public String fieldName { get; set; }

        public int value { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public String sensorType { get; set; }
        public String extreme { get; set; }


        public async void Start(String SERVER_PATH, String TOKEN)
        {
            do
            {
                await SendDataTask(5000, SERVER_PATH, TOKEN);

            } while (true);
        }

        Task SendDataTask(int time, String SERVER_PATH, String TOKEN)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                SendData(SERVER_PATH,TOKEN);
            });
        }

        public void SendData( String SERVER_PATH, String TOKEN)
        {
            int res = value;
            Random rnd = new Random();

            if (sensorType == "ANALOG")
            {
                res = rnd.Next(MinValue, MaxValue);
            }
            else
            {
                res = rnd.Next(0, 1);
            }
            value = res;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "sensorNewValue/"+id+"/"+TOKEN+"/"+value;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception em) { }
        }
    }
}
