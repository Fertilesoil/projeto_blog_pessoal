using blogpessoal.Model;
using FluentValidation;

namespace blogpessoal.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {
        public TemaValidator()
        {
            RuleFor(d => d.Descricao)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(80);
        }
    }
}
