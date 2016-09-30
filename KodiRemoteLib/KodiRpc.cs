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

    public static Player_Play RPC_Player_Play {
      get {
        if (_RPC_Player_Play == null) {
          _RPC_Player_Play = new Player_Play();
        }
        return _RPC_Player_Play;
      }
    }
    private static Player_Play _RPC_Player_Play;

    public static Player_Stop RPC_Player_Stop {
      get {
        if (_RPC_Player_Stop == null) {
          _RPC_Player_Stop = new Player_Stop();
        }
        return _RPC_Player_Stop;
      }
    }
    private static Player_Stop _RPC_Player_Stop;

    public static Player_Pause RPC_Player_Pause {
      get {
        if (_RPC_Player_Pause == null) {
          _RPC_Player_Pause = new Player_Pause();
        }
        return _RPC_Player_Pause;
      }
    }
    private static Player_Pause _RPC_Player_Pause;

    public static Player_SetPartyMode RPC_Player_SetPartyMode {
      get {
        if (_RPC_Player_SetPartyMode == null) {
          _RPC_Player_SetPartyMode = new Player_SetPartyMode();
        }
        return _RPC_Player_SetPartyMode;
      }
    }
    private static Player_SetPartyMode _RPC_Player_SetPartyMode;

  }
}
