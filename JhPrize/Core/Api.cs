using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;

using Newtonsoft.Json;

namespace JhPrize.Core
{
    public enum ApiOption
    {
        GetData,
        Select,
        Verify
    };

    public class Api
    {
        private static Dictionary<ApiOption, string> dic = new Dictionary<ApiOption, string>()
        {
            { ApiOption.GetData, "prize/get_data" },
            { ApiOption.Select,"prize/select" },
            { ApiOption.Verify,"prize/verify" }
        };

        public static string Pass { get; set; } = "123456";
        public static string Domain { get; set; } = "http://localhost/";

        public static string GetUrl(ApiOption option)
        {
            return $"{Domain}{dic[option]}";
        }



        public async static Task<String> PostAsync(ApiOption option, params KeyValuePair<String,String>[] p)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            var httpClient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri( GetUrl(option)));
            
            var responseMessage = await httpClient.PostAsync(new Uri(GetUrl(option)),new HttpFormUrlEncodedContent(p));
            
            if(responseMessage.StatusCode == HttpStatusCode.Ok)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

        public async static Task<ResponseModel<PrizeModel>> DrawPrizeAsync(string title, string no)
        {
            ResponseModel<PrizeModel> model = JsonConvert.DeserializeObject<ResponseModel<PrizeModel>>(
                await PostAsync(ApiOption.Select, 
                    new KeyValuePair<string, string>("pass", Pass),
                    new KeyValuePair<string, string>("no", no),
                    new KeyValuePair<string,string>("title",title)
                ));

            return model;
        }

        public async static Task<ResponseModel<PrizeModel>> AcceptPrizeAsync(string no)
        {
            ResponseModel<PrizeModel> model = JsonConvert.DeserializeObject<ResponseModel<PrizeModel>>(
                await PostAsync(ApiOption.Verify,
                    new KeyValuePair<string, string>("pass", Pass),
                    new KeyValuePair<string, string>("no", no)
                ));

            return model;
        }


        public async static Task<IEnumerable<PrizePool>> GetDataAsync()
        {
            ResponseModel<PrizePoolModel[]> model = JsonConvert.DeserializeObject<ResponseModel<PrizePoolModel[]>>(
                await PostAsync(ApiOption.GetData, new KeyValuePair<string, string>("pass", Pass)));
            List<PrizePool> result = new List<PrizePool>();
            return model.data.Select((a) => a.ToPrizePool());
        }
    }

    
}
