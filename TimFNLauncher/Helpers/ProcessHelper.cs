// Decompiled with JetBrains decompiler
// Type: RiftLauncher.Helpers.ProcessHelper
// Assembly: RiftLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03061649-5402-41ED-BA6A-E445B8CDF1BD
// Assembly location: C:\Users\Rafael Coelho\Desktop\kkkl\TimFN\launchers\TimFNLauncher.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace RiftLauncher.Helpers
{
  public class ProcessHelper
  {
    public static Process StartProcess(string path, bool shouldFreeze, string extraArgs = "")
    {
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = path,
          Arguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=f7b9gah4h5380d10f721dd6a " + extraArgs
        }
      };
      process.Start();
      if (shouldFreeze)
      {
        foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
          ProcessHelper.SuspendThread(ProcessHelper.OpenThread(2, false, thread.Id));
      }
      return process;
    }

    public static void InjectDll(int processId, string path)
    {
      IntPtr hProcess = ProcessHelper.OpenProcess(1082, false, processId);
      IntPtr procAddress = ProcessHelper.GetProcAddress(ProcessHelper.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
      uint num1 = (uint) ((path.Length + 1) * Marshal.SizeOf(typeof (char)));
      IntPtr num2 = ProcessHelper.VirtualAllocEx(hProcess, IntPtr.Zero, num1, 12288U, 4U);
      ProcessHelper.WriteProcessMemory(hProcess, num2, Encoding.Default.GetBytes(path), num1, out UIntPtr _);
      ProcessHelper.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAddress, num2, 0U, IntPtr.Zero);
    }

    [DllImport("kernel32.dll")]
    public static extern int SuspendThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    public static extern int ResumeThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenThread(
      int dwDesiredAccess,
      bool bInheritHandle,
      int dwThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hHandle);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(
      int dwDesiredAccess,
      bool bInheritHandle,
      int dwProcessId);

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr VirtualAllocEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      uint dwSize,
      uint flAllocationType,
      uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      byte[] lpBuffer,
      uint nSize,
      out UIntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(
      IntPtr hProcess,
      IntPtr lpThreadAttributes,
      uint dwStackSize,
      IntPtr lpStartAddress,
      IntPtr lpParameter,
      uint dwCreationFlags,
      IntPtr lpThreadId);
  }
}
