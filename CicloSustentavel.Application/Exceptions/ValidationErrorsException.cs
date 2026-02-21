namespace CicloSustentavel.Application.Exceptions;

public class ValidationErrorsException : Exception
{
    public List<string> ErrorMessages { get; }

    public ValidationErrorsException(List<string> errorMessages) 
        : base(string.Join(" | ", errorMessages))
    {
        ErrorMessages = errorMessages;
    }
}