using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using diagrammatically.Domein;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;
using Newtonsoft.Json;

namespace diagrammatically.oxforddictionaries
{
    public class SearchConsumer : WordFinder
    {
        public async Task<IEnumerable<WordMatch>> ConsumeAsync(string input, IEnumerable<string> langs)
        {
            if(langs.All(l => l != "en"))
                return new List<WordMatch>();

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
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
                {
                    return reader.ReadToEnd()
                        .Pipe(JsonConvert.DeserializeObject<SearchResulds>)
                        .Results
                        .Select(r => new WordMatch(input,r.Word,r.Score, 0,"OxfordDix"))
                        .ToArray();
                }
            }
            catch
            {
                return new List<WordMatch>();
            }
        }
    }
}
