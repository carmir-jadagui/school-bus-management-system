using System.Linq.Expressions;

namespace SBMS.API.Validators
{
    public class BoyModelValidator : AbstractValidator<BoyModel>, IValidator<BoyModel>
    {
        public BoyModelValidator() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("The model cannot be NULL.");

            ValidateNotNullNotEmpty(x => x.Dni);
            ValidateNotNullNotEmpty(x => x.FirstName);
            ValidateNotNullNotEmpty(x => x.LastName);
            ValidateNotNullNotEmpty(x => x.Gender);
            ValidateNotNullNotEmpty(x => x.Age);

            RuleFor(x => x.Age)
                .InclusiveBetween(5, 18)
                .WithMessage("Age must be between 5 and 18 years.");

            RuleFor(x => x.Gender)
                .Must(g => g == "F" || g == "M")
                .WithMessage("Gender must be 'F' (Femenino) or 'M' (Masculino).");
        }

        // Valida que los valores no vengan nulos y/o vacíos
        private void ValidateNotNullNotEmpty<TProperty>(Expression<Func<BoyModel, TProperty>> property)
        {
            RuleFor(property)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("The field cannot be NULL.")
                .NotEmpty().WithMessage("The field cannot be empty.");
        }
    }
}