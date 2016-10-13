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
  public class TKodiStation : IKodiStation {

    #region --- Public properties ------------------------------------------------------------------------------
    public string Name { get; set; }
    public string DnsName { get; set; }
    public string DisplayName {
      get {
        return $"{Name} - {DnsName}:{Port}";
      }
    }
    public IPHostEntry IpHostEntry { get; set; } = new IPHostEntry();
    public IPAddress Ip => IpHostEntry.AddressList.FirstOrDefault();
    public int Port { get; set; }
    public Uri BaseUri {
      get {
        return new Uri($"http://{DnsName}:{Port}");
      }
    }
    public List<IKodiPlayer> KodiPlayers { get; } = new List<IKodiPlayer>();
    public IKodiPlayer ActiveKodiPlayer => KodiPlayers.FirstOrDefault(x => x.IsActive);

    public bool IsConnected {
      get {
        return ActiveKodiPlayer != null;
      }
    }
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TKodiStation() { }

    public TKodiStation(string name, string dnsName, int port = 8080) {
      Name = name;
      DnsName = dnsName;
      IpHostEntry = Dns.GetHostEntry(dnsName);
      Port = port;
    }

    public void Dispose() {

    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public async Task Connect() {
      await GetActivePlayers().ConfigureAwait(false);
    }

    //public async Task<T> Execute<T>(JsonRpcRequestBase kodiRequest) {

    //  Trace.WriteLine($"Execution request : {kodiRequest.JsonRpcFullname}");
    //  using (HttpClientHandler Handler = new HttpClientHandler()) {
    //    Handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
    //    //Handler.Credentials = CredentialCache.DefaultNetworkCredentials;
    //    Handler.UseDefaultCredentials = true;
    //    using (HttpClient Client = new HttpClient(Handler)) {
    //      Client.BaseAddress = BaseUri;


    //      HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, $"jsonrpc?{kodiRequest.JsonRpcFullname}");
    //      string JsonContent = kodiRequest.RequestParameters.ToJson();

    //      Request.Content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

    //      Trace.WriteLine($"Content={JsonContent}");

    //      HttpResponseMessage Response = await Client.SendAsync(Request);

    //      string ResponseAsString = await Response.Content.ReadAsStringAsync();

    //      Trace.WriteLine($"Response : {ResponseAsString}");

    //      var JsonResult = JObject.Parse(ResponseAsString);

    //      switch (typeof(T).Name) {
    //        case "Kodi_ActivePlayer":
    //          Kodi_ActivePlayer RetVal = new Kodi_ActivePlayer();
    //          RetVal.PlayerId = (int)JsonResult.SelectToken("result[0].playerid");
    //          RetVal.PlayerType = (string)JsonResult.SelectToken("result[0].type");
    //          return (T)Convert.ChangeType(RetVal, typeof(T));
    //      }

    //      return default(T);
    //    }
    //  }
    //}

    //public async void PlayerStop(KodiResponse_ActivePlayers player) {
    //  Player_Stop RpcPlayerStop = new Player_Stop(player.PlayerId);
    //  await RpcPlayerStop.Execute<JsonRpcResponseEmpty>(this);
    //}

    //public async void PlayerPlay(KodiResponse_ActivePlayers player) {
    //  Player_Play RpcPlayerPlay = new Player_Play(player.PlayerId);
    //  await RpcPlayerPlay.Execute<JsonRpcResponseEmpty>(this);
    //}

    //public async void PlayerPause(KodiResponse_ActivePlayers player) {
    //  Player_Pause RpcPlayerPause = new Player_Pause(player.PlayerId);
    //  await RpcPlayerPause.Execute<JsonRpcResponseEmpty>(this);
    //}

    public async Task GetActivePlayers() {
      KodiPlayers.Clear();
      Player_GetActivePlayers RpcPlayerGetActivePlayers = new Player_GetActivePlayers();
      await RpcPlayerGetActivePlayers.Execute<KodiResponse_ActivePlayers>(this)
        .ContinueWith(t => {
          foreach (KeyValuePair<int, string> KodiPlayerItem in t.Result.ActivePlayers) {
            if (KodiPlayerItem.Value == TKodiPlayerType.Audio.Value) {
              KodiPlayers.Add(new TKodiPlayerAudio(this, KodiPlayerItem.Key));
              continue;

            } else if (KodiPlayerItem.Value == TKodiPlayerType.Video.Value) {
              KodiPlayers.Add(new TKodiPlayerVideo(this, KodiPlayerItem.Key));
              continue;

            }
          }
        }
      ).ConfigureAwait(false);

    }

    public async Task SetPartyMode() {
      Player_SetPartyMode RpcPlayerSetPartyMode = new Player_SetPartyMode();
      await RpcPlayerSetPartyMode.Execute<JsonRpcResponseEmpty>(this)
        .ContinueWith(async t => {
          await GetActivePlayers();
        }
      );

    }

    //public async Task<KodiResponse_CurrentlyPlayedItem> GetCurrentItem(KodiResponse_ActivePlayers player) {
    //  string[] Properties = new string[] { "title", "artist" };
    //  Player_GetItem RpcPlayerGetItem = new Player_GetItem(Properties, player.PlayerId);
    //  KodiResponse_CurrentlyPlayedItem CurrentItem = await RpcPlayerGetItem.Execute<KodiResponse_CurrentlyPlayedItem>(this);
    //  return CurrentItem;
    //}

    //#region --- Singletons --------------------------------------------
    //public Player_GetActivePlayers RPC_Player_GetActivePlayer {
    //  get {
    //    if (_RPC_Player_GetActivePlayer == null) {
    //      _RPC_Player_GetActivePlayer = new Player_GetActivePlayers();
    //    }
    //    return _RPC_Player_GetActivePlayer;
    //  }
    //}
    //private Player_GetActivePlayers _RPC_Player_GetActivePlayer;

    //public Player_PlayPause RPC_Player_PlayPause {
    //  get {
    //    if (_RPC_Player_PlayPause == null) {
    //      _RPC_Player_PlayPause = new Player_PlayPause();
    //    }
    //    return _RPC_Player_PlayPause;
    //  }
    //}
    //private Player_PlayPause _RPC_Player_PlayPause;

    //public Player_Play RPC_Player_Play {
    //  get {
    //    if (_RPC_Player_Play == null) {
    //      _RPC_Player_Play = new Player_Play();
    //    }
    //    return _RPC_Player_Play;
    //  }
    //}
    //private Player_Play _RPC_Player_Play;

    //public Player_Stop RPC_Player_Stop {
    //  get {
    //    if (_RPC_Player_Stop == null) {
    //      _RPC_Player_Stop = new Player_Stop();
    //    }
    //    return _RPC_Player_Stop;
    //  }
    //}
    //private Player_Stop _RPC_Player_Stop;

    //public Player_Pause RPC_Player_Pause {
    //  get {
    //    if (_RPC_Player_Pause == null) {
    //      _RPC_Player_Pause = new Player_Pause();
    //    }
    //    return _RPC_Player_Pause;
    //  }
    //}
    //private Player_Pause _RPC_Player_Pause;

    //public Player_SetPartyMode RPC_Player_SetPartyMode {
    //  get {
    //    if (_RPC_Player_SetPartyMode == null) {
    //      _RPC_Player_SetPartyMode = new Player_SetPartyMode();
    //    }
    //    return _RPC_Player_SetPartyMode;
    //  }
    //}
    //private Player_SetPartyMode _RPC_Player_SetPartyMode; 
    //#endregion --- Singletons --------------------------------------------
  }
}
