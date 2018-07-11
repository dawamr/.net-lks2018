Imports MySql.Data.MySqlClient
Public Class Login
    Public user_id As Integer
    Public check As String
    
    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click
        register.Show()
        Me.Hide()
    End Sub

    Public Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Call koneksi()
        Try
            Dim str, rolenya As String
            str = "select * from users where email = '" & email.Text & "' and password = '" & password.Text & "'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                user_id = CInt(rd.Item("id"))
                rolenya = CStr(rd.Item("role"))
                    If (rolenya = "member") Then
                        IndexMember.Show()
                        Me.Hide()
                    End If
                    If (rolenya = "operator") Then
                        MsgBox("Operator")
                    End If
                    If (rolenya = "petugas") Then
                        MsgBox("Petugas")
                    End If

                If CheckBox1.Checked = True Then
                    check = "true"
                End If
                If CheckBox1.Checked = True Then
                    check = "false"
                End If
            Else
                MsgBox("Email or password in correct!", MsgBoxStyle.Information)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click
        forgotpassword.Show()
        Me.Hide()
    End Sub
End Class