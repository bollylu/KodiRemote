using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KodiRemoteLib;
using BLTools;
using BLTools.ConsoleExtension;
using System.Diagnostics;

namespace KodiRemoteConsole {
  class Program {
    static void Main(string[] args) {
      Test();
    }

    static async void Test() {
      KodiPlayer Player = new KodiPlayer("OSMC", "osmc.newnet.priv", 8080);

      Console.WriteLine($"Kodi player {Player.Name} is named {Player.DnsName} and its IP is {Player.Ip.AddressList.First().MapToIPv4().ToString()}");

      Trace.WriteLine("Doing request");
      string Test = Player.Execute(new Player_GetActivePlayers()).Result;
      Trace.WriteLine("Got result");
      Trace.WriteLine(Test);

      ConsoleExtension.Pause();
      Trace.WriteLine("Completed");
    }
  }
}
