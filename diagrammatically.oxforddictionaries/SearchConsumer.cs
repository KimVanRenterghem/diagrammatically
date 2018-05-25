using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using diagrammatically.Domein;
using CSharp.Pipe;
using Newtonsoft.Json;

namespace diagrammatically.oxforddictionaries
{
    public class SearchConsumer : IInputConsumer
    {
        public async Task<IEnumerable<string>> Consume(string input)
        {
            const string language = "en";
            const string appId = "";
            const string appKey = "";

            var url = @"https://od-api.oxforddictionaries.com:443/api/v1/search/" + language + "?q=" + input;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("app_id", appId);
            request.Headers.Add("app_key", appKey);
            try
            {
                using (var response = (HttpWebResponse) await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd()
                        .Pipe(JsonConvert.DeserializeObject<SearchResulds>)
                        .results
                        .Select(r => r.word)
                        .ToArray();
                }
            }
            catch (Exception e)
            {
                return new string[0];
            }
        }
    }
}
