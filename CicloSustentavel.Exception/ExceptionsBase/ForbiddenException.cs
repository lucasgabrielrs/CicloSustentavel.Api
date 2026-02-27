namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class ForbiddenException : CicloSustentavelException
    {
        public string Message { get; set; }

        public ForbiddenException() : this("Você não tem permissão para acessar este recurso.")
        {
        }

        public ForbiddenException(string message)
        {
            Message = message;
        }
    }
}
