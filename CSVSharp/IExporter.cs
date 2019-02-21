using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    public interface IExporter<T> 
    {
        string GetHeader();

        string GetData(IEnumerable<T> data);

        string GetFullCsv(IEnumerable<T> data);
    }
}
