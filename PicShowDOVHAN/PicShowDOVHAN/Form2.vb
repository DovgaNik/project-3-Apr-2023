Public Class Form2
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Form1.PictureBox1.Image = Image.FromFile(ListBox1.SelectedItem)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If ListBox1.SelectedIndex <> -1 Then
            If ListBox1.SelectedIndex = 0 Then
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            Else
                ListBox1.SelectedIndex = ListBox1.SelectedIndex - 1
            End If
        Else
            MessageBox.Show("You haven't selected anything", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ListBox1.SelectedIndex <> -1 Then
            If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
                ListBox1.SelectedIndex = 0
            Else
                ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
            End If
        Else
            MessageBox.Show("You haven't selected anything", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class