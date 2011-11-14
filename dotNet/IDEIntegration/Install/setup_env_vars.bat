if exist "%programfiles(x86)%" (
  set install_dir="%programfiles(x86)%\Raconteur"
) else (
  set install_dir="%programfiles%\Raconteur"
)