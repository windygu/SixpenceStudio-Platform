nuget restore platform\src\dotnet\SixpenceStudio.Platform.sln
$buildException = MSBuild.exe "platform\src\dotnet\SixpenceStudio.Platform.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }

If (Test-Path platform\build) { Remove-Item -Recurse -Force "platform\build\*.nupkg" }

$lastVersion = nuget list SixpenceStudio.Core -Source http://nuget.karldu.cn/nuget
$lastVersion = $lastVersion -replace "SixpenceStudio.Core ","Current Version: "
Write-Output $lastVersion

$newVersion = Read-Host "Enter new version"

nuget pack platform\src\dotnet\SixpenceStudio.Core\SixpenceStudio.Core.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory platform\build
nuget pack platform\src\dotnet\SixpenceStudio.WeChat\SixpenceStudio.WeChat.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory platform\build

nuget push platform\build\*.nupkg -Source http://nuget.karldu.cn/nuget -ApiKey 9F2E9384-F50A-43FF-8BA9-5D5E981C6561
