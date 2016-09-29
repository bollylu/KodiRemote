using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class Player_GetActivePlayers : JsonRpcBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "GetActivePlayers"; } }
    
    public Player_GetActivePlayers(int id = 1) : base () {
      Id = id;
    }

    public async Task<Kodi_ActivePlayer> Execute(IKodiPlayer player) {
      string ResponseAsString = await GetResponseString(player);

      var JsonResult = JObject.Parse(ResponseAsString);

      Kodi_ActivePlayer RetVal = new Kodi_ActivePlayer();
      RetVal.PlayerId = (int)JsonResult.SelectToken("result[0].playerid");
      RetVal.PlayerType = (string)JsonResult.SelectToken("result[0].type");
      return RetVal;

    }
  }
}
