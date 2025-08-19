using System.Linq.Expressions;

namespace SBMS.API.Validators
{
    public class DriverModelValidator : AbstractValidator<DriverModel>, IValidator<DriverModel>
    {
        public DriverModelValidator() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("The model cannot be NULL.");

            ValidateNotNullNotEmpty(x => x.Dni);
            ValidateNotNullNotEmpty(x => x.FirstName);
            ValidateNotNullNotEmpty(x => x.LastName);

            RuleFor(x => x.Telephone)
                .Matches(@"^\+?[0-9\s\-]{7,15}$")
                .When(x => !string.IsNullOrEmpty(x.Telephone)) // aplica solo si tiene valor
                .WithMessage("Telephone must be a valid phone number");
        }

        // Valida que los valores no vengan nulos y/o vacíos
        private void ValidateNotNullNotEmpty<TProperty>(Expression<Func<DriverModel, TProperty>> property)
        {
            RuleFor(property)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The field cannot be NULL.")
                .NotEmpty().WithMessage("The field cannot be empty.");
        }
    }
}