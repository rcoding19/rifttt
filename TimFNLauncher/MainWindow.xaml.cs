// Decompiled with JetBrains decompiler
// Type: RiftLauncher.MainWindow
// Assembly: RiftLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03061649-5402-41ED-BA6A-E445B8CDF1BD
// Assembly location: C:\Users\Rafael Coelho\Desktop\kkkl\TimFN\launchers\TimFNLauncher.exe

using AdonisUI.Controls;
using Newtonsoft.Json;
using RiftLauncher.Helpers;
using RiftLauncher.Models;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace RiftLauncher
{
  public partial class MainWindow : AdonisWindow, IComponentConnector
  {
    internal PasswordBox SecretBox;
    internal TextBox InstallBox;
    private bool _contentLoaded;

    public MainWindow() => this.InitializeComponent();

    private void LoadForm(object sender, RoutedEventArgs e)
    {
      UserSettings userSettings = this.GetUserSettings();
      if (userSettings.CurrentInstall == null)
      {
        EpicInstall epicInstall = this.GetInstallList().InstallationList.Where<EpicInstall>((Func<EpicInstall, bool>) (install => install.AppName == "Fortnite")).First<EpicInstall>();
        if (epicInstall != null)
          userSettings.CurrentInstall = epicInstall.InstallLocation;
        this.UpdateSettings(userSettings);
      }
      this.InstallBox.Text = userSettings.CurrentInstall;
      this.SecretBox.Password = userSettings.Secret;
    }

    private void StartGame(object sender, RoutedEventArgs e)
    {
      UserSettings userSettings = this.GetUserSettings();
      if (userSettings.CurrentInstall == null)
        return;
      userSettings.CurrentInstall = this.InstallBox.Text;
      userSettings.Secret = this.SecretBox.Password;
      this.UpdateSettings(userSettings);
      Process process1 = ProcessHelper.StartProcess(userSettings.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe", true);
      Process process2 = ProcessHelper.StartProcess(userSettings.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_BE.exe", true);
      Process process3 = ProcessHelper.StartProcess(userSettings.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe", false, "-AUTH_TYPE=exchangecode -AUTH_LOGIN=unused -AUTH_PASSWORD=" + this.SecretBox.Password);
      this.Hide();
      process3.WaitForInputIdle();
      ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Platanium.dll"));
      Thread.Sleep(1000);
      ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Rift.dll"));
      process3.WaitForExit();
      this.Show();
      process1.Kill();
      process2.Kill();
    }

    private EpicInstallList GetInstallList()
    {
      string path = "C:\\ProgramData\\Epic\\UnrealEngineLauncher\\LauncherInstalled.dat";
      return !File.Exists(path) ? (EpicInstallList) null : JsonConvert.DeserializeObject<EpicInstallList>(File.ReadAllText(path));
    }

    private UserSettings GetUserSettings()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rift", "settings.json");
      if (!File.Exists(path))
      {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
          Directory.CreateDirectory(Path.GetDirectoryName(path));
        UserSettings userSettings = new UserSettings()
        {
          CurrentInstall = (string) null,
          Secret = ""
        };
        File.WriteAllText(path, JsonConvert.SerializeObject((object) userSettings));
      }
      return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(path));
    }

    private void UpdateSettings(UserSettings newSettings)
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rift", "settings.json");
      if (!File.Exists(path))
        return;
      File.WriteAllText(path, JsonConvert.SerializeObject((object) newSettings));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RiftLauncher;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.LoadForm);
          break;
        case 2:
          this.SecretBox = (PasswordBox) target;
          break;
        case 3:
          this.InstallBox = (TextBox) target;
          break;
        case 4:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.StartGame);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
