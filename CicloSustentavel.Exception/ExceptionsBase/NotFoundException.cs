namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class NotFoundException : CicloSustentavelException
    {
        public string Message { get; set; }

        public NotFoundException(string message)
        {
            Message = message;
        }
    }
}
