@echo off
cl /EHsc /std:c++17 main.cpp person.cpp
if errorlevel 1 (
    echo "COMPILE ERROR"
   exit /b %errorlevel%
)
main.exe