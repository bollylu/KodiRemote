using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Net;
using System.Net.Http;

namespace KodiRemoteLib {
  public class KodiPlayer {

    public string Name { get; set; }
    public string DnsName { get; set; }
    public IPHostEntry Ip { get; set; }
    public int Port { get; set; }

    public KodiPlayer() { }

    public KodiPlayer(string name, string dnsName, int port=8080) {
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

    public async Task<string> Execute(KodiRpcBase kodiRequest) {
      Dictionary<string, string> Parameters = new Dictionary<string, string>();
      Parameters.Add("jsonrpc", "2.0");
      Parameters.Add("method", $"{kodiRequest.RpcFullname}");
      Parameters.Add("id", "1");

      using (HttpClient Client = new HttpClient()) {
        Client.BaseAddress = new Uri($"http://{DnsName}:{Port}");

        HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, "jsonrpc");
        Request.Content = new StringContent(ToJson(Parameters), Encoding.UTF8, "application/json");

        HttpResponseMessage Response = await Client.SendAsync(Request, HttpCompletionOption.ResponseContentRead);

        return await Response.Content.ReadAsStringAsync();
      }
    }

    private string ToJson(Dictionary<string,string> source) {
      if (source==null ||source.Count==0) {
        return "{}";
      }
      StringBuilder RetVal = new StringBuilder("{");
      foreach(KeyValuePair<string, string> KVPItem in source) {
        RetVal.Append($"\"{KVPItem.Key}\" : \"{KVPItem.Value}\",");
      }
      RetVal.Truncate(1);
      RetVal.Append("}");
      return RetVal.ToString();
    }
  }
}
