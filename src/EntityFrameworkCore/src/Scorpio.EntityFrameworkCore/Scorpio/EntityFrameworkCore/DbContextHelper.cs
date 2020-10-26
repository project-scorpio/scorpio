using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Scorpio.Entities;

namespace Scorpio.EntityFrameworkCore
{
    internal static class DbContextHelper
    {
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
                from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    property.PropertyType.IsAssignableToGenericType(typeof(DbSet<>)) &&
                    property.PropertyType.GenericTypeArguments[0].IsAssignableTo<IEntity>()
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
}
