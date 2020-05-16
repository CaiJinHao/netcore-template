@echo off
@title Internet of things service uninstalled
@color 2
@echo Uninstalling Internet of things services
@nssm stop IotAppUpdateService
@nssm stop IotAppService
@nssm remove IotAppUpdateService
@nssm remove IotAppService
@pause