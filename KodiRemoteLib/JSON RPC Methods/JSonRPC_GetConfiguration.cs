﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KodiRemoteLib {
  public class JSonRPC_GetConfiguration : JsonRpcRequestBase {
    
    public override string JsonRpcNamespace { get { return "JSONRPC"; } }
    public override string JsonRpcMethod { get { return "GetConfiguration"; } }
    
    public JSonRPC_GetConfiguration() : base() { }

  }
}
