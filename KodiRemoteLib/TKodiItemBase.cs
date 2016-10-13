using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiItemBase : IKodiItem {

    public string Name { get; set; }
    public string Id { get; set; }

    public TKodiItemBase() {
      Name = "";
      Id = "";
    }
  }
}
