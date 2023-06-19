using System.Reflection;
using AutoMapper;

namespace Infrastructure.Mappings;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;
        var sourceType = typeof(TSource);
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var item in destinationProperties)
        {
            if (sourceType.GetProperty(item.Name,flags) == null)
            {
                expression.ForMember(item.Name, opt => opt.Ignore());
            }
        }

        return expression;
    }
}