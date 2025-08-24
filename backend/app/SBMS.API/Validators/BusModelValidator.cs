using System.Linq.Expressions;

namespace SBMS.API.Validators
{
    public class BusModelValidator : AbstractValidator<BusModel>, IValidator<BusModel>
    {
        public BusModelValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("The model cannot be NULL.");

            ValidateNotNullNotEmpty(x => x.Plate);

            RuleFor(x => x.Plate)
                .Matches(@"^([A-Z]{3}-?\d{3}|[A-Z]{2}\s?\d{3}\s?[A-Z]{2})$")
                .WithMessage("Plate must be in format XXX-000, XXX000, XX000XX or XX 000 XX");
        }

        // Valida que los valores no vengan nulos y/o vacíos
        private void ValidateNotNullNotEmpty<TProperty>(Expression<Func<BusModel, TProperty>> property)
        {
            RuleFor(property)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The field cannot be NULL.")
                .NotEmpty().WithMessage("The field cannot be empty.");
        }
    }
}