using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkPackageManager.App.Interop;
using SdkPackageManager.App.Models;
using SdkPackageManager.App.Services;

namespace SdkPackageManager.Tests;

public class PackageServiceTests
{
    [Fact]
    public void RefreshStatus_WhenInstalledVersionIsOlder_SetsUpdateAvailable()
    {
        //arrange
        FakePackageInterop interop = new()
        {
            ComparisonResult = -1
        };

        PackageService service = new(interop);

        PackageInfo package = new(
            "Graphics SDK",
            "1.2.0",
            "1.4.0");


        //act
        service.RefreshStatus(package);


        //assert
        Assert.Equal(
            PackageStatus.UpdateAvailable,
            package.Status);
    }

    private sealed class FakePackageInterop : IPackageInterop
    {
        public int ComparisonResult { get; set; }

        public int CompareVersions(
            int firstMajor,
            int firstMinor,
            int firstPatch,
            int secondMajor,
            int secondMinor,
            int secondPatch)
        {
            return ComparisonResult;
        }

        public bool IsPackageNameValid(string? packageName)
        {
            return true;
        }
    }
}
