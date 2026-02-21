using FluentValidation;
using CicloSustentavel.Communication.Requests.Users;

namespace CicloSustentavel.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("O nome do usuário é obrigatório.")         
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ter um formato válido.")
            .MaximumLength(255).WithMessage("O email deve ter no máximo 255 caracteres.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        RuleFor(request => request.Role)
            .NotEmpty().WithMessage("O tipo do usuário é obrigatório.")
            .MaximumLength(50).WithMessage("O perfil deve ter no máximo 50 caracteres.");
    }
}