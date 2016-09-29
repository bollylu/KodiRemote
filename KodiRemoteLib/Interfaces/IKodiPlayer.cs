using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public interface IKodiPlayer {
    string Name { get; set; }
    string DnsName { get; set; }
    IPHostEntry Ip { get; set; }
    int Port { get; set; }
    Uri BaseUri { get; }
  }
}
