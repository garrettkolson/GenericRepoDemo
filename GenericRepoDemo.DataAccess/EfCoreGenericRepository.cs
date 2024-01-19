using System.Linq.Expressions;
using GenericRepoDemo.Domain.Interfaces;
using GenericRepoDemo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericRepoDemo.DataAccess;

public class EfCoreGenericRepository : IGenericRepository
{
    private readonly GenericRepoDemoDbContext _context;

    public EfCoreGenericRepository(GenericRepoDemoDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync<T>(int id) where T : DomainModelBase
    {
        return await _context.FindAsync<T>(id);
    }

    public async Task<List<T>> GetAllAsync<T>() where T : DomainModelBase
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase
    {
        return await getQueryable(predicate).ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase
    {
        return await getQueryable(predicate).FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase
    {
        return await getQueryable(predicate).AnyAsync();
    }

    public async Task AddAsync<T>(T entity) where T : DomainModelBase
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(T entity) where T : DomainModelBase
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(T entity) where T : DomainModelBase
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    private IQueryable<T> getQueryable<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase
    {
        return _context.Set<T>().Where(predicate);
    }
}