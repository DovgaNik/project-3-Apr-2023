Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim currentFile As String
    Public Function UnixToDateTime(ByVal strUnixTime As String) As DateTime

        Dim nTimestamp As Double = strUnixTime
        Dim nDateTime As System.DateTime = New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
        nDateTime = nDateTime.AddSeconds(nTimestamp)

        Return nDateTime

    End Function

    Function DateTimeToUnix(uTime As DateTime)
        Return (uTime - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    End Function

    Function clearDB()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("BookID", "BookID")
        DataGridView1.Columns.Add("Name", "Name")
        DataGridView1.Columns.Add("Author", "Author")
        DataGridView1.Columns.Add("Is Borrowed", "Is borrowed")
        DataGridView1.Columns.Add("Borrow date", "Borrow date")
        DataGridView1.Columns.Add("Return date", "Return date")
    End Function

    Private Sub AddANewBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddANewBookToolStripMenuItem.Click
        Dim bookname As String = InputBox("Plese, enter the name of the book you want to add: ", "New book").Trim()
        Dim authorname As String = InputBox("Plese, enter the name of the author of the book you want to add: ", "New book").Trim()

        If bookname <> Nothing And authorname <> Nothing Then
            DataGridView1.Rows.Add(DataGridView1.Rows.Count - 1, bookname, authorname, False, New DateTime(1970, 1, 1, 0, 0, 0), New DateTime(1970, 1, 1, 0, 0, 0))
        Else
            MessageBox.Show("You did not enter a correct value!!!")
        End If

    End Sub

    Private Sub RemoveABookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveABookToolStripMenuItem.Click

        Dim rowind As Integer = DataGridView1.CurrentCell.RowIndex
        DataGridView1.Rows.RemoveAt(rowind)
        For index = rowind To DataGridView1.RowCount - 1
            DataGridView1.Rows(index).Cells(0).Value = index
        Next

    End Sub

    Private Sub CreateANewDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateANewDatabaseToolStripMenuItem.Click
        clearDB()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim fileName As String
        Dim opDg As New OpenFileDialog

        opDg.Filter = "Book database file|*.bookdb|All files|*.*"
        opDg.FilterIndex = 1
        opDg.DefaultExt = ".bookdb"
        opDg.AddExtension = True

        If opDg.ShowDialog() = DialogResult.OK Then
            fileName = opDg.FileName
        Else
            Exit Sub
        End If

        If File.Exists(fileName) Then
            currentFile = fileName
            clearDB()
            Dim objReader As New StreamReader(fileName)
            Dim ID As Integer
            ID = 0
            Do While objReader.Peek() <> -1

                Dim objects As String()
                objects = objReader.ReadLine().Split("|".ToCharArray)
                If objects.Count = 5 Then
                    DataGridView1.Rows.Add(ID, objects(0), objects(1), Convert.ToBoolean(Convert.ToInt16(objects(2))), UnixToDateTime(objects(3)), UnixToDateTime(objects(4)))
                End If
                ID += 1
            Loop

        Else
            MessageBox.Show("File " + fileName + " does not exist, plese check again!!!")
        End If
    End Sub
    Private Sub ABookIsReturnedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ABookIsReturnedToolStripMenuItem.Click
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(3).Value = False
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(4).Value = New DateTime(1970, 1, 1, 0, 0, 0)
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(5).Value = New DateTime(1970, 1, 1, 0, 0, 0)
    End Sub

    Private Sub HelppageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelppageToolStripMenuItem.Click
        FormHelp.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.MultiSelect = False
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs)
        save(currentFile)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        saveAs()
    End Sub

    Function saveAs()
        Dim svDg As New SaveFileDialog

        svDg.Filter = "Book database file|*.bookdb|All files|*.*"
        svDg.FilterIndex = 1
        svDg.DefaultExt = ".bookdb"
        svDg.AddExtension = True

        If svDg.ShowDialog() = DialogResult.OK Then
            save(svDg.FileName)
        End If
    End Function

    Function save(filename)
        Try
            File.Delete(filename)
        Catch ex As Exception
            MessageBox.Show("Unable to delete the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        For index = 0 To DataGridView1.RowCount - 2
            File.AppendAllText(filename, DataGridView1.Rows(index).Cells(1).Value + "|" + DataGridView1.Rows(index).Cells(2).Value + "|" + Convert.ToInt16(DataGridView1.Rows(index).Cells(3).Value).ToString + "|" + DateTimeToUnix(DataGridView1.Rows(index).Cells(4).Value).ToString + "|" + DateTimeToUnix(DataGridView1.Rows(index).Cells(5).Value).ToString + vbNewLine)
        Next
    End Function

    Private Sub ABookIsBorrowedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ABookIsBorrowedToolStripMenuItem.Click
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(3).Value = True
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(4).Value = New DateTime().Now
        DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(5).Value = New DateTime().Now.AddDays(5)
    End Sub

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        Dim selIndex As Integer = DataGridView1.CurrentCell.RowIndex
        Dim info As New Form2
        info.selIndex = selIndex
        info.TextBox1.Text = DataGridView1.Rows(selIndex).Cells(1).Value
        info.TextBox2.Text = DataGridView1.Rows(selIndex).Cells(2).Value
        info.CheckBox1.Checked = DataGridView1.Rows(selIndex).Cells(3).Value
        If info.CheckBox1.Checked Then
            info.DateTimePicker1.Value = DataGridView1.Rows(selIndex).Cells(4).Value
            info.DateTimePicker2.Value = DataGridView1.Rows(selIndex).Cells(5).Value
        End If
        info.Show()
    End Sub

    Function Savebookinfo(ind As Integer, name As String, author As String, checked As Boolean, borr As DateTime, ret As DateTime)
        DataGridView1.Rows(ind).Cells(1).Value = name
        DataGridView1.Rows(ind).Cells(2).Value = author
        DataGridView1.Rows(ind).Cells(3).Value = checked
        DataGridView1.Rows(ind).Cells(4).Value = borr
        DataGridView1.Rows(ind).Cells(5).Value = ret
    End Function

End Class
