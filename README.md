# CRUD Clientes con NET 8 - Arquitectura Limpia
## Pasos para levantar el proyecto
  - Ejecutar el script de base de datos que se encuentra en la siguiente ruta `db/Data.sql`
  - Cambiar la cadena de conexión de base de datos `ConnectionStrings` del archivo `appsettings.json`
  - Cambiar la ruta del repositorio de archivos `PathFiles` del archivo `appsettings.json`
  - Ejecutar el proyecto API (https)

## Tecnologias .NET
* FluentValidation
* Entity Framework Core
* Swagger
## Base de datos
* Sql Server
## Arquitectura
- Unit of Work
- Repository
- ORM
- Clean Architecture
  * Application
  * Domain
  * Infraestructure
  * API