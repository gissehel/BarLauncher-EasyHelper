rm -rf BarLauncher.EasyHelper/bin BarLauncher.EasyHelper/obj
dotnet.exe publish BarLauncher.EasyHelper/BarLauncher.EasyHelper.csproj -c Release -f netstandard2.0
dotnet.exe pack BarLauncher.EasyHelper/BarLauncher.EasyHelper.csproj -c Release

rm -rf BarLauncher.EasyHelper.Test.Mock/bin BarLauncher.EasyHelper.Test.Mock/obj
dotnet.exe publish BarLauncher.EasyHelper.Test.Mock/BarLauncher.EasyHelper.Test.Mock.csproj -c Release -f netstandard2.0
dotnet.exe pack BarLauncher.EasyHelper.Test.Mock/BarLauncher.EasyHelper.Test.Mock.csproj -c Release

rm -rf BarLauncher.EasyHelper.Wox/bin BarLauncher.EasyHelper.Wox/obj
dotnet.exe publish BarLauncher.EasyHelper.Wox/BarLauncher.EasyHelper.Wox.csproj -c Release -f net48
dotnet.exe pack BarLauncher.EasyHelper.Wox/BarLauncher.EasyHelper.Wox.csproj -c Release

rm -rf BarLauncher.EasyHelper.Flow.Launcher/bin BarLauncher.EasyHelper.Flow.Launcher/obj
dotnet.exe publish BarLauncher.EasyHelper.Flow.Launcher/BarLauncher.EasyHelper.Flow.Launcher.csproj -c Release -f net5.0-windows
dotnet.exe pack BarLauncher.EasyHelper.Flow.Launcher/BarLauncher.EasyHelper.Flow.Launcher.csproj -c Release

