using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public class TKodiPlayerType {

    public string Value {
      get {
        return _Value.ToString();
      }
    }

    private EKodiPlayerType _Value = EKodiPlayerType.unknown;

    private void SetValue(EKodiPlayerType kodiPlayerType) {
      _Value = kodiPlayerType;
    }

    public static TKodiPlayerType Audio {
      get {
        TKodiPlayerType RetVal = new TKodiPlayerType();
        RetVal.SetValue(EKodiPlayerType.audio);
        return RetVal;
      }
    }

    public static TKodiPlayerType Video {
      get {
        TKodiPlayerType RetVal = new TKodiPlayerType();
        RetVal.SetValue(EKodiPlayerType.video);
        return RetVal;
      }
    }

    public static TKodiPlayerType Unknown {
      get {
        TKodiPlayerType RetVal = new TKodiPlayerType();
        RetVal.SetValue(EKodiPlayerType.unknown);
        return RetVal;
      }
    }

  }
}
