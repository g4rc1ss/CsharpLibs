[![Build Status](https://dev.azure.com/asierGarciss/Tecnologia%20.NET/_apis/build/status/CsharpLibs?branchName=master)](https://dev.azure.com/asierGarciss/Tecnologia%20.NET/_build/latest?definitionId=9&branchName=master)

# Introduction 
Conjunto de librerías desarrolladas en .Net Standard o .Net CORE

Cuando se compile los proyectos en modo `Release` se almacenarán los paquetes Nuget en la carpeta `NugetPackages`

# Getting Started
> Installation process
1. Instalaremos Visual Studio con los paquetes de desarrollo .Net multiplataforma
2. Instalaremos un cliente git como GitHub Desktop

> Software dependencies
- **Databases.csproj** -> `System.Data.SQLite.Core`

# Build and Test
Los proyectos tienen que ir ubicados en la carpeta `01_CsharpLibs`, en ella habrá dos carpetas
1. **NetCore**: En esta carpeta se ubicarán todas las librerías desarrolladas en `.Net Core`
2. **NetStandard**: En esta se ubicarán los desarrollados en `.NetStandard`

Se intentará usar siempre que se pueda .Net Standard puesto que es una librería portable

El uso y creación de Test es obligatorio, con ellos se demostrará y comprobará que las librerías funcionan y 
realizan las acciones para las que están destinadas.

Los Test se tendrán que ser fiables y deberán de llamarse igual que el proyecto con "Test" por delante.
De tal manera que quedaría `TestNombreLibreria`.

# Pull Requests

Para crear la Pull Requests la podemos crear desde el cliente git o desde la pagina de github

Agregamos el Título y la descripción del Pull Requests y la creamos. Esta pasara por un proceso de validación
automático que ejecutará los Test y compilará la solución. Si esos dos procesos no fallan  se subirán los paquetes
NuGet generados automáticamente para poder ser descargados mas adelante.

# Distribución

La distribución de este software se realizará el paquetes NuGet.

La dirección del **repositorio Nuget** es: 

    https://pkgs.dev.azure.com/asierGarciss/_packaging/AsierLibs/nuget/v3/index.json

**usuario**: 

    -------

El **token**, solo de lectura: 

    2lei7o5huwyojdznmocbv2rv4b5yls437373vzrtobkxjy4z3jja