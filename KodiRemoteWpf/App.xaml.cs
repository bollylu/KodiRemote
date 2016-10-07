using BLTools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BLTools.MVVM;
using BLTools.Debugging;
using System.Reflection;
using System.IO;

namespace KodiRemoteWpf {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {

    public const string PARAM_LOGFILE = "log";
    public const string PARAM_LOGBASE = "logbase";
    public const string PARAM_CONFIG = "config";

    public const string DEFAULT_PROD_LOGBASE = @"c:\Logs";
    public const string DEFAULT_DEV_LOGBASE = @"c:\Logs";
    public const string DEFAULT_CONFIG = "config.xml";

    public static SplitArgs Args;

    public static bool InDesignMode {
      get {
        return !(Application.Current is App);
      }
    }

    private void Application_Startup(object sender, StartupEventArgs e) {
      Args = new SplitArgs(Environment.GetCommandLineArgs());

      Trace.AutoFlush = true;
      SetLogDestination();

      EventHandler<IntAndMessageEventArgs> DisplayProgressToTraceLog = (s, ea) => {
        if (!string.IsNullOrWhiteSpace(ea.Message)) {
          Trace.WriteLine(ea.Message);
        }
      };
      EventHandler<IntAndMessageEventArgs> DisplayErrorToTraceLog = (s, ea) => {
        Trace.WriteLine(ea.Message, Severity.Warning);
      };

      MVVMBase.OnExecutionProgress += DisplayProgressToTraceLog;
      MVVMBase.OnExecutionError += DisplayErrorToTraceLog;

      ApplicationInfo.ApplicationStart();

    }

    private void Application_Exit(object sender, ExitEventArgs e) {
      ApplicationInfo.ApplicationStop();
    }

    public static string GetPictureFullname(string name = "default", string extension = "png") {
      return $"/{Assembly.GetEntryAssembly().GetName().Name};component/Pictures/{name}.{extension}";
    }

    public static void SetLogDestination() {
      string LogBase;
      if (!System.Diagnostics.Debugger.IsAttached) {
        LogBase = Args.GetValue<string>(PARAM_LOGBASE, DEFAULT_PROD_LOGBASE);
      } else {
        LogBase = Args.GetValue<string>(PARAM_LOGBASE, DEFAULT_DEV_LOGBASE);
      }

      string LogFile = Args.GetValue<string>(PARAM_LOGFILE, "");

      string ConfigFile = Args.GetValue<string>(PARAM_CONFIG, DEFAULT_CONFIG);

      //Trace.Listeners.Clear();
      //Trace.Listeners.Add(new DefaultTraceListener());

      if (LogFile != "") {
        Trace.Listeners.Add(new TimeStampTraceListener(Path.Combine(LogBase, LogFile)));
      } else {
        string LogFilename = Path.Combine(LogBase, TraceFactory.GetTraceDefaultLogFilename());
        Trace.Listeners.Add(new TimeStampTraceListener(LogFilename));
      }

      foreach (TraceListener TraceListenerItem in Trace.Listeners) {
        if (TraceListenerItem is TimeStampTraceListener) {
          TimeStampTraceListener LocalTimestampTraceListener = TraceListenerItem as TimeStampTraceListener;
          LocalTimestampTraceListener.DisplayUserId = true;
          LocalTimestampTraceListener.DisplayComputerName = true;
        }
      }

    }


  }
}
