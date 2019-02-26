using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class ExportOrderAttribute : Attribute
    {
        public int Order{ get; }
        public ExportOrderAttribute(int Order)
        {
            this.Order = Order;
        }
    }
}
