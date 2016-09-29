using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Net;
using System.Net.Http;
using System.Diagnostics;

namespace KodiRemoteLib {
  public class KodiPlayer {

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

    public async Task<string> Execute(JsonRpcBase kodiRequest) {

      Trace.WriteLine($"Execution request : {kodiRequest.RpcFullname}");
      using (HttpClientHandler Handler = new HttpClientHandler()) {
        Handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
        //Handler.Credentials = CredentialCache.DefaultNetworkCredentials;
        Handler.UseDefaultCredentials = true;
        using (HttpClient Client = new HttpClient(Handler)) {
          Client.BaseAddress = BaseUri;


          HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, $"jsonrpc?{kodiRequest.RpcFullname}");
          Request.Content = new StringContent(ToJson(kodiRequest.RequestParameters), Encoding.UTF8, "application/json");

          Trace.WriteLine($"Content={ToJson(kodiRequest.RequestParameters)}");

          HttpResponseMessage Response = await Client.SendAsync(Request);

          string RetVal = await Response.Content.ReadAsStringAsync();
          Trace.WriteLine($"Response : {RetVal}");
          return RetVal;
        }
      }
    }

    private string ToJson(Dictionary<string, object> source) {
      if (source == null || source.Count == 0) {
        return "{}";
      }
      StringBuilder RetVal = new StringBuilder("{");
      foreach (KeyValuePair<string, object> KVPItem in source) {
        if (KVPItem.Value.GetType().IsGenericType) {
          RetVal.Append($"\"{KVPItem.Key}\" : ");
          RetVal.Append(ToJson((KVPItem.Value as Dictionary<string, object>)));
          RetVal.Append(", ");
          continue;
        }

        switch (KVPItem.Value.GetType().Name.ToLower()) {
          case "int32":
            RetVal.Append($"\"{KVPItem.Key}\" : {KVPItem.Value}, ");
            break;
          case "boolean":
          case "bool":
            RetVal.Append($"\"{KVPItem.Key}\" : {KVPItem.Value.ToString().ToLower()}, ");
            break;
          default:
            RetVal.Append($"\"{KVPItem.Key}\" : \"{KVPItem.Value}\", ");
            break;
        }

      }
      RetVal.Truncate(2);
      RetVal.Append("}");
      return RetVal.ToString();
    }
  }
}
