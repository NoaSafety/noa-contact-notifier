@echo off
FOR /f "tokens=*" %%i IN ('docker ps -aq') DO docker rm -f %%i
docker network prune -f
docker volume prune -f
pause