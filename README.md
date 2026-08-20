# Developer SDK Package Manager

A small Windows desktop application built to explore **WPF/.NET desktop development and managed/native interoperability between C# and C++**.

The application models a fictional developer SDK/package manager. It displays installed and available package versions and allows users to simulate installing, updating, and removing packages.

The project is deliberately generic and does not use or reproduce any proprietary SDKs, internal tooling, or company-specific information.

## Features

* Windows desktop UI built with **WPF and .NET 8**
* Package list using WPF `DataGrid` and data binding
* Simulated **Install**, **Update**, and **Remove** operations
* Live UI updates using `INotifyPropertyChanged`
* Package collection exposed through `ObservableCollection<T>`
* Native **C++ DLL**
* Real C# → C++ communication using **P/Invoke**
* Native version comparison used to determine package update status
* x64 native build configuration
* Automatic native DLL deployment using a Visual Studio/MSBuild post-build event

Example fictional packages include:

* Graphics SDK
* Audio Tools
* Network SDK
* Debug Tools

## Architecture

The solution contains two projects:

```text
SdkPackageManager
│
├── SdkPackageManager.App
│   │
│   ├── Interop
│   │   └── NativeMethods.cs
│   │
│   ├── Models
│   │   ├── PackageInfo.cs
│   │   └── PackageStatus.cs
│   │
│   ├── Services
│   │   └── PackageService.cs
│   │
│   ├── App.xaml
│   ├── MainWindow.xaml
│   └── MainWindow.xaml.cs
│
└── SdkPackageManager.Native
    ├── PackageNative.h
    ├── PackageNative.cpp
    └── ...
```

The application follows this basic flow:

```text
WPF UI
   ↓
C# application logic
   ↓
PackageService
   ↓
NativeMethods / PInvoke
   ↓
Native C++ DLL
```

### WPF application

`SdkPackageManager.App` is a .NET 8 WPF application responsible for:

* displaying package information;
* handling user interaction;
* maintaining package state;
* exposing package data to WPF through data binding;
* coordinating package operations through `PackageService`.

### Application service

`PackageService` contains package-related application logic.

It determines whether a package is:

* Not Installed
* Installed
* Update Available

Version comparison itself is delegated to the native library.

### Native interoperability

`NativeMethods.cs` defines the P/Invoke boundary between managed C# and native C++.

The native library exports:

```cpp
CompareVersions(...)
```

using C linkage and an exported DLL function.

C# declares the corresponding native method using `DllImport`:

```csharp
[DllImport(
    "SdkPackageManager.Native.dll",
    CallingConvention = CallingConvention.Cdecl,
    ExactSpelling = true)]
```

This creates a real managed/native execution path:

```text
C# managed code
      ↓
P/Invoke
      ↓
native C++ DLL
      ↓
version comparison
      ↓
integer result returned to C#
```

The native comparison returns:

```text
-1  first version is older
 0  versions are equal
 1  first version is newer
```

The C# service then converts that low-level result into an application-level `PackageStatus`.

## WPF concepts used

The project uses several core WPF concepts:

### XAML

Window layout and controls are defined separately from application logic using XAML.

### Data binding

The package `DataGrid` binds to:

```csharp
ObservableCollection<PackageInfo>
```

rather than being populated manually by UI code.

### Property change notification

`PackageInfo` implements:

```csharp
INotifyPropertyChanged
```

allowing changes such as an updated installed version or package status to be reflected immediately in the UI.

## Managed and native code

The solution intentionally contains both managed and native components.

**Managed code**

* C#
* .NET 8
* WPF
* executed under the .NET runtime

**Native code**

* C++
* compiled using MSVC
* built as an x64 Windows DLL
* executed directly as native machine code

P/Invoke provides the interoperability boundary between the two.

## Building the project

### Requirements

* Windows
* Visual Studio 2022
* **.NET desktop development** workload
* **Desktop development with C++** workload
* .NET 8 SDK

### Build

1. Clone the repository.
2. Open `SdkPackageManager.sln` in Visual Studio.
3. Ensure the solution configuration is `Debug | x64`.
4. Build the solution.
5. Set `SdkPackageManager.App` as the startup project if necessary.
6. Run the application.

The native C++ project is built as an x64 DLL.

A post-build step copies `SdkPackageManager.Native.dll` into the WPF application's output directory so that it can be located when P/Invoke loads the native library.

## Package operations

Package installation is intentionally simulated.

### Install

For a package that is not installed, its installed version becomes the currently available version.

### Update

For a package where a newer version is available, the installed version is updated to the available version.

### Remove

The installed version is cleared and the package returns to the `Not Installed` state.

The project does not download or install real SDKs or software.

## Purpose

This project was created as a focused learning exercise to gain hands-on experience with technologies outside my previous C#/C++ game-development work, particularly:

* traditional .NET Windows desktop development;
* WPF and XAML;
* WPF data binding;
* managed/native interoperability;
* P/Invoke and `DllImport`;
* native Windows DLLs;
* exported C++ functions;
* calling conventions;
* Visual Studio multi-project solutions;
* MSBuild-based project configuration.

The project is intentionally small so that its architecture and interoperability boundary remain easy to understand and explain.

## Possible future improvements

Potential extensions include:

* passing strings and structured data across the managed/native boundary;
* unit tests;
* MVVM and `ICommand`;
* JSON-backed package metadata;
* logging;
* search and filtering;
* CMake configuration for the native component;
* GitHub Actions CI;
* performance profiling.

These are intentionally outside the initial prototype scope.
