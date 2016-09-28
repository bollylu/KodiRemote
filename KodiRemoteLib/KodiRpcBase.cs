using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public abstract class KodiRpcBase {

    public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();
    

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

    public KodiRpcBase() {
      
      Parameters.Add("jsonrpc", "2.0");
      Parameters.Add("method", $"{RpcFullname}");
      Parameters.Add("id", "1");
    }
    
  }
}
