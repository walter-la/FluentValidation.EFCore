using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.EFCore
{
    public class DbContextPropertyDescriptor<TDbContext> : IDbContextPropertyDescriptor
         where TDbContext : DbContext
    {
        private readonly IProperty _property;

        public DbContextPropertyDescriptor(IProperty property)
        {
            _property = property;
        }

        public int? GetMaxLength()
        {
            return _property.GetMaxLength();
        }

        public bool IsRequired()
        {
            return !_property.IsNullable;
        }

    }
}
