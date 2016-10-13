using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class KodiResponse_CurrentlyPlayedItem : JsonRpcResponseBase {

    public int PlayerId { get; set; }
    public string Title { get; private set; }
    public List<string> Artists { get; private set; } = new List<string>();
    public string Album { get; private set; }
    public int TrackNumber { get; private set; }
    public int Duration { get; private set; }

    public KodiResponse_CurrentlyPlayedItem() { }
    public KodiResponse_CurrentlyPlayedItem(int playerId, IEnumerable<string> properties) {
      PlayerId = playerId;
    }

    public override void Initialize(string jsonData) {
      if (string.IsNullOrWhiteSpace(jsonData)) {
        return;
      }

      dynamic JsonParsed = JsonConvert.DeserializeObject<dynamic>(jsonData);

      try {
        Title = JsonParsed.result.item.title;
        foreach (string ArtistItem in JsonParsed.result.item.artist) {
          Artists.Add(ArtistItem);
        }
        Album = JsonParsed.result.item.album;
        TrackNumber = JsonParsed.result.item.track;
        Duration = JsonParsed.result.item.duration;

      } catch { }
    }
  }

}
