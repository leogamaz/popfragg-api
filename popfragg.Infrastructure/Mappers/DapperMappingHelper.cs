using Dapper;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace popfragg.Infrastructure.Mappers
{
    public static class DapperMappingHelper
    {
        public static void RegisterColumnMappings(params Assembly[] assemblies)
        {
            var typesWithColumnAttrs = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetProperties()
                    .Any(p => p.GetCustomAttribute<ColumnAttribute>() != null));

            foreach (var type in typesWithColumnAttrs)
            {
                SqlMapper.SetTypeMap(
                    type,
                    new CustomPropertyTypeMap(
                        type,
                        (t, columnName) =>
                            t.GetProperties()
                                .FirstOrDefault(prop =>
                                    prop.GetCustomAttributes<ColumnAttribute>(false)
                                        .Any(attr => attr.Name == columnName))
                            ?? throw new InvalidOperationException($"Property for column '{columnName}' not found in type '{t.Name}'")
                    )
                );
            }
        }
    }
}