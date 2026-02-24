using FluentValidation;
using FluentValidation.Validators;

namespace CicloSustentavel.Application.Validators
{
    public class PasswordUserValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "PasswordUserValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} is not a valid password.";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if(string.IsNullOrWhiteSpace(password)) {
                context.AddFailure("A senha é obrigatória.");
                return false;
            } 
            
            if(password.Length < 6) {
                context.AddFailure("A senha deve conter pelo menos 6 caracteres.");
                return false;
            }

            return true;
        }
    }
}
