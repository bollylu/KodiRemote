using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public abstract class KodiRpcBase {

    public abstract string RpcNamespace { get; }
    public abstract string RpcMethod { get; }

    public virtual string RpcFullname {
      get {
        return $"{RpcNamespace}.{RpcMethod}";
      }
    }

    public virtual string JsonRpcVersion {
      get {
        return "\"jsonrpc\":\"2.0\"";
      }
    }
    public virtual string JsonRpcMethod {
      get {
        return $"\"method\":\"{RpcNamespace}.{RpcMethod}\"";
      }
    }
    public abstract string JsonQuery { get; }

    internal string JsonBase {
      get {
        return $"{{{JsonRpcVersion},{JsonRpcMethod}}}";
      }
    }

    
  }
}
