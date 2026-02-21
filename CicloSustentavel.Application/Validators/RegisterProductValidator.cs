using FluentValidation;
using CicloSustentavel.Communication.Requests.Products;

namespace CicloSustentavel.Application.Validators;

public class RegisterProductValidator : AbstractValidator<RegisterProductRequestJson>
{
    public Func<DateTime> CurrentTime { get; set; } = () => DateTime.Now;

    public RegisterProductValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(request => request.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

        RuleFor(request => request.InventoryQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");

        RuleFor(request => request.ExpirationDate)
            .Must(date => date > CurrentTime()).WithMessage("A data de validade deve ser uma data futura.");

        RuleFor(request => request.Category)
            .InclusiveBetween(0, 6).WithMessage("Categoria inválida."); // Baseado no seu Enum de 7 posições
    }
}