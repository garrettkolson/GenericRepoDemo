using System.Linq.Expressions;
using GenericRepoDemo.Domain.Models;

namespace GenericRepoDemo.Domain.Interfaces;

public interface IGenericRepository
{
    Task<T?> GetByIdAsync<T>(int id) where T : DomainModelBase;
    
    Task<List<T>> GetAllAsync<T>() where T : DomainModelBase;
    
    Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase;
    
    Task<T?> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase;
    
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : DomainModelBase;
    
    Task AddAsync<T>(T entity) where T : DomainModelBase;
    
    Task UpdateAsync<T>(T entity) where T : DomainModelBase;
    
    Task DeleteAsync<T>(T entity) where T : DomainModelBase;
}