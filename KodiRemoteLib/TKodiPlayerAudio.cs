using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiPlayerAudio : TKodiPlayerBase {

    public TKodiPlayerAudio(TKodiStation station, int id = -1) : base(station, id) {
      KodiPlayerType = TKodiPlayerType.Audio;
    }

    public override async Task<IKodiItem> GetCurrentItem() {
      string[] Properties = new string[] { "title", "artist", "album", "track", "duration" };
      Player_GetItem RpcPlayerGetItem = new Player_GetItem(Properties, Id);
      KodiResponse_CurrentlyPlayedItem CurrentItem = await RpcPlayerGetItem.Execute<KodiResponse_CurrentlyPlayedItem>(Station).ConfigureAwait(false);

      TKodiItemAudio RetVal = new TKodiItemAudio();
      RetVal.Title = CurrentItem.Title;
      RetVal.Artists = CurrentItem.Artists;
      RetVal.Album = CurrentItem.Album;
      RetVal.TrackNumber = CurrentItem.TrackNumber;
      RetVal.Duration = new TimeSpan(0, 0, CurrentItem.Duration);
      return RetVal;

    }
  }
}
