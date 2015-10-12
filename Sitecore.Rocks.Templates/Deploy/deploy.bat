echo off

set projectName=%1
set projectBinDir=%2
set targetDir=%localappdata%\Sitecore\Sitecore.Rocks\Plugins\

echo copying plugin to %targetDir%	

if exist "%targetDir%%projectName%.dll" (
	echo rename old dll
    rename "%targetDir%%projectName%.dll" %projectName%.dll.old
)

xcopy /Y "%projectBinDir%*" "%targetDir%"