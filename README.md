![LovewingLogo](LovewingLogo.png)

[![Build status](https://ci.appveyor.com/api/projects/status/838a3fvg62djv93f?svg=true)](https://ci.appveyor.com/project/sr229/lovewing)

## What's this repo about?

This repository would contain Project Lovewing. We accept contributions here.

## Building

### Windows

You can use Visual Studio, Rider, MonoDevelop or any other IDE that supports building C# projects.

### Linux

You can also use an IDE like Rider or MonoDevelop but if you prefer using Vim here's how to build from cli.

```bash
# Install mono
$ sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
$ echo "deb http://download.mono-project.com/repo/ubuntu xenial main" | sudo tee /etc/apt/sources.list.d/mono-official.list
$ sudo apt-get update
$ sudo apt-get install -y mono-complete

# Clone the repo
$ git clone https://github.com/ClarityMoe/Lovewing --recursive # use "-b dev" for development branch
$ cd Lovewing

# Restore NuGet packages
$ curl -O https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
$ mono nuget.exe restore

# Build the game
$ msbuild

# Run the game
$ mono Lovewing.Game/bin/Debug/Lovewing.Game.exe
```

## Disclaimer

Idol Resources and assets are by KLab/Bushiroad. All rights reserved.

osu!Framework is by Dean "peppy" Herbert of ppy Ltd. Licensed under MIT.

Xamarin, the underlying source code and the Xamarin logo are trademarks of Xamarin inc. All rights reserved.

Lovewing is under Eclipse Public License 1.0 (Source Code) and CC-BY-NC (Original assets).
