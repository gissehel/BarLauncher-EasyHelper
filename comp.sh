rm -rf ./*/bin ./*/obj ./build

dotnet.exe build BarLauncher-EasyHelper.sln -c Release
dotnet.exe pack BarLauncher-EasyHelper.sln -c Release

