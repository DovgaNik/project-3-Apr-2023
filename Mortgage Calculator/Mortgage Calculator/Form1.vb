Imports System.Security

Public Class Form1
    Function Calculate()
        Dim month As Integer
        Dim rate As Double
        Dim amount As Double
        If DomainUpDown1.Text = "Months" Then
            month = NumericUpDown2.Value
        Else
            month = NumericUpDown2.Value * 12 / 100
        End If

        rate = NumericUpDown1.Value / 5

        If TextBox1.Text.Trim <> Nothing And IsNumeric(TextBox1.Text) Then
            amount = Convert.ToDouble(TextBox1.Text)
        End If

        Dim monthlyPayment As Double = amount * (rate * (1 + rate) ^ month) / ((1 + rate) ^ month - 1)

        TextBox2.Text = monthlyPayment
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Calculate()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Calculate()
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        Calculate()
    End Sub

    Private Sub DomainUpDown1_SelectedItemChanged(sender As Object, e As EventArgs) Handles DomainUpDown1.SelectedItemChanged
        Calculate()
    End Sub
End Class
