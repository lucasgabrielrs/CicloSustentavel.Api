using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    void IUserRepository.Delete(Guid id)
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

    UserModel? IUserRepository.GetById(Guid id)
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

    public void LinkUserToEmpresa(Guid userId, Guid empresaId)
    {
        var user = _context.Users.Include(u => u.Empresas).FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        var empresa = _context.Empresas.Find(empresaId);
        if (empresa == null)
            throw new ArgumentException("Empresa não encontrada.");

        if (!user.Empresas.Any(e => e.Id == empresaId))
        {
            user.Empresas.Add(empresa);
            _context.SaveChanges();
        }
    }

    public void UnlinkUserFromEmpresa(Guid userId, Guid empresaId)
    {
        var user = _context.Users.Include(u => u.Empresas).FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        var empresa = user.Empresas.FirstOrDefault(e => e.Id == empresaId);
        if (empresa != null)
        {
            user.Empresas.Remove(empresa);
            _context.SaveChanges();
        }
    }

    public List<EmpresaModel> GetUserEmpresas(Guid userId)
    {
        var user = _context.Users.Include(u => u.Empresas).FirstOrDefault(u => u.Id == userId);
        return user?.Empresas.ToList() ?? new List<EmpresaModel>();
    }
}