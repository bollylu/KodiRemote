using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class Player_GetActivePlayers : KodiRpcBase {
    public override string JsonQuery {
      get {
        return JsonBase;
      }
    }

    public override string RpcMethod {
      get {
        return "GetActivePlayers";
      }
    }

    public override string RpcNamespace {
      get {
        return "Player";
      }
    }
  }
}
