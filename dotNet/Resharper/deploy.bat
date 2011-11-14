set outdir="%programfiles(x86)%\JetBrains\ReSharper\v6.0\Bin\Plugins\Raconteur"

rd %outdir% /q /s
mkdir %outdir%

xcopy /y bin\debug\*Raconteur*.* %outdir%

rem start ..\Raconteur.sln