REM installing the font files from within the role root folder fonts
REM put your fonts there

net user fontinstaller InstFont3tC00$ /add

net localgroup Administrators fontinstaller /add

cd "%ROLEROOT%\approot\fonts"

echo running psexec ... >> test.txt

start /w psexec -accepteula -u fontinstaller -p InstFont3tC00$  fontreg.exe /copy 

net user  fontinstaller /delete

exit /b 0


