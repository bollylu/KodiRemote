using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public abstract class JsonRpcBase {

    public abstract string RpcNamespace { get; }
    public abstract string RpcMethod { get; }
    public virtual string RpcFullname {
      get {
        return $"{RpcNamespace}.{RpcMethod}";
      }
    }

    public Dictionary<string, object> RequestParameters {
      get {
        Dictionary<string, object> TempParameters = new Dictionary<string, object>(_RequestParameters);
        if (KodiParameters.Count != 0) {
          TempParameters.Add("params", KodiParameters);
        }
        if (Id !=-1) {
          TempParameters.Add("id", Id);
        }
        return TempParameters;
      }
    }
    private Dictionary<string, object> _RequestParameters = new Dictionary<string, object>();

    public Dictionary<string, object> KodiParameters { get; } = new Dictionary<string, object>();

    public int Id { get; set; } = -1;

    public JsonRpcBase(int id = -1) {
      _RequestParameters = new Dictionary<string, object>();
      _RequestParameters.Add("jsonrpc", "2.0");
      _RequestParameters.Add("method", $"{RpcFullname}");
      Id = id;
    }

  }
}
