
@echo off

REM !!! Generated by the fmp-cli 1.33.0.  DO NOT EDIT!

md Assloud\Assets\3rd\fmp-xtc-assloud

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-assloud-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\Assloud\Assets\3rd\fmp-xtc-assloud\
