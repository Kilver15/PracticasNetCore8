Lista de tareas basica con SQLServer.

Integrar SqlServer:
- Instalar EntityFrameworkCore con NuGet.
- Crear clase Context, que herede DbContext de EFC.
- Crear constructor escribiento "ctor".
- Se agrega el argumento "(DbContextOptions<TodoContext> options): base(options)".
- Crear DbSet del modelo.
- Configurar ConecctionStrings en appsettings.json:
  - Agregar dentro "Connection", debe quedar asi:
        "Connection": "Server=.\\SQLExpress;Database=TodoDB;Trusted_Connection=true;TrustServerCertificate=true"
  Otra forma de ver la cadena de conexion es:
  - Ir Ver > Explorador de servidores > Explorador de objetos SQLServer > clic derecho a la conexion > propiedades.
-  Instalar via NuGet, EFC.SqlServer.
-  En program.cs, en services agregar lo siguiente:
  var ConnectionString = builder.Configuration.GetConnectionString("Connection");
- Enseguida se registra el servicio para la conexion:
  builder.Services.AddDbContext<TodoContext>(
    options => options.UseSqlServer(connectionString));

Crear migraciones:
- Instalar paquete EFC.Tools.
- En Herramientas > Consola de admin de paquetes:
  - Add-Migration Initial (Se acostumbra a poner ese nombre a la primera migracion).
  - Si todo sale bien se creara la carpeta Migrations.
  - Update-database para actualizar la BD en el servidor.
 
Fuentes:
https://learn.microsoft.com/es-es/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio
https://www.youtube.com/watch?v=Gua0O0Q7I58&ab_channel=Inform%C3%A1ticaDP
