using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public abstract class JsonRpcResponseBase : IKodiJsonResponse {
    public string JsonRpcVersion { get { return "2.0"; } }

    public abstract void Initialize(string jsonData);
  }
}
