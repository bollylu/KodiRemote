using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_Play : JsonRpcBase {

    public override string RpcNamespace { get { return "Player"; } }
    public override string RpcMethod { get { return "PlayPause"; } }

    public Player_Play(int playerId = 1) : base(1) {
      KodiParameters.Add("playerid", playerId);
      KodiParameters.Add("play", true);
    }
  }
}
