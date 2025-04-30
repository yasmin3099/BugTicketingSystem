using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public interface IGenericRepo <T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    public Task Delete(Guid id);
}
