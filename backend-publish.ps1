function UpdatePackage($Version) {
	$Arr = $Version.split(".");
	$Arr[2] = [convert]::ToInt32($Arr[2], 10) + 1;
	return $Arr -join ".";
}

nuget restore SixpenceStudio.Platform.sln
$buildException = MSBuild.exe "SixpenceStudio.Platform.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }

If (Test-Path nuget) { Remove-Item -Recurse -Force "nuget\*.nupkg" }

$lastVersion = nuget list SixpenceStudio.Core -Source http://nuget.karldu.cn/nuget
$lastVersion = $lastVersion -replace "SixpenceStudio.Core ",""
$newVersion = UpdatePackage $lastVersion

Write-Host "New Version: $newVersion"

nuget pack SixpenceStudio.Core\SixpenceStudio.Core.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory nuget
nuget pack SixpenceStudio.WeChat\SixpenceStudio.WeChat.csproj -Version $newVersion -Properties Configuration=Release -OutputDirectory nuget

nuget push nuget\*.nupkg -Source http://nuget.karldu.cn/nuget -ApiKey 9F2E9384-F50A-43FF-8BA9-5D5E981C6561

Write-Host "Nuget Publish Success!"