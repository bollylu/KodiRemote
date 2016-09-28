using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_GetActivePlayers : KodiRpcBase {
    
    public override string RpcNamespace { get { return "Player"; } }
    public override string RpcMethod { get { return "GetActivePlayers"; } }

    public override string JsonQuery {
      get {
        return JsonConvert.SerializeObject(Parameters);
      }
    }
    public Player_GetActivePlayers() : base() { }
  }
}
