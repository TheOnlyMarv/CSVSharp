using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    public static class ExporterFactory
    {
        public static IFormatProvider FormatProvider { get; set; }

        public static IExporter<T> CreateExporter<T>(IFormatProvider formatProvider = null)
        {
            if (formatProvider == null && FormatProvider == null)
            {
                return new Exporter<T>();
            }
            else
            {
                return new Exporter<T>(formatProvider ?? formatProvider);
            }
        }
    }
}
