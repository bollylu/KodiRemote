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

    public IKodiStation KodiStation {
      get {
        return _KodiStation;
      }
      set {
        _KodiStation = value;
        NotifyPropertyChanged(nameof(KodiStation));
        NotifyPropertyChanged(nameof(Connection));
        NotifyPropertyChanged(nameof(IsConnected));
        NotifyPropertyChanged(nameof(IsNotConnected));
      }
    }
    private IKodiStation _KodiStation;

    public string Connection {
      get {
        if (!IsConnected) {
          return "Not connected";
        }
        return $"{KodiStation.DnsName}:{KodiStation.Port}";
      }
    }

    public bool IsConnected {
      get {
        return (KodiStation != null);
      }
    }
    public bool IsNotConnected {
      get {
        return (!IsConnected);
      }
    }

    public string CurrentTitle {
      get {
        return _CurrentTitle;
      }
      set {
        _CurrentTitle = value;
        NotifyPropertyChanged(nameof(CurrentTitle));
      }
    }
    private string _CurrentTitle;

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

    public string PictureButtonPrevious { get { return App.GetPictureFullname("ButtonPrevious"); } }
    public string PictureButtonNext { get { return App.GetPictureFullname("ButtonNext"); } }
    public string PictureButtonPause { get { return App.GetPictureFullname("ButtonPause"); } }
    public string PictureButtonPlay { get { return App.GetPictureFullname("ButtonPlay"); } }
    public string PictureButtonStop { get { return App.GetPictureFullname("ButtonStop"); } }

    private string _CurrentPicture;

    public TRelayCommand CommandConnect { get; set; }
    public TRelayCommand CommandHelpContact { get; private set; }
    public TRelayCommand CommandHelpAbout { get; private set; }

    #region Interface pictures
    public string ContactPicture {
      get {
        return App.GetPictureFullname("help");
      }
    }
    public string FileOpenPicture {
      get {
        return App.GetPictureFullname("FileOpen");
      }
    }
    public string FileQuitPicture {
      get {
        return App.GetPictureFullname("FileQuit");
      }
    }
    #endregion Interface pictures


    public MainViewModel() : base() {
      _InitializeCommands();
    }

    public MainViewModel(IKodiStation kodiStation) {
      KodiStation = kodiStation;
      _InitializeCommands();
    }

    private void _InitializeCommands() {
      CommandConnect = new TRelayCommand(() => { _CommandConnect(); }, (x) => true);
      CommandHelpContact = new TRelayCommand(
        () => {
          MessageBox.Show("Support contact : Luc Bolly");
        },
        _ => { return true; }
      );
      CommandHelpAbout = new TRelayCommand(() => HelpAbout(), (x) => true);

    }

    private void _CommandConnect() {

      KodiStation = new TKodiStation("OSMC", "osmc.newnet.priv", 8080);

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
