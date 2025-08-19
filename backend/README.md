# school-bus-management-system
Sistema de Gestión de Micros Escolares, Alumnos y Choferes 


# Tecnologías
- Backend: .NET 8
- Arquitectura: Hexagonal
- Patrones utilizados: Repository Pattern, Herencia en entidades
- Contenedores: Docker / Docker Compose


# Documentación API: 
- Swagger


# Requisitos previos
- Herramienta para el desarrollo: Visual Studio Community 2022.
    Puede descargarlo de: https://visualstudio.microsoft.com/es/vs/community/

- Herramienta para usar el contenedor: Docker Desktop.
    Puede descargarlo de: https://docs.docker.com/desktop/

- Instalar la extensión de Docker en Visual Studio.

- Clonar el repositorio.

- Permitir la conexión de la BD MySql con el Docker. 
    1.- Ir a la herramienta instaladas para la administración de la BD de MySql (ver database\README.md).
    2.- Ejecutar el siguiente script:
    CREATE USER 'NOMBRE_DE_USUARIO'@'%' IDENTIFIED BY 'CONTRASEÑA_DEL_USUARIO';
	GRANT ALL PRIVILEGES ON SBMS.* TO 'NOMBRE_DE_USUARIO'@'%' WITH GRANT OPTION;
	FLUSH PRIVILEGES;
        Nota: reemplazar las variables de NOMBRE_DE_USUARIO y CONTRASEÑA_DEL_USUARIO, por los valores correspondientes.

# Variables
- BD
    - SERVER = host / ip del servidor de sql, seguido de una "," con el número del puerto. 
    Ejemplo: 192.168.0.1,3306
    Nota: normalmente para MySql, el puerto por defecto es el 3306.
    - DATABASE = nombre de la base de datos, que para este caso, si no cambió su valor en el script de creación de la misma, será: SBMS.
    - USER = el nombre del usuario al cual le dió acceso en el paso previo.
    - PASSWORD = contraseña usada para el usuario del punto anterior.

- Variable de entorno
    - ASPNETCORE_ENVIRONMENT = indica el ambiente en el cual se está trabajando. Por defecto viene "Development", esto es para que se pueda ejecutar el Swagger.


# Levantar con Docker
1.- Tener corriendo el Docker Desktop.
2.- Desde el Visual Studio Community 2022 ir a: Herramientas -> Línea de Comandos -> PowerShell para Desarrolladores.
3.- Validar estar en el directorio: backend

- Manualmente:
    4.- Reeplazar los valores de las variables de la BD, que están en el archivo app.env  
    5.- Ejecutar el siguiente comando:
        docker build -f app/SBMS.API/Dockerfile -t sbmsimage .
    6.- Puede revisar por comando (docker images) o por el Docker Desktop si se creó correctamente la imágen.
    7.- docker run -it --rm -p 3000:8080 --env-file app.env --name sbmscontainer sbmsimage

- Con Docker Compose:
    4.- Reeplazar los valores de las variables de la BD, que están en el archivo .env 
    5.- Ejecutar el siguiente comando:
        docker-compose up -d


# Acceder al Swagger
Una vez que el contenedor de Docker está activo: http://localhost:3000/swagger/index.html


# Arquitectura
La solución sigue Hexagonal Architecture, separando:
- app 
    - SBMS.API: Controladores y endpoints, program.cs y validators.
- src
    - core
        - SBMS.Application: Servicios / lógica de negocio
        - SBMS.Base: Clases personalizadas de exceptiones y modelos reutilizables para las respuestas.
        - SBMS.Domain: Interfaces de los servicios y respositorios, así como también los modelos que servirán de nexo entre la capa de infraestructura/persistencia y la de core/apis.
    - Infrastructure: Entities, DbContex, Repositorio; el acceso a datos / comunicación con la BD.


# Patrones de diseño y POO
- Herencia (POO).
    - Se creó AuditModel, el cual contiene propiedades de auditoria, tales como son: id, CreatedAt y UpdatedAt; que usaran las entidades de Boy, Bus y Driver.
    - Se creó PersonBaseModel, el cual hereda de AuditModel; asímismo, contiene las propiedades básicas que usarán las entidades de Boy y Driver. Las mismas son: Dni, FirstName y LastName.
    
    Esto deja las clases de la siguiente manera:
    - Bus : AuditModel
    - PersonBaseModel : AuditModel
    - Boy : PersonBaseModel
    - Driver : PersonBaseModel

- Repository Pattern (como patrón de diseño).
Se aplicó para crear una única interfaz de repositorio, para los CRUD de Boy y Drive, ya que comparten exactamente los mismos endpoint, basado en los datos de una persona.
    - GetPersonAll()
    - GetPersonByDNI(int dni)
    - CreatePerson(T personModel)
    - UpdatePerson(T personModel)
    - DeletePerson