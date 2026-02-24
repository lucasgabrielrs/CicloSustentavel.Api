namespace CicloSustentavel.Domain.Models
{
    public class EmpresaModel
    {
        public Guid Id { get; set; }
        public string NomeFantasia { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
