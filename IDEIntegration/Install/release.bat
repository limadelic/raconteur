copy ..\bin\Debug\IDEIntegration\*.* ..\..\live /y
copy ..\bin\Debug\ItemTemplates\Templates.zip ..\..\live\RaconteurFeature.zip /y

if "%programfiles(x86)%"=="" (
  copy ..\bin\Debug\IDEIntegration\*.* "%programfiles%\Raconteur" /y
) else (
  copy ..\bin\Debug\IDEIntegration\*.* "%programfiles(x86)%\Raconteur" /y
)

pause