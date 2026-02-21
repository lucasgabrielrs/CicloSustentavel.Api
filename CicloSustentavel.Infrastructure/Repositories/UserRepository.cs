using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Infrastructure.Data;
using System.Runtime.Intrinsics.X86;

namespace CicloSustentavel.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public UserModel? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    void IUserRepository.Create(UserModel userM)
    {
        var user = userM as UserModel;
        if (user == null)
            throw new ArgumentException("Expected UserModel", nameof(user));

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    void IUserRepository.Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    List<UserModel> IUserRepository.GetAll()
    {
        return _context.Users.ToList();
    }

    UserModel? IUserRepository.GetById(int id)
    {
        return _context.Users.Find(id);
    }

    void IUserRepository.Update(UserModel userM)
    {
        var user = userM as UserModel;
        if (user == null)
            throw new ArgumentException("Expected UserModel", nameof(user));

        _context.Users.Update(user);
        _context.SaveChanges();
    }
}