Imports MySql.Data.MySqlClient
Public Class IndexMember
    Public item_id, user_id, _price As Integer

    Private Sub Index_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tampilDataComboBox()
        count()
        load_profile()
        ComboBox1.Focus()
        barang()
    End Sub
    Sub barang()
        Dim stoknya As Integer

        Call koneksi()
        str = "select * from items first "
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            judul.Text = CStr(rd.Item("name"))
            penulis.Text = CStr(rd.Item("penulis"))
            penerbit.Text = CStr(rd.Item("penerbit"))
            page_total.Text = CStr(rd.Item("total_page"))
            tipe.Text = CStr(rd.Item("cover_type"))
            _price = CInt(rd.Item("price"))
            harga.Text = CStr(_price)
            harga2.Text = CStr(rd.Item("price"))
            deskripsi.Text = CStr(rd.Item("description"))
            stoknya = CInt(rd.Item("stock"))
            item_id = CInt(rd.Item("id"))
            PictureBox2.ImageLocation = CStr(rd("path"))
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
            If (stoknya > 0) Then
                stok.Text = "Tersedia"
            End If
            If (stoknya <= 0) Then
                stok.Text = "Habis"
            End If
        End If
    End Sub

    Sub count()
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
    Sub tampilDataComboBox()
        Call koneksi()
        Dim str As String
        str = "select name from items Order By  name asc"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox1.Items.Add(rd("name"))
            Loop
        Else

        End If
    End Sub

    Public Sub ComboBox1_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles ComboBox1.SelectedValueChanged
        Dim stoknya As Integer

        Call koneksi()
        str = "select * from items where name ='" & ComboBox1.Text & "' "
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            judul.Text = CStr(rd.Item("name"))
            penulis.Text = CStr(rd.Item("penulis"))
            penerbit.Text = CStr(rd.Item("penerbit"))
            page_total.Text = CStr(rd.Item("total_page"))
            tipe.Text = CStr(rd.Item("cover_type"))
            _price = CInt(rd.Item("price"))
            harga.Text = CStr(_price)
            harga2.Text = CStr(rd.Item("price"))
            deskripsi.Text = CStr(rd.Item("description"))
            stoknya = CInt(rd.Item("stock"))
            item_id = CInt(rd.Item("id"))
            PictureBox2.ImageLocation = CStr(rd("path"))
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
            If (stoknya > 0) Then
                stok.Text = "Tersedia"
            End If
            If (stoknya <= 0) Then
                stok.Text = "Habis"
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox1.TextChanged
        If (TextBox1.Text = "Filter by name") Then
            TextBox1.ForeColor = Color.Silver
        End If
        If (TextBox1.Text <> "Filter by name") Then
            TextBox1.ForeColor = Color.Black

            Call koneksi()
            str = "select * from items where name like '%" & TextBox1.Text & "%'"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                ComboBox1.Items.Clear()
                Call koneksi()
                Dim str As String
                str = "select name from items where name like '%" & TextBox1.Text & "%'"
                cmd = New MySqlCommand(str, conn)
                rd = cmd.ExecuteReader
                If rd.HasRows Then
                    Do While rd.Read
                        ComboBox1.Items.Add(rd("name"))
                    Loop
                Else
                    ComboBox1.Items.Clear()
                End If
            End If

        End If

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As System.EventArgs) Handles ComboBox1.TextChanged
        If (ComboBox1.Text = "") Then
            ComboBox1.Text = "Pilih Buku"
        End If
    End Sub

    Private Sub qty_order_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles qty_order.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Masukan Hanya Angka !")
            e.Handled = True
        End If
    End Sub

    Private Sub addItem_Click(sender As System.Object, e As System.EventArgs) Handles addItem.Click
        'Dim code As String = "TOPAN" & Hour(Now()) & Minute(Now()) & Format(Date.Today)

        Try
            Call koneksi()
            Dim items, id, user_id, x As Integer
            Dim status, str As String
            user_id = Login.user_id
            str = "select * from transaction_details where item_id = '" & item_id & "'and status = 'proses' and user_id = '" & user_id & "';"
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                status = CStr(rd("status"))
                items = CInt(rd("item_id"))
                id = CInt(rd("id"))
                x = CInt(rd("qty"))
                If (status = "proses") Then
                    Call koneksi()
                    str = "UPDATE transaction_details SET qty = '" & qty_order.Text & "', total = '" & _price * CInt(qty_order.Text) & "' WHERE id = '" & id & "';"
                    cmd = New MySqlCommand(str, conn)
                    cmd.ExecuteNonQuery()
                    cmd = conn.CreateCommand

                    cmd.CommandText = "UPDATE items SET stock = stock +'" & x - CInt(qty_order.Text) & "' where id = '" & items & "'"
                    cmd.ExecuteNonQuery()
                    count()
                End If
            Else
                Call koneksi()
                cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO transaction_details (item_id, user_id, qty,status) VALUES ('" & item_id & "','" & user_id & "', '" & qty_order.Text & "','proses');"
                cmd.ExecuteNonQuery()
                cmd = conn.CreateCommand
                cmd.CommandText = "UPDATE items SET stock = stock -'" & qty_order.Text & "' where id = '" & items & "'"
                cmd.ExecuteNonQuery()
                count()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Panel4_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Label9_Click(sender As System.Object, e As System.EventArgs) Handles Label9.Click
        ProfileMember.Show()
        Me.Close()
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
                Label4.Text = CStr(rd("name"))
                Label2.Text = CStr(rd("name"))
                PictureBox1.ImageLocation = CStr(rd("img_profile"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    Private Sub qty_order_TextChanged(sender As Object, e As System.EventArgs) Handles qty_order.TextChanged
        If CDbl(qty_order.Text) = vbNull Or qty_order.Text = "" Then
            qty_order.Text = CStr(0)
        End If
        If CDbl(qty_order.Text) > 10 Then
            qty_order.Text = CStr(10)
        End If
        harga2.Text = CStr(CInt(harga.Text) * CInt(qty_order.Text))
    End Sub
    Private Sub Label14_Click(sender As System.Object, e As System.EventArgs) Handles Label14.Click
        cart.Show()
        Me.Close()
    End Sub

End Class