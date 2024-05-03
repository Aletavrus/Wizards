using Avalonia.Data.Converters;
using Game.Model;

using System;
using System.Globalization;

namespace Game.Infrastructure;

public class CellToScreenConverter : IValueConverter
{
    public static CellToScreenConverter Instance { get; } = new CellToScreenConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return System.Convert.ToDouble(value) * GameMap.CellSize;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Avalonia.Data.BindingNotification.ExtractValue(value);
    }
}