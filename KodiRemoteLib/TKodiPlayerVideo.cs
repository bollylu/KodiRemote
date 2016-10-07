using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiPlayerVideo : TKodiPlayerBase {

    public TKodiPlayerVideo(TKodiStation station, int id = -1) : base(station, id) {
      KodiPlayerType = TKodiPlayerType.Video;
    }

  }
}
