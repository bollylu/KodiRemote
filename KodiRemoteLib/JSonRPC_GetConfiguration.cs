using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class JSonRPC_GetConfiguration : JsonRpcBase {
    
    public override string RpcNamespace { get { return "JSONRPC"; } }
    public override string RpcMethod { get { return "GetConfiguration"; } }
    
    public JSonRPC_GetConfiguration() : base() { }
  }
}
