using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications;
public class BaseSpacification<T> : ISpacification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> criteria { get; set; } = null;
    public List<Expression<Func<T, object>>> includes { get; set; } = new();
    public BaseSpacification() { } // بدون فلتر - جيب كل الداتا
    public BaseSpacification(Expression<Func<T, bool>> CriteriaExpression)
    {
        criteria = CriteriaExpression; // مثال: P => P.Id == 10
    }

}
