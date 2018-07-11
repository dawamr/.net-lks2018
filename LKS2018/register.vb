Imports MySql.Data.MySqlClient
Public Class register

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If (password.Text <> password2.Text) Then
            MsgBox("Password Belum Cocok", MsgBoxStyle.Information)
        End If
        If (password.Text = password2.Text) Then
            Call koneksi()
            Try
                cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO Users (name,email,password,role) VALUES ('" & nama.Text & "', '" & email.Text & "','" & password.Text & "','member');"
                cmd.ExecuteNonQuery()
                MsgBox("Berhasil Mendaftar !", MsgBoxStyle.Information)
                IndexMember.Show()
                Me.Hide()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles email.TextChanged

    End Sub
End Class