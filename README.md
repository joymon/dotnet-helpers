| Area      |      Badges  |
|:----------|:-------------|
| Build | [![Build status](https://ci.appveyor.com/api/projects/status/ry2o7n3as7j0axp8?svg=true)](https://ci.appveyor.com/project/joymon/dotnet-helpers)|
| Code | ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/joymon/dotnet-helpers) ![GitHub repo size](https://img.shields.io/github/repo-size/joymon/dotnet-helpers) [![](https://tokei.rs/b1/github/joymon/dotnet-helpers)](https://github.com/joymon/dotnet-helpers) |
| Code quality | [![Total alerts](https://img.shields.io/lgtm/alerts/g/joymon/dotnet-helpers.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/joymon/dotnet-helpers/alerts/) |
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
Install-Package DotNet.Helpers -Version 0.0.95-beta
```

## Documentation

[Documentation site - development version](https://joymon.github.io/dotnet-helpers) (Please note this may not be inline with nuget package)

# Development

## Structure of this library

- There would be only one nuget package with one assembly planned at this moment. That is created from single source repo using multi targeting features.
- All the technological features will be there in that one assembly. If the application is targeting Full .Net framework, it will include all the helper classes for WinForms, WPF, ASP.Net etc.... This can be considered as drawback but the goal is to limit the pieces and have one nuget package with one assembly.

## Versioning
This library using [SemVer](https://semver.org/) for versioning. For the versions available, see the tags on this repository.

# Built with
- C#, .Net - Language and framework
- Visual Studio 2019 - IDE
- LGTM - Ensuring code quality
- Synk - Security analysis
- CodeCov - Test coverage
- AppVeyor - Continuous integration and delivery
- Nuget - Distributing library

# On the internet

- [Another .Net Helper library via nuget package system](https://joymonscode.blogspot.com/2018/08/another-net-helper-library-via-nuget.html)
- [Code coverage for .Net multi-targeting library](https://joymonscode.blogspot.com/2020/03/code-coverage-for-net-multi-targeting.html)

# Contributing

As normal via pull requests (PR). There is no slack channel, mail group or any other communication mechanism setup as this project is at the very early stage.I am not sure how long I will have interest into .Net. Full framework is already going out of main stream and only .Net Core will continue from .Net 5 onwards.

To my understanding the advent of JavaScript Everywhere (Node, Electron etc...) and Scala for distributed backend programming is reducing the .Net foot print. For bare metal programming, .Net was never there.

# Acknowledgments
- Github for freely hosting the source code
- AppVeyor for their freemium pricing model.
- All the developers whose technologies are used
