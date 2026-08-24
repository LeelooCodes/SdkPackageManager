using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkPackageManager.App.Interop;

public sealed class NativePackageInterop : IPackageInterop
{
    public int CompareVersions(
        int firstMajor,
        int firstMinor,
        int firstPatch,
        int secondMajor,
        int secondMinor,
        int secondPatch)
    {
        return NativeMethods.CompareVersions(
            firstMajor,
            firstMinor,
            firstPatch,
            secondMajor,
            secondMinor,
            secondPatch);
    }

    public bool IsPackageNameValid(string? packageName)
    {
        return NativeMethods.ValidatePackageName(packageName) != 0;
    }
}
