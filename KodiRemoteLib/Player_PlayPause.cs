using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_PlayPause : JsonRpcBase {
    
    public override string RpcNamespace { get { return "Player"; } }
    public override string RpcMethod { get { return "PlayPause"; } }
    
    public Player_PlayPause(int id = 1) : base(id) {
      KodiParameters.Add("playerid", 1);
    }
  }
}
