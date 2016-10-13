using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class KodiResponse_ActivePlayers : JsonRpcResponseBase {

    public Dictionary<int, string> ActivePlayers { get; } = new Dictionary<int, string>();

    public KodiResponse_ActivePlayers() { }

    public override void Initialize(string jsonData) {
      if (string.IsNullOrWhiteSpace(jsonData)) {
        return;
      }
      var JsonParse = JObject.Parse(jsonData);
      List<JToken> Results = JsonParse.SelectTokens("result").ToList();
      foreach(JToken ResultItem in Results) {
        try {
          int PlayerId = (int)ResultItem[0].SelectToken("playerid");
          string PlayerType = (string)ResultItem[0].SelectToken("type");

          ActivePlayers.Add(PlayerId, PlayerType);
          
        } catch {}
      }
      
    }

  }
}
