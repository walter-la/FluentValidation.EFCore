using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.EFCore
{
    public class DbRuleBuilder<T> : IDbRuleBuilder<T>
    {
        private readonly IDbContextPropertyDescriptor _dbContextPropertyDescriptor;
        private readonly IRuleBuilderOptions<T, string> _ruleBuilder;

        public DbRuleBuilder(
            IDbContextPropertyDescriptor dbContextPropertyDescriptor,
            IRuleBuilderOptions<T, string> ruleBuilder)
        {
            _dbContextPropertyDescriptor = dbContextPropertyDescriptor;
            _ruleBuilder = ruleBuilder;

        }

        public IDbRuleBuilder<T> MaximumLengthFromEntity()
        {
            int? maximumLength = _dbContextPropertyDescriptor.GetMaxLength();

            if (maximumLength.HasValue)
            {
                _ruleBuilder.MaximumLength(maximumLength.Value);
            }

            return this;
        }

        public IDbRuleBuilder<T> NotEmptyFromEntity()
        {
            var isRequired = _dbContextPropertyDescriptor.IsRequired();

            if (isRequired)
            {
                _ruleBuilder.NotEmpty();
            }

            return this;
        }

        public IDbRuleBuilder<T> NotNullFromEntity()
        {
            var isRequired = _dbContextPropertyDescriptor.IsRequired();

            if (isRequired)
            {
                _ruleBuilder.NotNull();
            }

            return this;
        }

        public IRuleBuilderOptions<T, string> ToRuleBuilderOptions()
        {
            return _ruleBuilder;
        }
    }
}
