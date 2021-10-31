// Decompiled with JetBrains decompiler
// Type: RiftLauncher.App
// Assembly: RiftLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03061649-5402-41ED-BA6A-E445B8CDF1BD
// Assembly location: C:\Users\Rafael Coelho\Desktop\kkkl\TimFN\launchers\TimFNLauncher.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace RiftLauncher
{
  public partial class App : Application
  {
    private bool _contentLoaded;

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
      Application.LoadComponent((object) this, new Uri("/RiftLauncher;component/app.xaml", UriKind.Relative));
    }

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
