using CicloSustentavel.Domain.Models;

namespace CicloSustentavel.Domain.Repositories;

public interface IUserRepository
{
    void Create(UserModel user);
    void Update(UserModel user);
    void Delete(int id);
    UserModel? GetById(int id);
    List<UserModel> GetAll();
    UserModel? GetByEmail(string email);
}