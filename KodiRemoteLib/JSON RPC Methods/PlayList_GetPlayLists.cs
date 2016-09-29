using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class PlayList_GetPlayLists : JsonRpcBase {

    public override string JsonRpcNamespace { get { return "PlayList"; } }
    public override string JsonRpcMethod { get { return "GetPlayLists"; } }
    
    public PlayList_GetPlayLists(int id = 1) : base () {
    }
  }
}
