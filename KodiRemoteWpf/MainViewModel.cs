using BLTools.MVVM;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteWpf {
  public class MainViewModel : MVVMBase {

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



  }
}
