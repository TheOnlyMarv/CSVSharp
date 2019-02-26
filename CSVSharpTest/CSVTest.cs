using System;
using System.Linq;
using CSVSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVSharpTest
{
    [TestClass]
    public class CSVTest
    {

        [TestMethod]
        public void TestNotNull()
        {
            IExporter<TestClass> exporter = ExporterFactory.CreateExporter<TestClass>();
            Assert.IsNotNull(exporter);
        }

        [TestMethod]
        public void TestHeader()
        {
            IExporter<TestClass> exporter = ExporterFactory.CreateExporter<TestClass>();
            string header = exporter.GetHeader();

            Assert.IsNotNull(header, "No header found");
            string[] headerEntries = header.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.IsNotNull(headerEntries, "No headerentries found");
            var orderedProperties = typeof(TestClass).GetProperties().Select(x => x.Name).OrderBy(x => x).ToArray();
            Assert.IsTrue(headerEntries.Length  == orderedProperties.Length, "Wrong number of properties");
            for (int i = 0; i < headerEntries.Length; i++)
            {
                Assert.IsTrue(headerEntries[i] == orderedProperties[i], $"Wrong order! Value: {headerEntries[i]} Expected: {orderedProperties[i]}");
            }
            Console.WriteLine(header);
        }

        [TestMethod]
        public void TestHeaderWithExportIgnore()
        {
            IExporter<TestClass2> exporter = ExporterFactory.CreateExporter<TestClass2>();
            string header = exporter.GetHeader();

            Assert.IsNotNull(header, "No header found");
            string[] headerEntries = header.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.IsNotNull(headerEntries, "No headerentries found");
            var orderedProperties = 
                typeof(TestClass2).GetProperties().Select(x => x.Name)
                .Where(x => !new string[] { "TString", "TGuid" }.Contains(x))
                .OrderBy(x => x).ToArray();
            Assert.IsTrue(headerEntries.Length == orderedProperties.Length, "Wrong number of properties");
            for (int i = 0; i < headerEntries.Length; i++)
            {
                Assert.IsTrue(headerEntries[i] == orderedProperties[i], $"Wrong order! Value: {headerEntries[i]} Expected: {orderedProperties[i]}");
            }
            Console.WriteLine(header);
        }


        [TestMethod]
        public void TestHeaderWithExportIgnoreAndOrder()
        {
            IExporter<TestClass3> exporter = ExporterFactory.CreateExporter<TestClass3>();
            string header = exporter.GetHeader();

            Assert.IsNotNull(header, "No header found");
            string[] headerEntries = header.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.IsNotNull(headerEntries, "No headerentries found");
            var orderedProperties =
                typeof(TestClass3).GetProperties().Select(x => x.Name)
                .Where(x => !new string[] { nameof(TestClass3.TString), nameof(TestClass3.TGuid), nameof(TestClass3.TNInt), nameof(TestClass3.TObject) }.Contains(x)).OrderBy(x => x).ToArray();
            orderedProperties = new string[] { nameof(TestClass3.TObject), nameof(TestClass3.TNInt) }.Concat(orderedProperties).ToArray();
            Assert.IsTrue(headerEntries.Length == orderedProperties.Length, "Wrong number of properties");
            for (int i = 0; i < headerEntries.Length; i++)
            {
                Assert.IsTrue(headerEntries[i] == orderedProperties[i], $"Wrong order! Value: {headerEntries[i]} Expected: {orderedProperties[i]}");
            }
            Console.WriteLine(header);
        }
    }

    class TestClass
    {
        public string TString { get; set; }
        public int TInt { get; set; }
        public double TDouble { get; set; }
        public decimal TDecimal { get; set; }
        public Guid TGuid { get; set; }
        public object TObject { get; set; }

        public int? TNInt { get; set; }
        public double? TNDouble { get; set; }
        public decimal TNDecimal{ get; set; }
        public Guid? TNGuid { get; set; }
    }

    class TestClass2 : TestClass
    {
        [ExportIgnore]
        public new string TString{ get; set; }

        [ExportIgnore]
        public new Guid TGuid { get; set; }
    }

    class TestClass3 : TestClass2
    {
        [ExportOrder(1)]
        public new int? TNInt { get; set; }
        [ExportOrder(0)]
        public new object TObject { get; set; }
    }
}
