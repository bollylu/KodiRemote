using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public static class KodiRpc {

    public static Player_GetActivePlayers RPC_Player_GetActivePlayer {
      get {
        if (_RPC_Player_GetActivePlayer == null) {
          _RPC_Player_GetActivePlayer = new Player_GetActivePlayers();
        }
        return _RPC_Player_GetActivePlayer;
      }
    }
    private static Player_GetActivePlayers _RPC_Player_GetActivePlayer;

    public static Player_PlayPause RPC_Player_PlayPause {
      get {
        if (_RPC_Player_PlayPause == null) {
          _RPC_Player_PlayPause = new Player_PlayPause();
        }
        return _RPC_Player_PlayPause;
      }
    }
    private static Player_PlayPause _RPC_Player_PlayPause;

  }
}
