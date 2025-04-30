using BUGSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly MyContext _context;

    public GenericRepo(MyContext context)
    {
        _context = context;
    }
    public async Task Add(T entity)
    {
       await _context.Set<T>()
         .AddAsync(entity);
    }
    public async Task Delete(Guid id)
    {
        var entity = await _context.Set<T>()
            .FindAsync(id) ?? null;
        _context.Set<T>()
            .Remove(entity);
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>()
             .AsNoTracking()
             .ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()
                .FindAsync(id);
    }
    public async Task Update(T entity)
    {

    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }


}
