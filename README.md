# Sistema de Gestión de Proyectos
Este proyecto fue desarrollado utilizando:
* .NET Framework 4.7
* Razor Pages
* Microsoft Identity
* Migraciones con base de datos SQL Server

## Utilización
* Cambiar conexion de base de datos en Web.config
* Habilitar migraciones en persistance, para ello abre una consola en el proyecto persistance y escribe el comando "enable-migration".
* Borrar las migraciones existentes y ejecutar el comando "add-migration Initalize"
* Para actualizar la base de datos utiliza el comando "update-database"

## Arquitectura 
<p align="center">
  <img src="https://4.bp.blogspot.com/-wSzGTxhqFbM/WxLMkV5QReI/AAAAAAAABvg/zLijvrM3ZiQsZKroCdzBZEu_yrQN-oaaQCLcBGAs/s1600/arquitectura_n_capas.png" alt="Arquitectura onnion"  width=50% height=50%>
</p>
