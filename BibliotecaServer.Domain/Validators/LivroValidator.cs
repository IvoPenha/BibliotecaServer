using BibliotecaServer.Domain.Entities;
using FluentValidation;

namespace BibliotecaServer.Domain.Validators;

public class LivroValidator : AbstractValidator<Livro>
{

    public LivroValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia.")

            .NotNull()
            .WithMessage("A entidade não pode ser nula.");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("O titulo não pode ser vazio.")

            .NotNull()
            .WithMessage("O titulo não pode ser nulo.")

            .MinimumLength(3)
            .WithMessage("O titulo deve ter no mínimo 3 caracteres.")

            .MaximumLength(100)
            .WithMessage("O titulo deve ter no máximo 100 caracteres.");
        
        RuleFor(x => x.Autor)
            .NotEmpty()
            .WithMessage("O autor não pode ser vazio.")
            .NotNull()
            .WithMessage("O autor não pode ser nulo.")
            .MinimumLength(3)
            .WithMessage("O autor deve ter no mínimo 3 caracteres.")
            .MaximumLength(100)
            .WithMessage("O autor deve ter no máximo 100 caracteres.");
        
        RuleFor(x => x.Ano)
            .NotEmpty()
            .WithMessage("O ano não pode ser vazio.")

            .NotNull()
            .WithMessage("O ano não pode ser nulo.")
            
            .GreaterThan(1400)
            .WithMessage("O ano deve ser maior que 1400.")
            
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("O ano deve ser menor ou igual ao ano atual.");
    }

}
