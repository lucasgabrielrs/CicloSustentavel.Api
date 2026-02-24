namespace CicloSustentavel.Domain.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ICollection<EmpresaModel> Empresas { get; set; } = new List<EmpresaModel>();
    }
}
