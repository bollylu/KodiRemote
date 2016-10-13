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

    public async Task PlayerStop() {
      Player_Stop RpcPlayerStop = new Player_Stop(Id);
      await RpcPlayerStop.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async Task PlayerPlay() {
      Player_Play RpcPlayerPlay = new Player_Play(Id);
      await RpcPlayerPlay.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async Task PlayerPause() {
      Player_Pause RpcPlayerPause = new Player_Pause(Id);
      await RpcPlayerPause.Execute<JsonRpcResponseEmpty>(Station);
    }

    public async Task PlayerPlayPause() {
      Player_PlayPause RpcPlayerPlayPause = new Player_PlayPause(Id);
      await RpcPlayerPlayPause.Execute<JsonRpcResponseEmpty>(Station);
    }

    public virtual async Task<IKodiItem> GetCurrentItem() {
      //string[] Properties = new string[] { "title", "artist" };
      //Player_GetItem RpcPlayerGetItem = new Player_GetItem(Properties, Id);
      //KodiResponse_CurrentlyPlayedItem CurrentItem = await RpcPlayerGetItem.Execute<KodiResponse_CurrentlyPlayedItem>(Station);
      return await Task.FromResult<IKodiItem>(null);
    }
  }
}
