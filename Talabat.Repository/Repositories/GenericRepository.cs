using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Spacifications;
using Talabat.Repository.Data;
using Talabat.Repository.Spacifications;

namespace Talabat.Repository.Repositories;
public class GenericRepository<T>(StoreDbContext context) : IGenericRepository<T> where T : BaseEntity 
{
    private readonly StoreDbContext _context = context;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        if (typeof(T) == typeof(Product))
           return (IReadOnlyList<T>)await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductCategory).ToListAsync();

        return await _context.Set<T>().ToListAsync();
    }
    public async Task<T?> Getasync(int id)
    {
        if (typeof(T) == typeof(Product))
            return await _context.Products.Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductCategory).FirstOrDefaultAsync() as T;
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpacification<T> spec)
    {
        return await ApplySpecification(spec).AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetWithSpecAsync(ISpacification<T> spec)
    {
        return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
    }
    public async Task<int> GetCountAsync(ISpacification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpacification<T> spec)
    {
        return SpesificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
    }

   
}