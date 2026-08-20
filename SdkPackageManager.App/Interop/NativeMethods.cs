using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace SdkPackageManager.App.Interop;

internal static class NativeMethods
{
    private const string DllName = "SdkPackageManager.Native.dll";

    [DllImport(
        DllName,
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern int CompareVersions(
        int firstMajor,
        int firstMinor,
        int firstPatch,
        int secondMajor,
        int secondMinor,
        int secondPatch);
}