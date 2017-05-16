Imports System.Text

Public Class Form1
    'https://www.techonthenet.com/oracle/datatypes.php
    Dim dataTypes() = {"char(size)", "nchar(size)", "nvarchar2(size)", "varchar2(size)", "long", "raw", "long raw", "number(p,s)", "numeric(p,s)", "float", "dec(p,s)", "decimal(p,s)", "integer", "int", "smallint", "real", "double precision", "date", "timestamp (fractional seconds precision)", "timestamp (fractional seconds precision) with time zone", "timestamp (fractional seconds precision) with local time zone", "interval year(year precision) to month", "interval day(day precision) to second (fractional seconds precision)", "bfile", "blob", "clob", "nclob", "rowid", "urowid(size)"}
    Const SynH As String = "CREATE TABLE "
    Const SynF As String = ");"
    Const pk As String = "CONSTRAINT [TableName]_pk PRIMARY KEY ([PkIdColumn])"
    Const fk As String = "CONSTRAINT fk_[ForeignTableName] FOREIGN KEY ([FkIdColumn]) REFERENCES [ForeignTableName]([FkIdColumn])"
    ''' <summary>
    '''         [TableName] , [PkIdColumn]
    '''         [ForeignTableName] , [FkIdColumn]
    ''' </summary>

    Function Syntax() As StringBuilder
        Dim x As New StringBuilder
        Dim dt As String
        x.Append(SynH)
        x.Append(TextBox1.Text & " (" & vbCrLf)
        For i = 0 To dg.RowCount - 1
            With dg.Rows(i)

                dt = .Cells(1).Value
                If IsNothing(dt) Then Continue For
                If dt.Contains("size") Then dt = dt.Replace("size", .Cells(2).Value)
                If dt.Contains("p,s") Then dt.Replace("p,s", IIf(.Cells(2).Value.ToString.Contains(","), .Cells(2).Value, .Cells(2).Value & ",1"))
                x.Append(.Cells(0).Value & " " & dt & " " & IIf(.Cells(3).Value = True, "NULL", "NOT NULL"))
                If i < dg.RowCount - 2 Then x.Append("," & vbCrLf)
            End With
        Next
        If Not TextBox2.Text.Trim.Length = 0 Then
            x.Append("," & vbCrLf)
            x.Append(pk.Replace("[TableName]", TextBox1.Text.Trim).Replace("[PkIdColumn]", TextBox2.Text.Trim))
        End If
        If Not TextBox3.Text.Trim.Length = 0 Then
            x.Append("," & vbCrLf)
            x.Append(fk.Replace("[ForeignTableName]", TextBox4.Text.Trim).Replace("[FkIdColumn]", TextBox3.Text.Trim))
        End If


        x.Append(vbCrLf)
        x.Append(SynF)
        x.Append(vbCrLf)
        Return x
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cbState As DataGridViewComboBoxColumn
        cbState = dg.Columns(1)
        cbState.Items.AddRange(dataTypes)
    End Sub
    Private Sub DataGridView1_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dg.CellFormatting
        If e.ColumnIndex = 1 Then
            If IsNothing(dg.CurrentRow) Then Exit Sub
            If IsNothing(dg.CurrentRow.Cells(1).Value) Then Exit Sub
            If Not dg.CurrentRow.Cells(1).Value.ToString.Contains("(") Then
                dg.CurrentRow.Cells(2).ReadOnly = True
                dg.CurrentRow.Cells(2).Value = ""
            Else
                dg.CurrentRow.Cells(2).ReadOnly = False
                dg.CurrentRow.Cells(2).Value = IIf(dg.CurrentRow.Cells(1).Value.ToString.Contains(","), "2,1", "10")
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Length = 0 Then TextBox1.Focus() : Exit Sub
        RichTextBox1.Text = Syntax().ToString
    End Sub
#Region "Right Click"
    Private Sub SelectColumnForPrimaryKeyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnForPrimaryKeyToolStripMenuItem.Click
        TextBox2.Text = dg.CurrentRow.Cells(0).Value
        Button2.Visible = True
    End Sub

    Private Sub SelectColumnForForeignKeyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnForForeignKeyToolStripMenuItem.Click
        TextBox3.Text = dg.CurrentRow.Cells(0).Value
        TextBox4.Text = dg.CurrentRow.Cells(0).Value.ToString.Split("_")(0)
        Button3.Visible = True
        Button4.Visible = True
    End Sub

    Private Sub RemoveColumnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveColumnToolStripMenuItem.Click
        Try
            dg.Rows.Remove(dg.CurrentRow)
        Catch ex As Exception

        End Try

    End Sub
#End Region
#Region "Clr buttons"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox2.Clear()
        Button2.Visible = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox4.Clear()
        Button3.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox3.Clear()
        Button4.Visible = False
        TextBox4.Clear()
        Button3.Visible = False
    End Sub
#End Region
#Region "Links"
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://github.com/Adykn")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://www.linkedin.com/in/adnan-khan-63595a81/")
    End Sub
#End Region

End Class
