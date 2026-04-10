using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Core.Repository.Contract;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> Getasync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetWithSpecAsync(ISpacification<T> spec);
    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpacification<T> spec);
}

