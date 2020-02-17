using FastFood.user_interface.components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FastFood
{
    public class EqualsConverter : IMultiValueConverter
    {
     
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
            {
                if (Int32.Parse(values[0].ToString()) == Int32.Parse(values[1].ToString()))
                {
                    return true;
                }
            }
            return false;
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
