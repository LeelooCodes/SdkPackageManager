using System.Windows;
using System.Collections.ObjectModel;
using SdkPackageManager.App.Models;
using SdkPackageManager.App.Services;
using SdkPackageManager.App.Interop;

namespace SdkPackageManager.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly PackageService _packageService = new(new NativePackageInterop());

    public ObservableCollection<PackageInfo> Packages { get; } = new()
    {
        new PackageInfo("Graphics SDK", "1.2.0", "1.4.0"),
        new PackageInfo("Audio Tools", "2.0.0", "2.0.0"),
        new PackageInfo("Network SDK", null, "1.1.0"),
        new PackageInfo("Debug Tools", "3.0.0", "3.2.0")
    };

    public MainWindow()
    {
        InitializeComponent();

        foreach (PackageInfo package  in Packages)
        {
            _packageService.RefreshStatus(package);
        }

        DataContext = this;
    }

    private PackageInfo? GetSelectedPackage()
    {
        if (PackageGrid.SelectedItem is PackageInfo package)
        {
            return package;
        }

        StatusMessageText.Text = "Select a package first.";
        return null;
    }

    private void Install_Click(object sender, RoutedEventArgs e)
    {
        PackageInfo? package = GetSelectedPackage();

        if (package is null)
        {
            return;
        }

        if (package.Status != PackageStatus.NotInstalled)
        {
            StatusMessageText.Text = $"{package.Name} is already installed.";
            return;
        }

        _packageService.Install(package);

        StatusMessageText.Text =
            $"Installed {package.Name} version {package.InstalledVersion}.";
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        PackageInfo? package = GetSelectedPackage();

        if (package is null)
        {
            return;
        }

        if (package.Status != PackageStatus.UpdateAvailable)
        {
            StatusMessageText.Text =
                $"{package.Name} does not have an available update.";

            return;
        }

        _packageService.Update(package);

        StatusMessageText.Text =
            $"Updated {package.Name} to version {package.InstalledVersion}.";
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        PackageInfo? package = GetSelectedPackage();

        if (package is null)
        {
            return;
        }

        if (package.Status == PackageStatus.NotInstalled)
        {
            StatusMessageText.Text = $"{package.Name} is not installed.";
            return;
        }

        _packageService.Remove(package);

        StatusMessageText.Text = $"Removed {package.Name}";
    }
}