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
      using (IKodiStation KodiStation = new TKodiStation("OSMC", "osmc.newnet.priv", 8080)) {

        Trace.WriteLine($"Kodi station {KodiStation.Name} is named {KodiStation.DnsName} and its IP is {KodiStation.Ip}");

        if (KodiStation.ActiveKodiPlayer == null) {
          KodiStation.SetPartyMode();
        }

        if (KodiStation.ActiveKodiPlayer != null) {
          Trace.WriteLine($"Player {KodiStation.ActiveKodiPlayer.KodiPlayerType.Value} is active with the ID {KodiStation.ActiveKodiPlayer.Id}");
          using (IKodiPlayer RunningPlayer = KodiStation.ActiveKodiPlayer) {
            RunningPlayer.PlayerPlay();
          }
        }

        //if (KodiStation.ActivePlayers.Count > 0) {
        //  RunningPlayer = KodiStation.ActivePlayers.First();
        //  Trace.WriteLine($"Player {RunningPlayer.PlayerId} of type {RunningPlayer.PlayerType} is playing");
        //  KodiStation.PlayerStop(RunningPlayer);
        //}
        
        //KodiStation.SetPartyMode();

        //if (KodiStation.ActivePlayers.Count > 0) {
        //  RunningPlayer = KodiStation.ActivePlayers.First();
        //  Trace.WriteLine($"Player {RunningPlayer.PlayerId} of type {RunningPlayer.PlayerType} is playing");
        //  KodiStation.PlayerPlay(RunningPlayer);
        //}

        //var RunningItem = KodiStation.GetCurrentItem(RunningPlayer).Result;

        //ConsoleExtension.Pause();
        Trace.WriteLine("Completed");
      }
    }
  }
}
