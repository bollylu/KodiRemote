using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class Player_GetActivePlayers : JsonRpcRequestBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "GetActivePlayers"; } }
    
    public Player_GetActivePlayers(int id = 1) : base () {
      Id = id;
    }

  }
}
