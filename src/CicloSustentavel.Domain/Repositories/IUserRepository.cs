using CicloSustentavel.Domain.Models;

namespace CicloSustentavel.Domain.Repositories;

public interface IUserRepository
{
    Task Create(UserModel user);
    Task Update(UserModel user);
    Task Delete(Guid id);
    Task<UserModel?> GetById(Guid id);
    Task<List<UserModel>> GetAll();
    Task<UserModel?> GetByEmail(string email);
    Task LinkUserToEmpresa(Guid userId, Guid empresaId);
    Task UnlinkUserFromEmpresa(Guid userId, Guid empresaId);
    Task<List<EmpresaModel>> GetUserEmpresas(Guid userId);
}