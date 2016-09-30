using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class Player_GetItem : JsonRpcRequestBase {

    public override string JsonRpcNamespace { get { return "Player"; } }
    public override string JsonRpcMethod { get { return "GetItem"; } }

    public override Dictionary<string, object> RequestParameters {
      get {
        Dictionary<string, object> RetVal = base.RequestParameters;
        RetVal.Add("id", "AudioGetItem");
        return RetVal;
      }
    }

    public Player_GetItem(IEnumerable<string> properties, int playerId = 1) : base() {
      KodiParameters.Add("playerid", playerId);
      if (properties == null || properties.Count() == 0) {
        KodiParameters.Add("properties", new string[] { "title" });
      } else {
        KodiParameters.Add("properties", properties.ToArray());
      }
      
    }
  }
}
