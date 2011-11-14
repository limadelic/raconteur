call setup_env_vars

copy ..\bin\Debug\IDEIntegration\*.* ..\..\live /y
copy ..\bin\Debug\ItemTemplates\Templates.zip ..\..\live\RaconteurFeature.zip /y


copy ..\bin\Debug\IDEIntegration\*.* %install_dir% /y

pause