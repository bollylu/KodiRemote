using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiItemAudio : TKodiItemBase {

    public string Title { get; set; }
    public List<string> Artists { get; set; } = new List<string>();
    public string Album { get; set; }
    public int TrackNumber { get; set; }
    public TimeSpan Duration { get; set; }

    public TKodiItemAudio() : base() {
      Title = "";
      Album = "";
      TrackNumber = 0;
      Duration = new TimeSpan(0);
    }
  }
}
