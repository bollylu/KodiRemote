using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KodiRemoteLib {
  public class KodiPlayer : IKodiPlayer {

    public string Name { get; set; }
    public string DnsName { get; set; }
    public IPHostEntry Ip { get; set; }
    public int Port { get; set; }
    public Uri BaseUri {
      get {
        return new Uri($"http://{DnsName}:{Port}");
      }
    }

    public KodiPlayer() { }

    public KodiPlayer(string name, string dnsName, int port = 8080) {
      Initialize(name, dnsName, port);
    }

    private void Initialize(string name, string dnsName, int port) {
      Name = name;
      DnsName = dnsName;
      Ip = Dns.GetHostEntry(dnsName);
      Port = port;
    }

    public bool IsAvailable() {
      return false;
    }

    public async Task<T> Execute<T>(JsonRpcBase kodiRequest) {

      Trace.WriteLine($"Execution request : {kodiRequest.JsonRpcFullname}");
      using (HttpClientHandler Handler = new HttpClientHandler()) {
        Handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
        //Handler.Credentials = CredentialCache.DefaultNetworkCredentials;
        Handler.UseDefaultCredentials = true;
        using (HttpClient Client = new HttpClient(Handler)) {
          Client.BaseAddress = BaseUri;


          HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, $"jsonrpc?{kodiRequest.JsonRpcFullname}");
          string JsonContent = kodiRequest.RequestParameters.ToJson();

          Request.Content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

          Trace.WriteLine($"Content={JsonContent}");

          HttpResponseMessage Response = await Client.SendAsync(Request);

          string ResponseAsString = await Response.Content.ReadAsStringAsync();

          Trace.WriteLine($"Response : {ResponseAsString}");

          var JsonResult = JObject.Parse(ResponseAsString);

          switch (typeof(T).Name) {
            case "Kodi_ActivePlayer":
              Kodi_ActivePlayer RetVal = new Kodi_ActivePlayer();
              RetVal.PlayerId = (int)JsonResult.SelectToken("result[0].playerid");
              RetVal.PlayerType = (string)JsonResult.SelectToken("result[0].type");
              return (T)Convert.ChangeType(RetVal, typeof(T));
          }

          return default(T);
        }
      }
    }

  }
}
