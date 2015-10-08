set remoteDll="%localappdata%\Sitecore\Sitecore.Rocks\Plugins\$(ProjectName).dll"
if exist remoteDll (
    rename %remoteDll%
)
copy /Y "$(TargetDir)$(ProjectName).dll" "%localappdata%\Sitecore\Sitecore.Rocks\Plugins\$(ProjectName).dll"