[![Build status](https://ci.appveyor.com/api/projects/status/ry2o7n3as7j0axp8?svg=true)](https://ci.appveyor.com/project/joymon/dotnet-helpers) [![Build status](https://img.shields.io/nuget/v/DotNet.Helpers.svg)](https://www.nuget.org/packages/DotNet.Helpers) [![Build status](https://img.shields.io/nuget/dt/DotNet.Helpers.svg)](https://www.nuget.org/packages/DotNet.Helpers) [![Test](https://img.shields.io/appveyor/tests/joymon/dotnet-helpers.svg)](https://img.shields.io/appveyor/tests/joymon/dotnet-helpers.svg)

# DotNet.Helpers
Utilities and helper classes for faster .Net development by writing less code

# What is the purpose of this helper library
This is yet another library for writing less .Net code to develop applications. Mainly focused on the functional programming concepts. To e frank C# lacks so much things which are helpful in functional programming. This will try to bride that.

# Why this helper library than others
There are so many other initiatives to generalize programming methods and are available as nuget packages. But those were not tailored to my use hence to start my own library. I could have joined with some of the libraries below but that needs extra time for communication and come to common conclusions. Feel free to try the below libraries too and sometimes those might be better suitable to you

## General helpers

- [https://github.com/Jishun/DotNetUtils](https://github.com/Jishun/DotNetUtils)
- [https://github.com/anonymousthing/angrybracket](https://github.com/anonymousthing/angrybracket)
- [https://github.com/tallesl/net-libraries-that-make-your-life-easier - Another list](https://github.com/tallesl/net-libraries-that-make-your-life-easier)

## Functional helpers

- [https://github.com/Hallmanac/Funqy-CSharp](https://github.com/Hallmanac/Funqy-CSharp)
- [https://github.com/NoelKennedy/scalesque](https://github.com/NoelKennedy/scalesque)
- [https://github.com/louthy/language-ext](https://github.com/louthy/language-ext)

# Structure of this library

- There would be only one nuget package with one assembly planned at this moment. That is created from single source repo using multi targetting features.
- All the technological features will be there in that one assembly. If the application is targeting Full .Net framework, it will include all the helper classes for WinForms, WPF, ASP.Net etc.... This can be considered as drawback but the goal is to limit the pieces and have one nuget package with one assembly.

# Contributions

As normal via pull requests (PR). There is no slack channel, mail group or any other communication mechanism setup as its at the very early stage and not sure how long I will have interest into .Net
To my understanding the advent of JavaScript Everywhere (Node, Electron etc...) and Scala for distributed backend programming is reducing the .Net foot print. For bare metal programming, .Net was never there.
