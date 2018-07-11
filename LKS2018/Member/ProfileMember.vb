Imports MySql.Data.MySqlClient
Public Class ProfileMember
    Private Path As String = Nothing


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Visible = True
        Label12.Visible = True
        TextBox4.Enabled = True
        Label7.Text = "Change Profile"
        Panel12.Visible = True
        Button5.Visible = True
        Button4.Visible = True


    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        On Error Resume Next
        OpenFileDialog1.Filter = "JPG FILES(*jpg)|*.jpg|JPEG FILEA(*jpeg)|*.jpeg|PNG FILES (*png)|*.png"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox3.Image = New Bitmap(OpenFileDialog1.FileName)
            Path = OpenFileDialog1.FileName
            TextBox5.Text = Path.Substring(Path.LastIndexOf("\") + 1)
            TextBox6.Text = OpenFileDialog1.FileName.Trim
            PictureBox3.Image = Image.FromFile(TextBox6.Text)
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox4.Visible = False
        Label12.Visible = False
        Label7.Text = "My Profile"
        Panel12.Visible = False
        Button5.Visible = False
        Button4.Visible = False
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Call koneksi()
        Dim user_id As Integer = Login.user_id
        Try
            str = "select id from users where id ='" & user_id & "' "
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            If rd.HasRows Then
                Call koneksi()
                str = "UPDATE USERS SET name = '" & TextBox1.Text & "', email =  '" & TextBox2.Text & "', password ='" & TextBox3.Text & "', img_profile =  '" & CChar(TextBox6.Text.Trim) & "' WHERE id = '" & user_id & "' "
                cmd = New MySqlCommand(str, conn)
                cmd.ExecuteNonQuery()
                load_profile()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ProfileMember_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_profile()
        Label12.Visible = True
        Button4.Visible = False
        Button5.Visible = False
        TextBox4.Visible = False
        Panel12.Visible = False
        count()
    End Sub
    Sub count()
        Dim user_id As Integer
        Call koneksi()
        user_id = Login.user_id
        str = "SELECT COUNT(*) FROM transaction_details where status = 'proses' and user_id = '" & user_id & "'"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            rd.Read()
            qty.Text = CStr(rd("COUNT(*)"))
        End If
    End Sub
    Sub load_profile()
        Call koneksi()
        Dim user_id As Integer = Login.user_id

        Try
            str = "select * from users where id ='" & user_id & "' "
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If rd.HasRows Then
                TextBox1.Text = CStr(rd("name"))
                TextBox2.Text = CStr(rd("email"))
                Label4.Text = CStr(rd("name"))
                Label2.Text = CStr(rd("name"))
                nama.Text = CStr(rd("name"))
                PictureBox2.ImageLocation = CStr(rd("img_profile"))
                PictureBox1.ImageLocation = CStr(rd("img_profile"))
                PictureBox3.ImageLocation = CStr(rd("img_profile"))
                TextBox5.Text = CStr(rd("img_profile"))
                TextBox6.Text = CStr(rd("img_profile"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click
        IndexMember.Show()
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click
        If Login.CheckBox1.Checked = False Then
            Login.Close()
            Index.Show()
            Me.Close()
        End If
        If Login.CheckBox1.Checked = True Then
            Index.Show()
            Me.Close()
        End If
    End Sub


    Private Sub Label14_Click(sender As System.Object, e As System.EventArgs) Handles Label14.Click
        cart.Show()
        Me.Close()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text <> TextBox3.Text Then
            TextBox4.ForeColor = Color.Red
        End If
        If TextBox4.Text = TextBox3.Text Then
            TextBox4.ForeColor = Color.Black
        End If

    End Sub

    Private Sub TextBox6_DoubleClick(sender As Object, e As System.EventArgs) Handles TextBox6.DoubleClick
        TextBox6.Enabled = True
    End Sub
End Class