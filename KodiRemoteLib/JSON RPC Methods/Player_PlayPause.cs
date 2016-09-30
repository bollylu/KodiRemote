using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_PlayPause : JsonRpcRequestBase {
    
    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "PlayPause"; } }
    
    public Player_PlayPause(int playerId = 0) : base() {
      KodiParameters.Add("playerid", playerId);
    }
    
  }
}
