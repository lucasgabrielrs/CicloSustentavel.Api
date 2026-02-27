namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class ConflictException : CicloSustentavelException
    {
        public string Message { get; set; }

        public ConflictException(string message)
        {
            Message = message;
        }
    }
}
