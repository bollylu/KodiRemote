using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public interface IKodiJsonResponse {
    string JsonRpcVersion { get; }
    void Initialize(string jsonData);
  }
}
