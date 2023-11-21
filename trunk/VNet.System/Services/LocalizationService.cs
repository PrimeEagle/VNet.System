using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.


namespace VNet.System.Services
{
    public class LocalizationService : ILocalizationService
    {
        public string GetString<TResource>(Expression<Func<TResource>> propertyExpression)
        {
            var propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo ?? throw new ArgumentException("Expression must be a property expression.", nameof(propertyExpression));
            var resourceManagerProperty = typeof(TResource).GetProperty("ResourceManager", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            var resourceManager = (ResourceManager) resourceManagerProperty?.GetValue(null, null);
            if (resourceManager is null) return string.Empty;

            var cultureInfoProperty = typeof(TResource).GetProperty("Culture", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (cultureInfoProperty is null) return string.Empty;

            var cultureInfo = (CultureInfo) cultureInfoProperty.GetValue(null, null);

            return resourceManager.GetString(propertyInfo.Name, cultureInfo);
        }
    }
}