@echo off
@title Internet of things service installation
@color 2
@echo Installing Internet of things service, please wait...
@set relativeAutoUpdatePath=/AutoUpdate/AutoUpdateBox.App.exe
@set absoluteAutoUpdatePath=%cd%%relativeAutoUpdatePath%
@echo %absoluteAutoUpdatePath%
@nssm install IotAppUpdateService %absoluteAutoUpdatePath%
@timeout 9
@echo Starting the service
@nssm start IotAppUpdateService && echo Service started successfully
@pause