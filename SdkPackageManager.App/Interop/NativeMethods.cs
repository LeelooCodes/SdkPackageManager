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

    [DllImport(
        DllName,
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern int ValidatePackageName(
        [MarshalAs(UnmanagedType.LPWStr)] string? packageName);     //it remembers that C++ expects a const wchar_t* packageName while C# has string. Those are not the same type. MarshalAs tells the P/Invoke marshaler:
    //Represent this managed C# string as a pointer to a null-terminated wide-character string when calling native code. LPWStr: LP -> Long pointer, historical windows terminology. W -> wide chars. Str -> string.
}