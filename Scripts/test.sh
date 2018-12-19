msbuild /p:Configuration=Debug ./Kabash/Kabash.sln 
dotnet vstest ./Kabash/Kabash.UnitTests/bin/Debug/Kabash.UnitTests.dll /InIsolation /logger:trx