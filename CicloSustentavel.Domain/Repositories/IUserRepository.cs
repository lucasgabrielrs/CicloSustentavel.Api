using CicloSustentavel.Domain.Models;

namespace CicloSustentavel.Domain.Repositories;

public interface IUserRepository
{
    void Create(UserModel user);
    void Update(UserModel user);
    void Delete(Guid id);
    UserModel? GetById(Guid id);
    List<UserModel> GetAll();
    UserModel? GetByEmail(string email);
    void LinkUserToEmpresa(Guid userId, Guid empresaId);
    void UnlinkUserFromEmpresa(Guid userId, Guid empresaId);
    List<EmpresaModel> GetUserEmpresas(Guid userId);
}