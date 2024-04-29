using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;

namespace Game.Infrastructure
{
    internal class TupleConverter: IMultiValueConverter
    {
        public static TupleConverter Instance { get; } = new TupleConverter();
        public object Convert(IList<object?> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Tuple.Create(value[0], value[1]);
        }
        public object ConvertBack(IList<object?> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
