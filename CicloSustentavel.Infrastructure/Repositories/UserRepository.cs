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

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _context.Users
            .Include(u => u.Empresas)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    async Task IUserRepository.Create(UserModel userM)
    {
        var user = userM as UserModel;
        if (user == null)
            throw new ArgumentException("Expected UserModel", nameof(user));

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    async Task IUserRepository.Delete(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    async Task<List<UserModel>> IUserRepository.GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    async Task<UserModel?> IUserRepository.GetById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    async Task IUserRepository.Update(UserModel userM)
    {
        var user = userM as UserModel;
        if (user == null)
            throw new ArgumentException("Expected UserModel", nameof(user));

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task LinkUserToEmpresa(Guid userId, Guid empresaId)
    {
        var user = await _context.Users.Include(u => u.Empresas).FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        var empresa = await _context.Empresas.FindAsync(empresaId);
        if (empresa == null)
            throw new ArgumentException("Empresa não encontrada.");

        if (!user.Empresas.Any(e => e.Id == empresaId))
        {
            user.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UnlinkUserFromEmpresa(Guid userId, Guid empresaId)
    {
        var user = await _context.Users.Include(u => u.Empresas).FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        var empresa = user.Empresas.FirstOrDefault(e => e.Id == empresaId);
        if (empresa != null)
        {
            user.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<EmpresaModel>> GetUserEmpresas(Guid userId)
    {
        var user = await _context.Users.Include(u => u.Empresas).FirstOrDefaultAsync(u => u.Id == userId);
        return user?.Empresas.ToList() ?? new List<EmpresaModel>();
    }
}