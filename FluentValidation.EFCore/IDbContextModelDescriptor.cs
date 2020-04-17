using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FluentValidation.EFCore
{
    public interface IDbContextModelDescriptor
    {
        IDbContextPropertyDescriptor GetPropertyDescriptor<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression);
    }
}
