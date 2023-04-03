Public Class Form2
    Public selIndex As Integer
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        MonthCalendar1.Enabled = CheckBox1.Checked
        DateTimePicker1.Enabled = CheckBox1.Checked
        DateTimePicker2.Enabled = CheckBox1.Checked
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MonthCalendar1.SelectionStart = DateTimePicker1.Value
        MonthCalendar1.SelectionEnd = DateTimePicker2.Value

        MonthCalendar1.Enabled = CheckBox1.Checked
        DateTimePicker1.Enabled = CheckBox1.Checked
        DateTimePicker2.Enabled = CheckBox1.Checked
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        MonthCalendar1.SelectionStart = DateTimePicker1.Value
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        MonthCalendar1.SelectionEnd = DateTimePicker2.Value
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        DateTimePicker1.Value = MonthCalendar1.SelectionStart
        DateTimePicker2.Value = MonthCalendar1.SelectionEnd
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Savebookinfo(selIndex, TextBox1.Text, TextBox2.Text, CheckBox1.Checked, DateTimePicker1.Value, DateTimePicker2.Value)
    End Sub
End Class