using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiPlayerBase : IKodiPlayer, IDisposable {
    public TKodiStation Station { get; protected set; }
    public int Id { get; } = -1;
    public bool IsActive { get; } = true;
    public TKodiPlayerType KodiPlayerType { get; protected set; } = TKodiPlayerType.Unknown;

    public TKodiPlayerBase(TKodiStation station, int id = -1) {
      Station = station;
      Id = id;
    }

    public void Dispose() {
      Station = null;
      KodiPlayerType = null;
    }

    public async void PlayerStop() {
      Player_Stop RpcPlayerStop = new Player_Stop(Id);
      await RpcPlayerStop.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async void PlayerPlay() {
      Player_Play RpcPlayerPlay = new Player_Play(Id);
      await RpcPlayerPlay.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async Task PlayerPause() {
      Player_Pause RpcPlayerPause = new Player_Pause(Id);
      await RpcPlayerPause.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async void PlayerPlayPause() {
      Player_PlayPause RpcPlayerPlayPause = new Player_PlayPause(Id);
      await RpcPlayerPlayPause.Execute<JsonRpcResponseEmpty>(Station);
    }
  }
}
