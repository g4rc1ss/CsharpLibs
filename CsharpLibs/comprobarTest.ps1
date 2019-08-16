$rutaCsharpLibs = Convert-Path ./01_CsharpLibs
$countCsharpLibs = (Get-ChildItem -Path $rutaCsharpLibs -Recurse -Filter *.cs).count

$rutaTest = Convert-Path ./02_Test
$countTest = (Get-ChildItem -Path $rutaTest -Recurse -Filter *.cs).count

if($countCsharpLibs -eq $countTest+1) {
    Write-Host "Hay " $countCsharpLibs "archivos en CsharpLibs y " $countTest "archivos en Test"

} else {
    Write-Host "Hay " $countCsharpLibs "archivos en CsharpLibs y " $countTest "archivos en Test"
    
    throw "Faltan Test. Tienes que crear un arhivo TestNombreClase.cs por cada archivo .cs de las librerias"
}