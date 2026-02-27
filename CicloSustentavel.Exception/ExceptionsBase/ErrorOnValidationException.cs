namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class ErrorOnValidationException : CicloSustentavelException
    {
        public List<string> Errors { get; set; }
        public ErrorOnValidationException(List<string> errorMessages)
        {
            Errors = errorMessages;
        }
    }
}
