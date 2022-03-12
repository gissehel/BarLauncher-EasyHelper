#!/usr/bin/env bash

rm -rf ./*/bin ./*/obj ./build ./nupkg

dotnet.exe pack BarLauncher-EasyHelper.sln -c Debug -p:Version=$(cat VERSION)-$(date +%s)

for nupkg in nupkg/*.nupkg
do
    nuget.exe add "${nupkg}" -source "${NUGET_REPO}"
done
