namespace CicloSustentavel.Communication.Responses.Users;

public class ResponseRegisteredUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public List<Guid> EmpresaIds { get; set; } = new List<Guid>();
}