using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KodiRemoteWpf {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    #region Public static variables
    /// <summary>
    /// public global reference to the main window
    /// </summary>
    public static MainWindow Self;
    public static NetworkCredential CurrentUserCredential;
    #endregion Public static variables

    #region Public properties
    public MainViewModel MainWindowViewModel { get; set; }
    #endregion Public properties

    public MainWindow() {
      InitializeComponent();
      MainWindowViewModel = new MainViewModel();
      this.DataContext = MainWindowViewModel;
    }

    private void mnuFileQuit_Click(object sender, RoutedEventArgs e) {
      Application.Current.Shutdown();
    }
  }
}
