using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.EFCore
{
    public interface IDbContextPropertyDescriptor
    {
        int? GetMaxLength();
        bool IsRequired();
    }
}
