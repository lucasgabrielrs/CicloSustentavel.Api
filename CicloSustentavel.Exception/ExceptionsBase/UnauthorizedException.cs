namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class UnauthorizedException : CicloSustentavelException
    {
        public string Message { get; set; }

        public UnauthorizedException() : this("Acesso não autorizado.")
        {
        }

        public UnauthorizedException(string message)
        {
            Message = message;
        }
    }
}
