using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class KodiResponse_CurrentlyPlayedItem : JsonRpcResponseBase {

    public int PlayerId { get; set; }

    public KodiResponse_CurrentlyPlayedItem() { }
    public KodiResponse_CurrentlyPlayedItem(int playerId, IEnumerable<string> properties) {
      PlayerId = playerId;
    }

    public override void Initialize(string jsonData) {
      var JsonResult = JObject.Parse(jsonData);
    }

  }
}
