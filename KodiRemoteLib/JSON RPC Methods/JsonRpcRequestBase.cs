using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace KodiRemoteLib {
  public abstract class JsonRpcRequestBase : IKodiJsonRequest {

    public virtual string JsonRpcVersion { get { return "2.0"; } }
    public abstract string JsonRpcNamespace { get; }
    public abstract string JsonRpcMethod { get; }
    public virtual string JsonRpcFullname {
      get {
        return $"{JsonRpcNamespace}.{JsonRpcMethod}";
      }
    }

    public virtual Dictionary<string, object> RequestParameters {
      get {
        if (_RequestParameters==null) {
          _RequestParameters = new Dictionary<string, object>();
          _RequestParameters.Add("jsonrpc", "2.0");
          _RequestParameters.Add("method", $"{JsonRpcFullname}");
        }
        Dictionary<string, object> TempParameters = new Dictionary<string, object>(_RequestParameters);
        if (KodiParameters.Count != 0) {
          TempParameters.Add("params", KodiParameters);
        }
        if (Id !=-1) {
          TempParameters.Add("id", Id);
        } 
        return TempParameters;
      }
    }
    protected Dictionary<string, object> _RequestParameters;

    public Dictionary<string, object> KodiParameters { get; } = new Dictionary<string, object>();

    public int Id { get; set; } = -1;

    private int NextId() {
      return ++Id;
    }
    public JsonRpcRequestBase() {
    }

    public virtual async Task<T> Execute<T>(IKodiPlayer player) where T : IKodiJsonResponse, new() {

      string Data = await GetResponseString(player).ConfigureAwait(false);
      if (typeof(T).Name.ToLower() == "jsonrpcresponseempty") {
        return default(T);
      }

      T RetVal = new T();
      RetVal.Initialize(Data);
      return RetVal;

    }

    protected async Task<string> GetResponseString(IKodiPlayer player) {

      Trace.WriteLine($"Execution request : {JsonRpcFullname}");
      using (HttpClientHandler Handler = new HttpClientHandler()) {
        Handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
        Handler.UseDefaultCredentials = true;
        using (HttpClient Client = new HttpClient(Handler)) {
          Client.BaseAddress = player.BaseUri;

          HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, $"jsonrpc?{JsonRpcFullname}");
          string JsonContent = RequestParameters.ToJson();

          Request.Content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

          Trace.WriteLine($"Content={JsonContent}");
          string ResponseAsString="";

          try {
            HttpResponseMessage Response = await Client.SendAsync(Request).ConfigureAwait(false);
            ResponseAsString = await Response.Content.ReadAsStringAsync();
          } catch (Exception ex) {
            Trace.WriteLine($"Problem during HTTP command : {ex.Message}");
          }

          Trace.WriteLine($"Response : {ResponseAsString}");

          return ResponseAsString;
        }
      }

    }
  }
}
