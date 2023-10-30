using BibliotecaServer.Domain.Entities;
using FluentValidation;

namespace BibliotecaServer.Domain.Validators;

public class EmprestimoValidator : AbstractValidator<Emprestimo>
{
    public EmprestimoValidator()
    {
        RuleFor(x => x.LivroId)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.UsuarioId)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.DataEmprestimo)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.DataDevolucao)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido"); 
    }
    
}

