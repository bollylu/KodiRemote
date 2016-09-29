using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class Kodi_ActivePlayer {
    public int PlayerId { get; set; }
    public string PlayerType { get; set; }
    public Kodi_ActivePlayer() { }
    public Kodi_ActivePlayer(int playerId, string playerType) {
      PlayerId = playerId;
      PlayerType = playerType;
    }

  }
}
