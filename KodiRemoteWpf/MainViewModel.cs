using BLTools.MVVM;
using KodiRemoteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KodiRemoteWpf {
  public class MainViewModel : MVVMBase {

    public List<IKodiStation> AvailableKodiStations { get; set; } = new List<IKodiStation>();

    public IKodiStation KodiStation {
      get {
        return _KodiStation;
      }
      set {
        _KodiStation = value;
        Task.Run(async () => await _KodiStation.Connect())
            .ContinueWith(async (t) => {
              if (IsConnected) {
                CurrentPlayingItem = (await KodiStation.ActiveKodiPlayer.GetCurrentItem()) as TKodiItemAudio;
              }
            })
            .ContinueWith((t) => {
              NotifyPropertyChanged(nameof(KodiStation));
              NotifyPropertyChanged(nameof(ConnectionName));
              NotifyPropertyChanged(nameof(IsConnected));
              NotifyPropertyChanged(nameof(PictureConnectionStatus));
              NotifyPropertyChanged(nameof(ActivePlayerDescription));
              //NotifyPropertyChanged(nameof(CurrentPlayingItem));
            });
      }
    }
    private IKodiStation _KodiStation;

    public string ConnectionName {
      get {
        if (!IsConnected) {
          return "Not connected";
        }
        return KodiStation.DisplayName;
      }
    }

    public bool IsConnected {
      get {
        if (KodiStation == null) {
          return false;
        }
        return (KodiStation.IsConnected);
      }
    }
    //public bool IsNotConnected {
    //  get {
    //    return (!IsConnected);
    //  }
    //}

    public string CurrentTitle {
      get {
        return _CurrentTitle;
      }
      set {
        _CurrentTitle = value;
        NotifyPropertyChanged(nameof(CurrentTitle));
        NotifyPropertyChanged(nameof(CurrentFullTitle));
      }
    }
    private string _CurrentTitle;

    public string CurrentFullTitle {
      get {
        return $"{CurrentTrack}. {CurrentTitle}";
      }
    }

    public int CurrentTrack {
      get {
        return _CurrentTrack;
      }
      set {
        _CurrentTrack = value;
        NotifyPropertyChanged(nameof(CurrentTrack));
        NotifyPropertyChanged(nameof(CurrentFullTitle));
      }
    }
    private int _CurrentTrack;

    public string CurrentAlbum {
      get {
        return _CurrentAlbum;
      }
      set {
        _CurrentAlbum = value;
        NotifyPropertyChanged(nameof(CurrentAlbum));
      }
    }
    private string _CurrentAlbum;

    public string CurrentPicture {
      get {
        if (_CurrentPicture == null) {
          return App.GetPictureFullname("ImageNotFound");
        }
        return App.GetPictureFullname(_CurrentPicture);
      }
      set {
        _CurrentPicture = value;
        NotifyPropertyChanged(nameof(CurrentPicture));
      }
    }
    private string _CurrentPicture;

    public string CurrentArtists {
      get {
        return _CurrentArtists;
      }
      set {
        _CurrentArtists = value;
        NotifyPropertyChanged(nameof(CurrentArtists));
      }
    }
    private string _CurrentArtists;

    public string ActivePlayerDescription {
      get {
        if (!IsConnected) {
          return "";
        }
        return $"Player {KodiStation.ActiveKodiPlayer.KodiPlayerType.Value} is active";
      }
    }

    public TKodiItemAudio CurrentPlayingItem {
      get {
        return _CurrentPlayingItem;
      }
      set {
        _CurrentPlayingItem = value;
        CurrentArtists = string.Join(", ", _CurrentPlayingItem.Artists);
        CurrentTitle = _CurrentPlayingItem.Title;
        CurrentAlbum = _CurrentPlayingItem.Album;
        CurrentTrack = _CurrentPlayingItem.TrackNumber;
      }
    }
    private TKodiItemAudio _CurrentPlayingItem;

    private bool PlayerIsPaused {
      get {
        return _PlayerIsPaused;
      }
      set {
        _PlayerIsPaused = value;
        NotifyPropertyChanged(nameof(PlayerPausedVisibility));
        NotifyPropertyChanged(nameof(PlayerPlayingVisibility));
      }
    }
    private bool _PlayerIsPaused = true;

    public Visibility PlayerPausedVisibility {
      get {
          return PlayerIsPaused ? Visibility.Collapsed : Visibility.Visible;
      }
    }

    public Visibility PlayerPlayingVisibility {
      get {
        return PlayerIsPaused ? Visibility.Visible : Visibility.Collapsed;
      }
    }

    public TRelayCommand<IKodiStation> CommandConnect { get; set; }
    public TRelayCommand CommandHelpContact { get; private set; }
    public TRelayCommand CommandHelpAbout { get; private set; }

    public TRelayCommand CommandPause { get; private set; }
    public TRelayCommand CommandPlay { get; private set; }

    #region Interface pictures
    public string ContactPicture { get { return App.GetPictureFullname("help"); } }
    public string FileOpenPicture { get { return App.GetPictureFullname("FileOpen"); } }
    public string FileQuitPicture { get { return App.GetPictureFullname("FileQuit"); } }
    public string PictureButtonPrevious { get { return App.GetPictureFullname("ButtonPrevious"); } }
    public string PictureButtonNext { get { return App.GetPictureFullname("ButtonNext"); } }
    public string PictureButtonPause { get { return App.GetPictureFullname("ButtonPause"); } }
    public string PictureButtonPlay { get { return App.GetPictureFullname("ButtonPlay"); } }
    public string PictureButtonStop { get { return App.GetPictureFullname("ButtonStop"); } }
    public string PictureConnectionStatus {
      get {
        if (IsConnected) {
          return App.GetPictureFullname("PictureOk");
        } else {
          return App.GetPictureFullname("PictureNotOk");
        }
      }
    }
    #endregion Interface pictures

    public MainViewModel() : base() {
      _Initialize();
      _InitializeCommands();
    }
    public MainViewModel(IKodiStation kodiStation) {
      _Initialize();
      KodiStation = kodiStation;
      _InitializeCommands();
    }

    private void _Initialize() {
      AvailableKodiStations.Add(new TKodiStation("Salon", "osmc.newnet.priv", 8080));
      AvailableKodiStations.Add(new TKodiStation("PC Luc", "lucwks6.newnet.priv", 8090));
    }

    private void _InitializeCommands() {
      CommandConnect = new TRelayCommand<IKodiStation>((x) => { _CommandConnect(x); }, (x) => true);
      CommandPause = new TRelayCommand(async () => { await _CommandPause(); }, (x) => true);
      CommandPlay = new TRelayCommand(async () => { await _CommandPlay(); }, (x) => true);

      CommandHelpContact = new TRelayCommand(
        () => {
          MessageBox.Show("Support contact : Luc Bolly");
        },
        _ => { return true; }
      );
      CommandHelpAbout = new TRelayCommand(() => HelpAbout(), (x) => true);

    }

    private async Task _CommandPause() {
      if (!IsConnected) {
        return;
      }
      await KodiStation.ActiveKodiPlayer.PlayerPause();
      PlayerIsPaused = !PlayerIsPaused;
    }

    private async Task _CommandPlay() {
      if (!IsConnected) {
        return;
      }
      await KodiStation.ActiveKodiPlayer.PlayerPlay();
      CurrentPlayingItem = await KodiStation.ActiveKodiPlayer.GetCurrentItem() as TKodiItemAudio;
      PlayerIsPaused = !PlayerIsPaused;
    }

    private async void _CommandConnect(IKodiStation kodiStation) {
      await kodiStation.GetActivePlayers();
    }

    private async void _CommandGetPlayingItem() {
      if (!IsConnected) {
        return;
      }
      CurrentPlayingItem = await KodiStation.ActiveKodiPlayer.GetCurrentItem() as TKodiItemAudio;
    }


    private void HelpAbout() {
      StringBuilder Usage = new StringBuilder();
      Usage.AppendLine(string.Format("KodiRemoteWpf v{0}", Assembly.GetEntryAssembly().GetName().Version.ToString()));
      Usage.AppendLine(@"Usage: KodiRemoteWpf [/config=<config.xml> (default=config.xml)]");
      Usage.AppendLine(@"                       [/logbase=<[\\server\share\]path> (default=c:\logs)]");
      Usage.AppendLine(@"                       [/log=<filename.log> (default=KodiRemoteWpf.log)]");
      MessageBox.Show(Usage.ToString());
    }
  }
}
