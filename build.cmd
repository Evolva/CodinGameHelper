@echo off
.\packages\build\FAKE\tools\Fake build.fsx "%1"
exit /b %errorlevel%