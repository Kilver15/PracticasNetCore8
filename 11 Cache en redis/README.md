Cache distribuido con Redis.
- Iniciar sesion en redis.io y crear una DB
- Buscar endpoint publico y la deafult user password.
  
- En el proyecto, instalar Microsoft.AspNetCore.OutputCaching.StackExchangeRedis
- en appsettings.json agregar a connection strings:
  "redis":"endpoint publico,pasword=default user password"
- En program agregar:
  builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});

- En los controladores usar OutputCache como se crea necesario.
  Ejemplo: [OutputCache(Duration = 30, Tags = ["personas"])]
Se puede checar lo de la Bd de Cache en Redisinsigt.
