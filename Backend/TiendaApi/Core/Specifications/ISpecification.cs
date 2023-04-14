using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criterio { get; }
        List<Expression<Func<T, Object>>> Includes { get; }
    }
}
