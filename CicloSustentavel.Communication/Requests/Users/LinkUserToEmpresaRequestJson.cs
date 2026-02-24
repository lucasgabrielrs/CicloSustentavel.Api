namespace CicloSustentavel.Communication.Requests.Users;

public class LinkUserToEmpresaRequestJson
{
    public Guid UserId { get; set; }
    public Guid EmpresaId { get; set; }
}
