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
mvn clean install
pause

exit