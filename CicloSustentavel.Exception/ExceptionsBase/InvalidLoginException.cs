namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class InvalidLoginException : CicloSustentavelException
    {
        public string Message { get; set; }

        public InvalidLoginException() : this("Email ou senha inválidos.")
        {
        }

        public InvalidLoginException(string message)
        {
            Message = message;
        }
    }
}
