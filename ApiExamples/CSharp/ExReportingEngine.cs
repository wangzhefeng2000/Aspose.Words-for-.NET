﻿// Copyright (c) 2001-2016 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.IO;
using ApiExamples.TestData;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Reporting;
using NUnit.Framework;
using DataSet = ApiExamples.TestData.DataSet;

namespace ApiExamples
{
    [TestFixture]
    public class ExReportingEngine : ApiExampleBase
    {
        private readonly string _image = MyDir + @"\Images\Test_636_852.gif";
        private readonly string _doc = MyDir + "ReportingEngine.TestDataTable.docx";

        [Test]
        public void SimpleCase()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Name]>> says: <<[s.Message]>>");

            SimpleDataSource sender = new SimpleDataSource("LINQ Reporting Engine", "Hello World");
            BuildReport(doc, sender, "s", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("LINQ Reporting Engine says: Hello World\f", doc.GetText());
        }

        [Test]
        public void StringFormat()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Name]:lower>> says: <<[s.Message]:upper>>, <<[s.Message]:caps>>, <<[s.Message]:firstCap>>");

            SimpleDataSource sender = new SimpleDataSource("LINQ Reporting Engine", "hello world");
            BuildReport(doc, sender, "s");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("linq reporting engine says: HELLO WORLD, Hello World, Hello world\f", doc.GetText());
        }

        [Test]
        public void NumberFormat()
        {
            Document doc = DocumentHelper.CreateSimpleDocument("<<[s.Value1]:alphabetic>> : <<[s.Value2]:roman:lower>>, <<[s.Value3]:ordinal>>, <<[s.Value1]:ordinalText:upper>>" + ", <<[s.Value2]:cardinal>>, <<[s.Value3]:hex>>, <<[s.Value3]:arabicDash>>, <<[s.Date]:\"MMMM\":lower>>");

            NumericDataSource sender = new NumericDataSource(1, 2.2, 200, DateTime.Parse("10.09.2016 10:00:00"));
            BuildReport(doc, sender, "s");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("A : ii, 200th, FIRST, Two, C8, - 200 -, september\f", doc.GetText());
        }

        [Test]
        public void DataTableTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestDataTable.docx");

            DataSet ds = DataTables.AddClientsTestData();
            BuildReport(doc, ds, "ds");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestDataTable Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestDataTable Out.docx", MyDir + @"\Golds\ReportingEngine.TestDataTable Gold.docx"));
        }

        [Test]
        public void NestedDataTableTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestNestedDataTable.docx");

            DataSet ds = DataTables.AddClientsTestData();
            BuildReport(doc, ds, "ds");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestNestedDataTable Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestNestedDataTable Out.docx", MyDir + @"\Golds\ReportingEngine.TestNestedDataTable Gold.docx"));
        }

        [Test]
        public void ChartTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestChart.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds.Managers, "managers");
            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestChart Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestChart Out.docx", MyDir + @"\Golds\ReportingEngine.TestChart Gold.docx"));
        }

        [Test]
        public void BubbleChartTest()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestBubbleChart.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds.Managers, "managers");
            doc.Save(MyDir + @"\Artifacts\ReportingEngine.TestBubbleChart Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.TestBubbleChart Out.docx", MyDir + @"\Golds\ReportingEngine.TestBubbleChart Gold.docx"));
        }

        [Test]
        public void IndexOf()
        {
            Document doc = new Document(MyDir + "ReportingEngine.TestIndexOf.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds, "ds");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("The names are: Name 1, Name 2, Name 3\f", doc.GetText());
        }

        [Test]
        public void IfElse()
        {
            Document doc = new Document(MyDir + "ReportingEngine.IfElse.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds.Managers, "m");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("You have chosen 3 item(s).\f", doc.GetText());
        }

        [Test]
        public void IfElseWithoutData()
        {
            Document doc = new Document(MyDir + "ReportingEngine.IfElse.docx");
            DataSet ds = new DataSet();

            BuildReport(doc, ds.Managers, "m");

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            Assert.AreEqual("You have chosen no items.\f", doc.GetText());
        }

        [Test]
        public void ExtensionMethods()
        {
            Document doc = new Document(MyDir + "ReportingEngine.ExtensionMethods.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds, "ds");
            doc.Save(MyDir + @"\Artifacts\ReportingEngine.ExtensionMethods Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.ExtensionMethods Out.docx", MyDir + @"\Golds\ReportingEngine.ExtensionMethods Gold.docx"));
        }

        [Test]
        public void Operators()
        {
            Document doc = new Document(MyDir + "ReportingEngine.Operators.docx");
            NumericDataSourceWithMethod testData = new NumericDataSourceWithMethod(1, 2.0, 3, null, true);

            ReportingEngine report = new ReportingEngine();
            report.KnownTypes.Add(typeof(NumericDataSourceWithMethod));
            report.BuildReport(doc, testData, "ds");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.Operators Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.Operators Out.docx", MyDir + @"\Golds\ReportingEngine.Operators Gold.docx"));
        }

        [Test]
        public void ContextualObjectMemberAccess()
        {
            Document doc = new Document(MyDir + "ReportingEngine.ContextualObjectMemberAccess.docx");
            DataSet ds = DataTables.AddClientsTestData();

            BuildReport(doc, ds, "ds");
            doc.Save(MyDir + @"\Artifacts\ReportingEngine.ContextualObjectMemberAccess Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.ContextualObjectMemberAccess Out.docx", MyDir + @"\Golds\ReportingEngine.ContextualObjectMemberAccess Gold.docx"));
        }

        [Test]
        public void InsertDocumentDinamically()
        {
            //By stream
            Document template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentByStream]>>");

            DocumentDataSource docStream = new DocumentDataSource(new FileStream(this._doc, FileMode.Open, FileAccess.Read));

            BuildReport(template, docStream, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by stream");

            //By doc
            template = DocumentHelper.CreateSimpleDocument("<<doc [src.Document]>>");

            DocumentDataSource docByDoc = new DocumentDataSource(new Document(MyDir + "ReportingEngine.TestDataTable.docx"));

            BuildReport(template, docByDoc, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by document");

            //By uri
            template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentByUri]>>");

            DocumentDataSource docByUri = new DocumentDataSource("http://www.sample-videos.com/doc/Sample-doc-file-100kb.doc");

            BuildReport(template, docByUri, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(uri) Gold.docx"), "Fail inserting document by uri");

            //By byte
            template = DocumentHelper.CreateSimpleDocument("<<doc [src.DocumentByByte]>>");

            DocumentDataSource docByByte = new DocumentDataSource(File.ReadAllBytes(MyDir + "ReportingEngine.TestDataTable.docx"));

            BuildReport(template, docByByte, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertDocumentDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertDocumentDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void InsertImageDinamically()
        {
            //By stream
            Document template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Stream]>>", ShapeType.TextBox);
            ImageDataSource docByStream = new ImageDataSource(new FileStream(this._image, FileMode.Open, FileAccess.Read));

            BuildReport(template, docByStream, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");

            //By image
            template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Image]>>", ShapeType.TextBox);
            ImageDataSource docByImg = new ImageDataSource(Image.FromFile(this._image, true));

            BuildReport(template, docByImg, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");

            //By Uri
            template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Uri]>>", ShapeType.TextBox);
            ImageDataSource docByUri = new ImageDataSource("http://joomla-aspose.dynabic.com/templates/aspose/App_Themes/V3/images/customers/americanexpress.png");

            BuildReport(template, docByUri, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(uri) Gold.docx"), "Fail inserting document by bytes");

            //By bytes
            template = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Bytes]>>", ShapeType.TextBox);
            ImageDataSource docByBytes = new ImageDataSource(File.ReadAllBytes(this._image));

            BuildReport(template, docByBytes, "src", ReportBuildOptions.None);
            template.Save(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.InsertImageDinamically Out.docx", MyDir + @"\Golds\ReportingEngine.InsertImageDinamically(stream,doc,bytes) Gold.docx"), "Fail inserting document by bytes");
        }

        [Test]
        public void WithoutKnownType()
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.Writeln("<<[new DateTime()]:”dd.MM.yyyy”>>");

            ReportingEngine engine = new ReportingEngine();
            Assert.That(() => engine.BuildReport(doc, ""), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void WorkWithKnownTypes()
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”dd.MM.yyyy”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”dd”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”MM”>>");
            builder.Writeln("<<[new DateTime(2016, 1, 20)]:”yyyy”>>");

            builder.Writeln("<<[new DateTime(2016, 1, 20).Month]>>");

            ReportingEngine engine = new ReportingEngine();
            engine.KnownTypes.Add(typeof(DateTime));
            engine.BuildReport(doc, "");

            doc.Save(MyDir + @"\Artifacts\ReportingEngine.KnownTypes Out.docx");

            Assert.IsTrue(DocumentHelper.CompareDocs(MyDir + @"\Artifacts\ReportingEngine.KnownTypes Out.docx", MyDir + @"\Golds\ReportingEngine.KnownTypes Gold.docx"));
        }


        [Test]
        public void StretchImagefitHeight()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Stream] -fitHeight>>", ShapeType.TextBox);

            ImageDataSource imageStream = new ImageDataSource(new FileStream(this._image, FileMode.Open, FileAccess.Read));

            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);

            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox 
                Assert.IsTrue(shape.ImageData.HasImage);

                //Assert that width is keeped and height is changed
                Assert.AreNotEqual(346.35, shape.Height);
                Assert.AreEqual(431.5, shape.Width);
            }

            dstStream.Dispose();
        }

        [Test]
        public void StretchImagefitWidth()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Stream] -fitWidth>>", ShapeType.TextBox);

            ImageDataSource imageStream = new ImageDataSource(new FileStream(this._image, FileMode.Open, FileAccess.Read));

            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);

            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox and 
                Assert.IsTrue(shape.ImageData.HasImage);

                //Assert that height is keeped and width is changed
                Assert.AreNotEqual(431.5, shape.Width);
                Assert.AreEqual(346.35, shape.Height);
            }

            dstStream.Dispose();
        }

        [Test]
        public void StretchImagefitSize()
        {
            Document doc = DocumentHelper.CreateTemplateDocumentWithDrawObjects("<<image [src.Stream] -fitSize>>", ShapeType.TextBox);

            ImageDataSource imageStream = new ImageDataSource(new FileStream(this._image, FileMode.Open, FileAccess.Read));

            BuildReport(doc, imageStream, "src", ReportBuildOptions.None);

            MemoryStream dstStream = new MemoryStream();
            doc.Save(dstStream, SaveFormat.Docx);

            doc = new Document(dstStream);

            NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);

            foreach (Shape shape in shapes)
            {
                // Assert that the image is really insert in textbox 
                Assert.IsTrue(shape.ImageData.HasImage);

                //Assert that height is changed and width is changed
                Assert.AreNotEqual(346.35, shape.Height);
                Assert.AreNotEqual(431.5, shape.Width);
            }

            dstStream.Dispose();
        }

        [Test]
        public void WithoutMissingMembers()
        {
            DocumentBuilder builder = new DocumentBuilder();

            //Add templete to the document for reporting engine
            DocumentHelper.InsertBuilderText(builder, new[] { "<<[missingObject.First().id]>>", "<<foreach [in missingObject]>><<[id]>><</foreach>>" });

            //Assert that build report failed without "ReportBuildOptions.AllowMissingMembers"
            Assert.That(() => BuildReport(builder.Document, new DataSet(), "", ReportBuildOptions.None), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void WithMissingMembers()
        {
            DocumentBuilder builder = new DocumentBuilder();

            //Add templete to the document for reporting engine
            DocumentHelper.InsertBuilderText(builder, new[] { "<<[missingObject.First().id]>>", "<<foreach [in missingObject]>><<[id]>><</foreach>>" });

            BuildReport(builder.Document, new DataSet(), "", ReportBuildOptions.AllowMissingMembers);

            //Assert that build report success with "ReportBuildOptions.AllowMissingMembers"
            Assert.AreEqual(ControlChar.ParagraphBreak + ControlChar.ParagraphBreak + ControlChar.SectionBreak, builder.Document.GetText());
        }

        private static void BuildReport(Document document, object dataSource, string dataSourceName, ReportBuildOptions reportBuildOptions)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.Options = reportBuildOptions;

            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object[] dataSource, string[] dataSourceName)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object dataSource, string dataSourceName)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource, dataSourceName);
        }

        private static void BuildReport(Document document, object dataSource)
        {
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(document, dataSource);
        }
    }
}