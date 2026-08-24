using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkPackageManager.App.Interop;
using Xunit;

namespace SdkPackageManager.Tests;

public class NativeInteropIntegrationTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public void CompareVersions_ThroughNativeDll_ReturnsOlderForOlderVersion()
    {
        //Arrange
        IPackageInterop interop = new NativePackageInterop();

        //Act
        int result = interop.CompareVersions(
            1, 2, 0,
            1, 4, 0);

        //Assert
        Assert.Equal(-1, result);
    }
}
