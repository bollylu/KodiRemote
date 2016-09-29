using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public interface IKodiJsonRequest {
    string JsonRpcVersion { get; }
    string JsonRpcNamespace { get; }
    string JsonRpcMethod { get; }
    string JsonRpcFullname { get; }

    Dictionary<string, object> RequestParameters { get; }
    Dictionary<string, object> KodiParameters { get; }

    int Id { get; set; }
  }
}
