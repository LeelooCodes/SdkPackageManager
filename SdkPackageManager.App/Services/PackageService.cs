using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using SdkPackageManager.App.Interop;
using SdkPackageManager.App.Models;

namespace SdkPackageManager.App.Services;

public class PackageService
{
    private readonly IPackageInterop _packageInterop;

    public PackageService(IPackageInterop packageInterop)
    {
        _packageInterop = packageInterop
            ?? throw new ArgumentNullException(nameof(packageInterop));
    }


    public void RefreshStatus(PackageInfo package)
    {
        if (package.InstalledVersion is  null)
        {
            package.Status = PackageStatus.NotInstalled;
            return;
        }

        Version installed = Version.Parse(package.InstalledVersion);
        Version available = Version.Parse(package.AvailableVersion);

        int comparison = _packageInterop.CompareVersions(
            installed.Major,
            installed.Minor,
            installed.Build,
            available.Major,
            available.Minor,
            available.Build);

        package.Status = comparison < 0
            ? PackageStatus.UpdateAvailable
            : PackageStatus.Installed;
    }

    private bool IsPackageNameValid(PackageInfo package)
    {
        return _packageInterop.IsPackageNameValid(package.Name);
    }

    public void Install(PackageInfo package)
    {
        if (!IsPackageNameValid(package))
        {
            throw new ArgumentException(
                "Package contains an invalid name.",
                nameof(package));
        }

        package.InstalledVersion = package.AvailableVersion;
        RefreshStatus(package);
    }

    public void Update(PackageInfo package)
    {
        if (!IsPackageNameValid(package))
        {
            throw new ArgumentException(

                "Package contains an invalid name.",
                nameof(package));
        }

        package.InstalledVersion = package.AvailableVersion;
        RefreshStatus(package);
    }

    public void Remove(PackageInfo package)
    {
        if (!IsPackageNameValid(package))
        {
            throw new ArgumentException(

                "Package contains an invalid name.",
                nameof(package));
        }

        package.InstalledVersion = null;
        RefreshStatus(package);
    }
}
