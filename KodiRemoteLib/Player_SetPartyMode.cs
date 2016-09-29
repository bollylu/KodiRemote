using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_SetPartyMode : JsonRpcBase {

    public override string RpcNamespace { get { return "Player"; } }
    public override string RpcMethod { get { return "Open"; } }

    public Player_SetPartyMode(int id = 1) : base(id) {

      KodiParameters.Add("item", (new Dictionary<string, object>() { { "partymode", "music"} }));

    }
  }
}
