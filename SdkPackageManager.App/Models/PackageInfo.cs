using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SdkPackageManager.App.Models;

public class PackageInfo : INotifyPropertyChanged
{
    private string? _installedVersion;
    private PackageStatus _status;

    public string Name { get; }

    public string? InstalledVersion
    {
        get => _installedVersion;
        set
        {
            if (_installedVersion == value)
            {
                return;
            }

            _installedVersion = value;
            OnPropertyChanged();
        }
    }

    public string AvailableVersion { get; }

    public PackageStatus Status
    {
        get => _status;
        set
        {
            if (_status == value)
            {
                return;
            }

            _status = value;
            OnPropertyChanged();
        }
    }

    public PackageInfo(
        string name,
        string? installedVersion,
        string availableVersion)
    {
        Name = name;
        InstalledVersion = installedVersion;
        AvailableVersion = availableVersion;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}