using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_Play : JsonRpcBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "PlayPause"; } }

    public Player_Play(int playerId = 1) : base() {
      KodiParameters.Add("playerid", playerId);
      KodiParameters.Add("play", true);
    }
  }
}
