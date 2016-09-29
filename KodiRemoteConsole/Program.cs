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

      Kodi_ActivePlayer CurrentPlayer =  KodiRpc.RPC_Player_GetActivePlayer.Execute(Player).Result;
      Trace.WriteLine($"Player {CurrentPlayer.PlayerId} of type {CurrentPlayer.PlayerType} is playing");

      KodiRpc.RPC_Player_PlayPause.Execute(Player);



      //Test = Player.Execute(new PlayList_GetPlayLists()).Result;

      //Test = Player.Execute<string>(new Player_Play(CurrentPlayer.PlayerId)).Result;
      //Thread.Sleep(5000);
      //Test = Player.Execute(new Player_SetPartyMode()).Result;

      //ConsoleExtension.Pause();
      Trace.WriteLine("Completed");
    }
  }
}
