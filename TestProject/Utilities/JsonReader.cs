using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestProject.Utilities
{
    public class JsonReader
    {
        public string extractData(string tokenName)
        {
            string jsonText = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(jsonText);

            return jsonObject.SelectToken(tokenName)!.Value<string>()!;
        }
    }


}
