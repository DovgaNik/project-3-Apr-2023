Imports System.IO

Public Class Form1
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim opDg As New OpenFileDialog

        opDg.Filter = "Images|*.jpg|All files|*.*"
        opDg.FilterIndex = 1
        opDg.DefaultExt = ".jpg"
        opDg.AddExtension = True
        opDg.Multiselect = True

        If opDg.ShowDialog() = DialogResult.OK Then

            Dim selector As New Form2
            For Each filename In opDg.FileNames

                If File.Exists(filename) Then
                    selector.ListBox1.Items.Add(filename)
                End If

            Next
            selector.Show()
        End If
    End Sub
End Class
