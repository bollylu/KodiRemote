using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KodiRemoteLib;
using BLTools;
using BLTools.ConsoleExtension;
using System.Diagnostics;
using BLTools.Debugging;
using System.Threading;

namespace KodiRemoteConsole {
  class Program {
    static void Main(string[] args) {
      TraceFactory.AddTraceConsole();
      Test();
    }

    static void Test() {
      KodiPlayer Player = new KodiPlayer("OSMC", "osmc.newnet.priv", 8080);

      Trace.WriteLine($"Kodi player {Player.Name} is named {Player.DnsName} and its IP is {Player.Ip.AddressList.First().MapToIPv4().ToString()}");

      string Test;

      Test = Player.Execute(new Player_GetActivePlayers(3320)).Result;
      //Test = Player.Execute(new PlayList_GetPlayLists()).Result;

      Test = Player.Execute(new Player_Play(0)).Result;
      //Thread.Sleep(5000);
      //Test = Player.Execute(new Player_SetPartyMode()).Result;

      //ConsoleExtension.Pause();
      Trace.WriteLine("Completed");
    }
  }
}
