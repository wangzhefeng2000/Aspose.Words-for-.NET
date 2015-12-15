﻿'///////////////////////////////////////////////////////////////////////
' Copyright 2001-2014 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.Words. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Aspose.Words
Imports Aspose.Words.Reporting

Namespace LINQ
    Public Class PieChart
        Public Shared Sub Run()
            ' The path to the documents directory.
            Dim dataDir As String = RunExamples.GetDataDir_LINQ()

            ' Load the template document.
            Dim doc As New Document(dataDir & Convert.ToString("PieChart.docx"))

            ' Create a Reporting Engine.
            Dim engine As New ReportingEngine()

            ' Execute the build report.
            engine.BuildReport(doc, Common.GetManagers(), "managers")

            dataDir = dataDir & Convert.ToString("PieChart Out.docx")

            ' Save the finished document to disk.
            doc.Save(dataDir)

            Console.WriteLine(Convert.ToString(vbLf & "Pie chart template document is populated with the data about managers." & vbLf & "File saved at ") & dataDir)

        End Sub
    End Class
End Namespace