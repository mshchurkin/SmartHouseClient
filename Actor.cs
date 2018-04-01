using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHouseClient
{
    public class Actor
    {
        public String id { get; set; }

        public String houseId { get; set; }

        public String fieldName { get; set; }

        public int value { get; set; }

        public String actorType { get; set; }

        public async void Start(String SERVER_PATH, String TOKEN)
        {
            do
            {
                await GetDataTask(5000, SERVER_PATH, TOKEN);

            } while (true);
        }

        Task GetDataTask(int time, String SERVER_PATH, String TOKEN)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                GetData(SERVER_PATH, TOKEN);
            });
        }

        public void GetData(String SERVER_PATH, String TOKEN)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "actorLastValue /"+this.id+"/"+TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;

                    var definition = new { value = "" };

                    var res = JsonConvert.DeserializeAnonymousType(json, definition);

                    value = Int32.Parse(res.value);

                }
            }
            catch (Exception em) { }
        }
    }
}
