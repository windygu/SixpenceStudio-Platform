nuget restore dotnet\SixpenceStudio.Platform.sln
$buildException = MSBuild.exe "dotnet\SixpenceStudio.Platform.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }

If (Test-Path nuget) { Remove-Item -Recurse -Force "nuget\*.nupkg" }

$lastVersion = nuget list SixpenceStudio.Core -Source http://nuget.karldu.cn/nuget
$lastVersion = $lastVersion -replace "SixpenceStudio.Core ","Current Version: "
Write-Output $lastVersion

$newVersion = Read-Host "Enter new version"

nuget pack dotnet\SixpenceStudio.Core\SixpenceStudio.Core.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory build
nuget pack dotnet\SixpenceStudio.WeChat\SixpenceStudio.WeChat.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory build

nuget push build\*.nupkg -Source http://nuget.karldu.cn/nuget -ApiKey 9F2E9384-F50A-43FF-8BA9-5D5E981C6561
