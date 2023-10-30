using BibliotecaServer.Domain.Entities;
using FluentValidation;

namespace BibliotecaServer.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia.")

            .NotNull()
            .WithMessage("A entidade não pode ser nula.");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio.")

            .NotNull()
            .WithMessage("O nome não pode ser nulo.")

            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres.")

            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email não pode ser vazio.")
            .NotNull()
            .WithMessage("O email não pode ser nulo.")
            .MinimumLength(3)
            .WithMessage("O email deve ter no mínimo 3 caracteres.")
            .MaximumLength(100)
            .WithMessage("O email deve ter no máximo 100 caracteres.")
            .EmailAddress()
            .WithMessage("O email deve ser válido.");

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("A data não pode ser vazia.")

            .NotNull()
            .WithMessage("A data não pode ser nula.")

            .MinimumLength(10)
            .WithMessage("O telefone deve ter no mínimo 10 caracteres.")

            .MaximumLength(11)
            .WithMessage("O telefone deve ter no máximo 11 caracteres.");
    }            
            
}