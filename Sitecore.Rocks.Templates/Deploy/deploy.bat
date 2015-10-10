echo off

set projectName=%1
set projectBinDir=%2
set targetDir=%localappdata%\Sitecore\Sitecore.Rocks\Plugins\

echo copying plugin to %targetDir%

rem if exist %targetDir%%projectName%.dll (
rem	echo delete old dll
rem    delete %remoteDll%
rem )

xcopy /Y %projectBinDir%%ProjectName%.dll %targetDir% 	