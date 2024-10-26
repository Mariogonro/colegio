# CRUD para Colegio - Prueba Técnica

## Descripción del Proyecto
Este proyecto es un CRUD (Crear, Leer, Actualizar y Eliminar) para un colegio, que permite llevar el control de profesores, alumnos, grados, asignación de grados a los alumnos, género de las personas y secciones. Está diseñado para gestionar y organizar eficientemente la información del colegio.

## Requisitos Previos
- Visual Studio 2022
- .NET Core
- SQL Server Express
- Conocimientos de C#, HTML, CSS (Bootstrap)
- Familiaridad con el patrón de diseño MVC (Model-View-Controller)

## Instalación
1. Clona el repositorio o descarga los archivos del proyecto.
2. Abre el proyecto en Visual Studio 2022.
3. Configura la cadena de conexión en el archivo "appsettings.json" con las credenciales de tu base de datos: 
    - "ConnectionDB": "Server=tuservidor;Database=Colegio;UserID=tuUsuario;Password=tuContraseña;Encrypt=false;MultipleActiveResultSets=true"
4. Para la creación de la base de datos ejecutar los siguientes comandos
    - Add-Migration InicialMigracion
    - Update-Database
## Uso
* Inicio: La pantalla principal muestra un carrusel y una bienvenida.
* Menú de Navegación: Desde el menú se puede acceder a las diferentes secciones del CRUD como Grado, Profesor, y Secciones.
* Operaciones CRUD:
* Crear: Permite agregar nuevos registros a la base de datos.
* Leer: Muestra una lista de registros existentes.
* Actualizar: Permite editar registros existentes.
* Eliminar: Permite eliminar registros, previa confirmación mediante un mensaje modal.