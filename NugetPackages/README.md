# NugetPackages

Se han configurado las librerías para que se compilen y se generen paquetes nuget automáticamente para poder distribuir los ensamblados `.dll` como los paquetes

---
![1_OpcionesCompilación](ReadmeNugetImages/1_OpcionesCompilacion.JPG)

Esta es la configuración de Compilación en Release, cuando se hace una Release es porque la librería funciona y se puede pasar a "Producción" y optimizar el código, asique se guardaran las `.dll` en NugetPackages y también se creara un fichero `.xml` como documentación del ensamblado, este se generara en la ruta del proyecto normal y se agregara al paquete NuGet.

---
![2_ConfiguraciónMetadatosNuGet](ReadmeNugetImages/2_ConfiguracionMetadatosNuGet.JPG)

Esta es la configuración del paquete NuGet

- **Package id:** Nombre del paquete, se usara para buscarlo en el repositorio por ejemplo

- **Package Version:** Version del paquete, por defecto 1.0, pero cuando se saquen mejoras grandes se deberá de subir para actualizar automáticamente los paquetes en los diferentes proyectos

- **Authors:** Nombre del creador o creadores de la librería 

- **Company:** Nombre de la empresa que lo crea, si no es empresa se pondrá el mismo nombre que el Author

- **Product:** Nombre del proyecto

- **Descripción:** Una breve descripción de lo que consta el proyecto
