namespace CicloSustentavel.Exception.ExceptionsBase
{
    public class DuplicateEmailException : CicloSustentavelException
    {
        public string Message { get; set; }

        public DuplicateEmailException() : this("Já existe um usuário com este email.")
        {
        }

        public DuplicateEmailException(string message)
        {
            Message = message;
        }
    }
}
