@echo off

set /p Build=<version.txt

docker build . -t noa-contact-notifier:%Build%
docker save noa-contact-notifier:%Build% > .\noa-contact-notifier-%Build%.tar
