nuget restore platform\src\dotnet\SixpenceStudio.Platform.sln
$buildException = MSBuild.exe "platform\src\dotnet\SixpenceStudio.Platform.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }

If (Test-Path platform\build) { Remove-Item -Recurse -Force "platform\build\*.nupkg" }

nuget pack platform\src\dotnet\SixpenceStudio.BaseSite\SixpenceStudio.BaseSite.csproj -Version 1.0.0 -Properties Configuration=Release -OutputDirectory "platform\build"
nuget pack platform\src\dotnet\SixpenceStudio.Platform\SixpenceStudio.Platform.csproj -Version 1.0.0 -Properties Configuration=Release -OutputDirectory "platform\build"

nuget push platform\build\*.nupkg -Source http://www.dumiaoxin.top:8001 -ApiKey 9F2E9384-F50A-43FF-8BA9-5D5E981C6561
