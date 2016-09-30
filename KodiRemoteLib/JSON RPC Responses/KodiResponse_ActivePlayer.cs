using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class KodiResponse_ActivePlayer : JsonRpcResponseBase {

    public int PlayerId { get; set; }
    public string PlayerType { get; set; }
    public KodiResponse_ActivePlayer() { }
    public KodiResponse_ActivePlayer(int playerId, string playerType) {
      PlayerId = playerId;
      PlayerType = playerType;
    }

    public override void Initialize(string jsonData) {
      var JsonResult = JObject.Parse(jsonData);
      try { PlayerId = (int)JsonResult.SelectToken("result[0].playerid"); } catch { PlayerId = -1; }
      try { PlayerType = (string)JsonResult.SelectToken("result[0].type"); } catch { PlayerType = ""; }
    }

  }
}
