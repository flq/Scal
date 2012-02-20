msbuild Scal.sln /p:Configuration=Release /verbosity:m
nuget pack .\Scal\Scal.csproj -Prop Configuration=Release
pause
