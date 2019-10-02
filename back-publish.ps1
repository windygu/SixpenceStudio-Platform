
Write-Message "Nuget restoring ..."
nuget restore platform\src\dotnet\SixpenceStudio-Platform\SixpenceStudio-Platform.sln
$buildException = MSBuild.exe platform\src\dotnet\SixpenceStudio-Platform\SixpenceStudio-Platform.sln
If (! $?) { Throw $buildException }
Write-Success-Message "Rebuild success."

Write-Message "Clearing build folder ..."
If (Test-Path platform\build) { Remove-Item -Recurse -Force "platform\build\*.nupkg" }
Write-Success-Message "Clear build folder successfully.

nuget pack platform\src\dotnet\SixpenceStudio-Platform\Platform.Core\Platform.Core.csproj -Version 2.1.0 -Properties Configuration=Release
Write-Success-Message "Nuget pack success."

Write-Message "Nuget pushing ..."
nuget push platform\build\*.nupkg -Source http://www.dumiaoxin.top:8001/nuget
