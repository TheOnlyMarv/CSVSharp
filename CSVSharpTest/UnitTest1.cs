using System;
using System.Linq;
using CSVSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVSharpTest
{
    [TestClass]
    public class UnitTest1
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
    }

    public class TestClass
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
}
