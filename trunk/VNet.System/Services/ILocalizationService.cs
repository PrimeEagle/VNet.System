using System.Linq.Expressions;

namespace VNet.System.Services;

public interface ILocalizationService
{
    string GetString<TResource>(Expression<Func<TResource>> propertyExpression);
}