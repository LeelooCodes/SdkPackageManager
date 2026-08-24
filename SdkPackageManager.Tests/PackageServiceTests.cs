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

    //this proves: CompareVersions() -> -1 means Update available
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


    //this proves: CompareVersions() -> 0 means Installed
    [Fact]
    public void RefreshStatus_WhenVersionsMatch_SetsInstalled()
    {
        //Arrange
        FakePackageInterop interop = new()
        {
            ComparisonResult = 0
        };

        PackageService service = new(interop);

        PackageInfo package = new(
            "Audio Tools",
            "2.0.0",
            "2.0.0");

        //Act
        service.RefreshStatus(package);

        //Assert
        Assert.Equal(
            PackageStatus.Installed,
            package.Status);
    }

    //RefreshStatus() should encounter package.InstalledVersion is null, and return it before it ever asks the native layer to compare versions.
    [Fact]
    public void RefreshStatus_WhenPackageIsNotInstalled_SetsNotInstalled()
    {
        //Arrange
        FakePackageInterop interop = new();
       
        PackageService service = new(interop);

        PackageInfo package = new(
            "Network SDK",
            null,
            "1.1.0");

        //Act
        service.RefreshStatus(package);

        //Assert
        Assert.Equal(
            PackageStatus.NotInstalled,
            package.Status);
    }


    //Tests Install. After Install() sets the version it calls RefreshStatus().
    [Fact]
    public void Install_SetsInstalledVersionToAvailableVersion()
    {
        //Arrange
        FakePackageInterop interop = new();

        PackageService service = new(interop);

        PackageInfo package = new(
            "Network SDK",
            null,
            "1.0.0");

        //Act
        service.Install(package);

        //Assert
        Assert.Equal(
            PackageStatus.Installed,
            package.Status);
    }

    //Tests Update version
    [Fact]
    public void Update_SetsInstalledVersionToAvailableVersion()
    {
        //Arrange
        FakePackageInterop interop = new();

        PackageService service = new(interop);

        PackageInfo package = new(
            "Graphics SDK",
            "1.2.0",
            "1.4.0");

        //Act
        service.Update(package);

        //Assert
        Assert.Equal(
            "1.4.0",
            package.InstalledVersion);

        Assert.Equal(
            PackageStatus.Installed,
            package.Status);
    }



    //Tests remove
    [Fact]
    public void Remove_ClearsInstalledVersionAndSetsNotInstalled()
    {
        //Arrange
        FakePackageInterop interop = new();

        PackageService service = new(interop);

        PackageInfo package = new(
            "Debug tools",
            "3.2.0",
            "3.2.0");

        //Act
        service.Remove(package);

        //Assert
        Assert.Null(package.InstalledVersion);

        Assert.Equal(
            PackageStatus.NotInstalled,
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
