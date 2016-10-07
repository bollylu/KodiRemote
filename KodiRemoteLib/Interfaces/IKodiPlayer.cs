using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public interface IKodiPlayer : IDisposable {
    int Id { get; }
    TKodiPlayerType KodiPlayerType { get; }
    bool IsActive { get; }

    void PlayerStop();
    Task PlayerPause();
    void PlayerPlayPause();
    void PlayerPlay();
  }
}
