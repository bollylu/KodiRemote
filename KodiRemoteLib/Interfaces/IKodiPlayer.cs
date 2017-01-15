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

    Task PlayerStop();
    Task PlayerPause();
    Task PlayerPlayPause();
    Task PlayerPlay();
    Task PlayerPlayRandom();

    Task<IKodiItem> GetCurrentItem();
  }
}
