@echo off
FOR /f "tokens=*" %%i IN ('docker images -aq') DO docker rmi -f %%i
docker network prune -f
docker volume prune -f
pause