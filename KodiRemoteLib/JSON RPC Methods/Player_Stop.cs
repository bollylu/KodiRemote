using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_Stop : JsonRpcRequestBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "Stop"; } }

    public Player_Stop(int playerId = 1) : base() {
      KodiParameters.Add("playerid", playerId);
    }
  }
}
