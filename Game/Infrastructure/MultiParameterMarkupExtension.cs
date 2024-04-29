using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace Game.Infrastructure
{
    public class MultiParameterMarkupExtension: MarkupExtension
    {
        public object value1 {  get; set; }
        public object value2 { get; set; }

        public MultiParameterMarkupExtension(object value1, object value2) 
        {
            this.value1 = value1;
            this.value2 = value2;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new object[]{value1, value2};
        }
    }
}
