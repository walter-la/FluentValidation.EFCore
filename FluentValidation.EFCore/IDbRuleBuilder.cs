using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.EFCore
{
    public interface IDbRuleBuilder<T>
    {
        IDbRuleBuilder<T> MaximumLengthFromEntity();
        IDbRuleBuilder<T> NotEmptyFromEntity();
        IDbRuleBuilder<T> NotNullFromEntity();
        
        IRuleBuilderOptions<T, string> ToRuleBuilderOptions();
    }
}
