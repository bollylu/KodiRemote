using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_SetPartyMode : JsonRpcRequestBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "Open"; } }

    public Player_SetPartyMode(int id = 1) : base() {

      KodiParameters.Add("item", (new Dictionary<string, object>() { { "partymode", "music"} }));

    }
    
  }
}
