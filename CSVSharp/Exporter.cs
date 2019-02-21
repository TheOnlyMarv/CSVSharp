using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    class Exporter<T> : IExporter<T>
    {
        public IEnumerable<PropertyInfo> OrderedProperties { get; }

        public IFormatProvider FormatProvider { get; }

        public Exporter()
        {
            FormatProvider = CultureInfo.CurrentCulture;
            OrderedProperties = typeof(T).GetProperties().OrderBy(x => x.Name).ToList();
        }

        public Exporter(IFormatProvider formatProvider) : this()
        {
            FormatProvider = formatProvider;
        }

        public string GetData(IEnumerable<T> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var dataEntry in data)
            {
                foreach (var property in OrderedProperties)
                {
                    var value = property.GetValue(dataEntry);
                    sb.Append(GetPropertyString(value));
                }
                sb.Append(";");
            }
            return sb.ToString();
        }

        public string GetFullCsv(IEnumerable<T> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHeader());
            sb.Append("\n");
            sb.Append(GetData(data));
            return sb.ToString();
        }

        public string GetHeader()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var propertyName in OrderedProperties.Select(x => x.Name))
            {
                sb.Append(propertyName);
                sb.Append(";");
            }
            return sb.ToString();
        }


        #region Interal
        private string GetPropertyString(object value)
        {
            string stringValue = "";
            if (value != null)
            {
                if (value is bool b)
                {
                    stringValue = b ? "Ja" : "Nein";
                }
                else if (value is int i)
                {
                    stringValue = i.ToString(FormatProvider);
                }
                else if (value is double d)
                {
                    stringValue = d.ToString(FormatProvider);
                }
                else if (value is decimal de)
                {
                    stringValue = de.ToString(FormatProvider);
                }
                else if (value is Guid g)
                {
                    stringValue = g.ToString("D", FormatProvider);
                }
            }
            return stringValue;
        }
        #endregion
    }
}
