Validaciones de modelos utilizando FluentValidation.
- Instalar via NuGet:
  FluentValidation.DepencyInjectionExtensions
  SharpGrip.FluentValidation.AutoValidation.Mvc

- En program.cs agregar lo siguiente:
 builder.Services.AddValidatorsFromAssemblyContaining<Program>();
 builder.Services.AddFluentValidationAutoValidation();

- Crear carpeta de validaciones
- Crear clase para validacion del modelo
- Heredar Abstractvalidator<clase a validar>
- Agregar ctor y dentro de este, las validaciones necesarias
- Ejemplo:
RuleFor(x => x.Titulo).NotEmpty().WithMessage("El título es requerido");

- https://docs.fluentvalidation.net/en/latest/
- https://www.youtube.com/watch?v=IRtpX3dMyl4&ab_channel=FelipeGavilanPrograma
