set FE=D:\Project\Drone\build\dcm-webapp
set BE=D:\Project\Drone\build\dcm-backend
set static=\src\main\resources\static\static
set distStatic=\dist\static
set distHtml=\dist\index.html
set FE_static=%FE%%distStatic%
set BE_static=%BE%%static%
set FE_distHtml=%FE%%distHtml%
set t=D:\Project\Drone\build\dcm-webapp\dist\index.html

cd /D %BE%
git pull origin develop
@RD /S /Q %BE_static%
cd /D %FE%
git pull origin develop
call npm run build
robocopy %FE_static% %BE_static% /E


@REM @REM cd /D %FE%
@REM @REM echo %FE_distHtml%
@REM @REM FOR /F "tokens=* delims=" %%x in (%FE_distHtml%) DO set Build=%%x
@REM @REM echo %Build%
@REM @echo off
@REM @REM FOR /F "tokens=* delims=" %%x in (%FE_distHtml%) DO set html=%%x
@REM @REM for /f "tokens=* delims=" %%x in (index.html) do set Build=%%x
@REM @REM @REM set /p Build=<new.html
@REM @REM echo %Build%
@REM COPY %FE_distHtml% %cd%\index.html
@REM move /y %cd%\index.html %cd%\index.txt
@REM for /f "tokens=* delims=" %%x in (index.txt) do set Build=%%x
@REM echo %Build%
@REM pause

exit