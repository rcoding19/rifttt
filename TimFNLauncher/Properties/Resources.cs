// Decompiled with JetBrains decompiler
// Type: RiftLauncher.Properties.Resources
// Assembly: RiftLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03061649-5402-41ED-BA6A-E445B8CDF1BD
// Assembly location: C:\Users\Rafael Coelho\Desktop\kkkl\TimFN\launchers\TimFNLauncher.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace RiftLauncher.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (RiftLauncher.Properties.Resources.resourceMan == null)
          RiftLauncher.Properties.Resources.resourceMan = new ResourceManager("RiftLauncher.Properties.Resources", typeof (RiftLauncher.Properties.Resources).Assembly);
        return RiftLauncher.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => RiftLauncher.Properties.Resources.resourceCulture;
      set => RiftLauncher.Properties.Resources.resourceCulture = value;
    }
  }
}
