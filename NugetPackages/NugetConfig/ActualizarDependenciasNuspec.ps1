$rutaNuspec = '.\xmlPrueba.nuspec'
$rutaCSProj = '..\..\CsharpLibs\01_CsharpLibs\'

$versionEntorno = ".NETStandard2.1"
$versionEntornoCarpeta = "netStandard2.1"
$dependencias = ""
$versionNuget = 3.0

try {
  
  # En los .csproj se almacena una etiqueta padre <ItemGroup> y dentro
  # Se ubica <PackageReference Include="Newtonsoft.Json" Version="12.0.3" />
  $ubicacionArchivos = Get-ChildItem $rutaCSProj -Recurse -Filter *.csproj -Name
  foreach ($leer in $ubicacionArchivos) {
    [xml]$xmlCsproj = Get-Content -Path ($rutaCSProj + $leer)
    $nodos = $xmlCsproj | Select-xml -XPath "//PackageReference"
    
    if ($null -ne $nodos) {
      foreach ($add in $nodos) {
        if (!$dependencias.Contains($add.Node.Attributes[0].Value)) {
          $dependencias += "<dependency id=""" + $add.Node.Attributes[0].Value + """ version=""" + $add.Node.Attributes[1].Value + """ exclude=""Build,Analyzers"" /> `n"
        }  
      }
    }
  }

  [xml]$xmlNuspec = Get-Content -Path $rutaNuspec
  $nodoVersion = $xmlNuspec | Select-xml -XPath "//package"
  if ($null -ne $nodoVersion) {
    $nodoVersion.Node.Name
  }
  else {
    Write-Host "No se encuentra el archivo " + $rutaNuspec
    exit
  }
  # En los nuspec las dependencias de han de agregar como 
  # <dependency id="System.Data.SQLite.Core" version="1.0.112" exclude="Build,Analyzers" />
  # version es en minuscula!!!

  $DocumentoNuspec = "<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd"">
<metadata>
<id>CsharpLibs</id>
<title>CSharp Libs</title>
<version>" + $versionNuget + "</version>
<authors>Asier Garcia</authors>
<owners>Asier Garcia</owners>
<requireLicenseAcceptance>false</requireLicenseAcceptance>
<description>Librerias creadas para futuros proyectos o usos</description>
<repository url=""https://github.com/g4rc1ss/CsharpLibs"" />
<dependencies>
<group targetFramework=""$versionEntorno"">
$dependencias
</group>
</dependencies>
</metadata>
<files>
<file src=""..\PackagesCompilaciones\netstandard*\*.dll"" target=""lib\$versionEntornoCarpeta\"" />
<file src=""..\PackagesCompilaciones\netstandard*\*.pdb"" target=""lib\$versionEntornoCarpeta\"" />
<file src=""..\PackagesCompilaciones\netstandard*\*.xml"" target=""lib\$versionEntornoCarpeta\"" />
</files>
</package>"

  $DocumentoNuspec > $rutaNuspec

  if ($DocumentoNuspec.Equals((Get-Content -Path $rutaNuspec))) {
    Write-Host "Se ha generado el documento correctamente"
  }
  else {
    Write-Host "No se ha generado el documento correctamente"
  }
}
catch {
  Write-Error $_.Exception.Message
  Write-Error "No se ha podido generar el documento correctamente, abortamos" -ErrorAction Stop
}
finally {
  # Se borran todas las variables usadas en el script
  Remove-Variable * -ErrorAction SilentlyContinue
}
