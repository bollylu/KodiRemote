using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class Player_GetActivePlayers : JsonRpcBase {

    public override string RpcNamespace { get { return "Player"; } }
    public override string RpcMethod { get { return "GetActivePlayers"; } }
    
    public Player_GetActivePlayers(int id = 1) : base (id) {
    }
  }
}
