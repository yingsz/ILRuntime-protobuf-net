for /R "./protos"  %%f in (*.proto) do (
protogen.exe -i:%%f -o:../../Code/RiseProto/%%~nf.cs -ns:RiseProto 
)
pause


