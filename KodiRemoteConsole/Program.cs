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
      TraceFactory.AddTraceDefaultLogFilename();
      Test();
    }

    static void Test() {
      using (KodiPlayer CurrentKodiPlayer = new KodiPlayer("OSMC", "osmc.newnet.priv", 8080)) {

        Trace.WriteLine($"Kodi player {CurrentKodiPlayer.Name} is named {CurrentKodiPlayer.DnsName} and its IP is {CurrentKodiPlayer.Ip.AddressList.First().MapToIPv4().ToString()}");
        KodiResponse_ActivePlayer RunningPlayer = null;

        if (CurrentKodiPlayer.ActivePlayers.Count > 0) {
          RunningPlayer = CurrentKodiPlayer.ActivePlayers.First();
          Trace.WriteLine($"Player {RunningPlayer.PlayerId} of type {RunningPlayer.PlayerType} is playing");
          //CurrentKodiPlayer.PlayerStop(RunningPlayer);
        }

        //CurrentKodiPlayer.SetPartyMode();
        //if (CurrentKodiPlayer.ActivePlayers.Count > 0) {
        //  RunningPlayer = CurrentKodiPlayer.ActivePlayers.First();
        //  Trace.WriteLine($"Player {RunningPlayer.PlayerId} of type {RunningPlayer.PlayerType} is playing");
        //  CurrentKodiPlayer.PlayerPlay(RunningPlayer);
        //}

        var RunningItem = CurrentKodiPlayer.GetCurrentItem(RunningPlayer).Result;


        //Test = Player.Execute(new PlayList_GetPlayLists()).Result;

        //Test = Player.Execute<string>(new Player_Play(CurrentPlayer.PlayerId)).Result;
        //Thread.Sleep(5000);
        //Test = Player.Execute(new Player_SetPartyMode()).Result;

        //ConsoleExtension.Pause();
        Trace.WriteLine("Completed");
      }
    }
  }
}
