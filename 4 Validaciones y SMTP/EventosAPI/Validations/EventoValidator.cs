using FluentValidation;
using EventosAPI.Models;
namespace EventosAPI.Validations
{
    public class EventoValidator : AbstractValidator<EventoItem>
    {
        public EventoValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El título es requerido");
            RuleFor(x => x.Detalle).NotEmpty().WithMessage("El detalle es requerido");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es requerida");
            RuleFor(x => x.Hora).NotEmpty().WithMessage("La hora es requerida");
            RuleFor(x => x.Creador).NotEmpty().WithMessage("El creador es requerido");
            RuleFor(x => x.Creador).EmailAddress().WithMessage("El correo debe ser un email válido");
            RuleFor(x => x.Hora).Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("La hora debe tener el formato HH:mm");
        }
    }
}
