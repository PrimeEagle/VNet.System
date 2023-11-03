using System.ComponentModel;
using System.Globalization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.


namespace VNet.System.Services
{
    public class CultureManagerService : ICultureManagerService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}