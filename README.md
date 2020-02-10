| Area      |      Badges  |
|:----------|:-------------|
| Build | [![Build status](https://ci.appveyor.com/api/projects/status/ry2o7n3as7j0axp8?svg=true)](https://ci.appveyor.com/project/joymon/dotnet-helpers)|
| Code | ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/joymon/dotnet-helpers) ![GitHub repo size](https://img.shields.io/github/repo-size/joymon/dotnet-helpers)  |
| Code quallity | [![Total alerts](https://img.shields.io/lgtm/alerts/g/joymon/dotnet-helpers.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/joymon/dotnet-helpers/alerts/) |
| Security | [![Known Vulnerabilities](https://snyk.io/test/github/joymon/dotnet-helpers/badge.svg)](https://snyk.io/test/github/joymon/dotnet-helpers) |
| Test |  [![codecov](https://codecov.io/gh/joymon/dotnet-helpers/branch/master/graph/badge.svg)](https://codecov.io/gh/joymon/dotnet-helpers) [![Test](https://img.shields.io/appveyor/tests/joymon/dotnet-helpers.svg)](https://ci.appveyor.com/project/joymon/dotnet-helpers) |
| Issues | [![Average time to resolve an issue](http://isitmaintained.com/badge/resolution/joymon/dotnet-helpers.svg)](http://isitmaintained.com/project/joymon/dotnet-helpers "Average time to resolve an issue") [![Percentage of issues still open](http://isitmaintained.com/badge/open/joymon/dotnet-helpers.svg)](http://isitmaintained.com/project/joymon/dotnet-helpers "Percentage of issues still open") |
| Deployment | [![Build status](https://img.shields.io/nuget/v/DotNet.Helpers.svg)](https://www.nuget.org/packages/DotNet.Helpers) [![Build status](https://img.shields.io/nuget/dt/DotNet.Helpers.svg)](https://www.nuget.org/packages/DotNet.Helpers) |

# DotNet.Helpers
Utilities and helper classes for faster .Net development by writing less code

# What is the purpose of this helper library
This is yet another library for writing less .Net code to develop applications. Mainly focused on the functional programming concepts. To be frank C# lacks so much things which are helpful in functional programming. This will try to bridge that.
Read the thoughts on why another library [here](why-library.md)
# Getting started

## Installation
```ps
Install-Package DotNet.Helpers -Version 0.0.30-beta
```

## Documentation

[Documentation site - development version](https://joymon.github.io/dotnet-helpers) (Please note this may not be inline with nuget package)

# Structure of this library

- There would be only one nuget package with one assembly planned at this moment. That is created from single source repo using multi targetting features.
- All the technological features will be there in that one assembly. If the application is targeting Full .Net framework, it will include all the helper classes for WinForms, WPF, ASP.Net etc.... This can be considered as drawback but the goal is to limit the pieces and have one nuget package with one assembly.

## Versioning
This library using [SemVer](https://semver.org/) for versioning. For the versions available, see the tags on this repository.

# Contributions

As normal via pull requests (PR). There is no slack channel, mail group or any other communication mechanism setup as its at the very early stage and not sure how long I will have interest into .Net
To my understanding the advent of JavaScript Everywhere (Node, Electron etc...) and Scala for distributed backend programming is reducing the .Net foot print. For bare metal programming, .Net was never there.

# Acknowledgments
- Github for freely hosting the source code
- AppVeyor for their free CI & CD support for all the open source projects.
