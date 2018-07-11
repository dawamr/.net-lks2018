Imports MySql.Data.MySqlClient
Public Class forgotpassword
    Public user_id As Integer
    Friend email_true As Integer
    Private Sub forgotpassword_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Panel4.Visible = False
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim user_id = Login.user_id
        If TextBox1.Text = TextBox2.Text Then
            Call koneksi()
            str = "select * from users where id = '" & email_true & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows Then
                Call koneksi()
                str = "UPDATE users SET password = '" & TextBox1.Text & "' where id = '" & email_true & "' "
                cmd = New MySqlCommand(str, conn)
                cmd.ExecuteNonQuery()
                Login.Show()
                Me.Close()
            End If
        Else
            MsgBox("Password Correct!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        str = "select * from users where email = '" & email.Text & "'"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            rd.Read()
            email_true = CInt(rd("id"))
            Panel4.Visible = True
        Else
            MsgBox("Email Not Found!", MsgBoxStyle.Information)

        End If

    End Sub
End Class