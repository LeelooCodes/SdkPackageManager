using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkPackageManager.App.Interop
{
    public interface IPackageInterop
    {
        int CompareVersions(
            int firstMajor,
            int firstMinor,
            int firstPatch,
            int secondMajor,
            int secondMinor,
            int secondPatch
            );

        bool IsPackageNameValid(string? packageName );
    }
}
