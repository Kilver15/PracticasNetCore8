- Tareas en 2do plano.
BackgroundServices es para tareas recurrentes mientras que IHostedLifecycleService
para tareas no reccurentes ademas para poder manejar funcionalidad durante el ciclo de vida de la solucion.

En BGService implementar clase abstracta Y EN IHLS implementar su interfaz.

Registrar en program.cs con builder.Services.AddHostedService<>();

- Logs.
Inyectar la interfaz Ilogger.

- https://render2web.com/asp-net-core/logging-en-asp-net-core-guia-completa-con-ejemplos/
- https://www.youtube.com/watch?v=Z3ISqWALbtU&ab_channel=FelipeGavilanPrograma
