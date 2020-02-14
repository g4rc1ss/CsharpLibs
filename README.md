## Creación, compilación y ejecución

````
C:\Test>dotnet new console
Preparándose...
La plantilla "Console Application" se creó correctamente.

Procesando acciones posteriores...
Ejecutando "dotnet restore" en C:\Test\Test.csproj...
  Restaurando paquetes para C:\Test\Test.csproj...
  Generación de archivo MSBuild C:\Test\obj\Test.csproj.nuget.g.props.
  Generación de archivo MSBuild C:\Test\obj\Test.csproj.nuget.g.targets.
  Restauración realizada en 147,78 ms para C:\Test\Test.csproj.

Restauración correcta.
````

````
C:\Test>dotnet build
Microsoft (R) Build Engine versión 15.9.20+g88f5fadfbe para .NET Core
Copyright (C) Microsoft Corporation. Todos los derechos reservados.

  Restauración realizada en 23,44 ms para C:\Test\Test.csproj.
  Test -> C:\Test\bin\Debug\netcoreapp2.2\Test.dll

Compilación correcta.
    0 Advertencia(s)
    0 Errores

Tiempo transcurrido 00:00:00.52
````

````
C:\Test>dotnet run
Hello World!

C:\Test>_
````
