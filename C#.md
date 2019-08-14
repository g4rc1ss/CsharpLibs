# C#

![CSharp Icono](https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png)

## CSharp
CSharp es un [lenguaje de programacion compilado](https://es.wikipedia.org/wiki/Lenguaje_de_programación_compilado) con la filosofia de hacer un codigo facil de leer y programar, con una sintaxis sencilla

CSharp se usa en diferentes entornos todos basados en .NET Framework o .NET CORE
- **.NET Framework** -> Es un conjunto de librerias que se usan con el fin de poder desarrollar aplicaciones rapidamente.

- **.NET CORE** -> Es un framework de codigo abierto, basado en .Net framework. Esta realizado desde cero practicamente y esta mas destinado a poder usar una plataforma como es ``.NET`` desde sistemas como MacOS y GNU_Linux
---
### Que framework usar?

Esta es una pregunta un poco mas complicada, yo personalmente apuesto por .NET CORE porque me gusta mas el rollo multiplataforma y que pueda crear una aplicacion y distribuirla/usarla en mi mac, mi windows y mi linux.

.NET Framework esta mas centrado en windows y tiene librerias muy potentes que todavia no han sido migradas a .NET CORE como por ejemplo la libreria WPF para el desarrollo de GUI etc. 

Si se desea hacer una aplicacion Desktop con el fin de correrla solo en plataformas windows sera mejor usar .net Framework porque esta mas hecha

En cambio si deseamos crear una aplicacion web (ASP) o aplicaciones en consola podriamos crear aplicaciones .``net CORE`` puesto que son aplicaciones multiplataforma y es algo que esta muy a futuro

---
### Diferencias entre Frameworks

- **NET Core es nuevo, y escrito prácticamente desde cero** -> NET Core es, en muchos sentidos, un reboot de .NET Framework y ha tenido que ser reescrito desde cero

- **.NET Core es open source** -> .NET Core es un proyecto completamente open source desde sus orígenes

- **.NET Core es multiplataforma** -> De esta forma, ahora sería totalmente posible, por ejemplo, programar una aplicación en Windows y ejecutarla en un Mac, o desarrollarla en Mac y ejecutarla en una distribución Debian de Linux.

- **En .NET Core el rendimiento es algo prioritario** -> Desde sus orígenes, en .NET Core el rendimiento ha sido siempre una prioridad
    >  Por dar algunas cifras, algo tan simple y recurrente como utilizar los métodos `IndexOf()` o `StartsWith()` sobre una cadena son un `200%` más rápidos en .NET Core que en .NET Framework. Un `ToString()` sobre el elemento de un enum gana un `600%` de rendimiento. `LINQ`es hasta un `300%` más eficiente en determinados puntos. `Lazy<T>` es un `500%` más rápido etc.

---
## Creacion, compilacion y ejecucion

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
