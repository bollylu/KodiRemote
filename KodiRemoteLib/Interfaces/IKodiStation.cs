﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public interface IKodiStation : IDisposable {
    string Name { get; set; }
    string DnsName { get; set; }
    string DisplayName { get; }
    IPHostEntry IpHostEntry { get; set; }
    IPAddress Ip { get; }
    int Port { get; set; }
    Uri BaseUri { get; }
    List<IKodiPlayer> KodiPlayers { get; }
    IKodiPlayer ActiveKodiPlayer { get; }
    bool IsConnected { get; }

    Task Connect();
    Task SetPartyMode();
    Task GetActivePlayers();

  }
}
