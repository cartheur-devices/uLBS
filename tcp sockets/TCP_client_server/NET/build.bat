@echo ************************************************************************
@echo ************************************************************************
@echo ************************************************************************
@call "c:\Program Files\Microsoft Visual Studio 8\VC\vcvarsall.bat" x86

@cd FileSystemClient
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd FileSystemServer
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd RemoteDesktopClient
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd RemoteDesktopServer
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd TCPClient
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd TCPClientCmd
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd TCPClientWin
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..

@cd TCPServer
@msbuild /t:build /p:Configuration=Release /nologo /v:m
@cd ..
