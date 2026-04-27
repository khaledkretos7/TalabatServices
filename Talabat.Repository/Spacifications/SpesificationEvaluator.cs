using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Repository.Spacifications;
public class SpesificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(
        IQueryable<TEntity> inputQuery,
        ISpacification<TEntity> spec)
    {
        var query = inputQuery; // context.Set<Products>() or _context.Products

        if (spec.criteria is not null)
            query = query.Where(spec.criteria); // يطبق الـ WHERE

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDesc is not null) 
        {
            query=query.OrderByDescending(spec.OrderByDesc);
        }
        
        if (spec.IsPaginatedEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        query = spec.includes.Aggregate(query,
            (currentQuery, includeExpression) =>
                currentQuery.Include(includeExpression)); // يطبق الـ JOINs

        return query;
    }

}