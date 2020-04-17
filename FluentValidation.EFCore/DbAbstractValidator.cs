/*
 Author: Walter-la
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FluentValidation.EFCore
{
    /// <summary>
    /// Provide mapping validation rules from DbContext which base on AbstractValidator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DbAbstractValidator<T> : AbstractValidator<T>
    {
        private readonly IDbContextModelDescriptor _dbContextModelDescriptor;

        protected DbAbstractValidator(IDbContextModelDescriptor dbContextModelDescriptor) : base()
        {
            _dbContextModelDescriptor = dbContextModelDescriptor;
        }

        public IDbRuleBuilder<T> CreateRuleMap<TEntity>(Expression<Func<T, string>> dest, Expression<Func<TEntity, string>> opt)
        {
            var propertyDescriptor = _dbContextModelDescriptor.GetPropertyDescriptor(opt);

            var ruleBuilderInitial = RuleFor(dest);
            return new DbRuleBuilder<T>(propertyDescriptor, ruleBuilderInitial.Must(x => true));
        }

    }
}
