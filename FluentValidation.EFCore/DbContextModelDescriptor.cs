using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FluentValidation.EFCore
{
    public class DbContextModelDescriptor<TDbContext> : IDbContextModelDescriptor
           where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public DbContextModelDescriptor(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbContextPropertyDescriptor GetPropertyDescriptor<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var property = GetProperty(expression);

            return new DbContextPropertyDescriptor<TDbContext>(property);
        }

        private IProperty GetProperty<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var memberInfo = GetMember(expression);

            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            var property = entityType.FindProperty(memberInfo);

            return property;
        }

        /// <summary>
        /// Gets a MemberInfo from a member expression.
        /// </summary>
        private static MemberInfo GetMember<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberExp = RemoveUnary(expression.Body) as MemberExpression;

            if (memberExp == null)
            {
                return null;
            }

            Expression currentExpr = memberExp.Expression;

            // Unwind the expression to get the root object that the expression acts upon.
            while (true)
            {
                currentExpr = RemoveUnary(currentExpr);

                if (currentExpr != null && currentExpr.NodeType == ExpressionType.MemberAccess)
                {
                    currentExpr = ((MemberExpression)currentExpr).Expression;
                }
                else
                {
                    break;
                }
            }

            if (currentExpr == null || currentExpr.NodeType != ExpressionType.Parameter)
            {
                return null; // We don't care if we're not acting upon the model instance.
            }

            return memberExp.Member;
        }


        private static Expression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression)toUnwrap).Operand;
            }

            return toUnwrap;
        }
    }
}
