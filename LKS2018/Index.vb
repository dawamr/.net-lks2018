Imports MySql.Data.MySqlClient
Public Class Index

    Private Sub Index_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call tampilDataComboBox()
        TextBox1.Text = "Filter Buku"
        barang()
    End Sub

    Sub tampilDataComboBox()
        Call koneksi()
        Dim str As String
        str = "select name from items"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox1.Items.Add(rd("name"))
            Loop
        Else

        End If
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles ComboBox1.SelectedValueChanged
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
            harga.Text = CStr(rd.Item("price"))
            deskripsi.Text = CStr(rd.Item("description"))
            stoknya = CInt(rd.Item("stock"))
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
        If (TextBox1.Text = "Filter Buku") Then
            TextBox1.ForeColor = Color.Silver
        End If
        If (TextBox1.Text <> "Filter Buku") Then
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

    Private Sub Label9_Click(sender As System.Object, e As System.EventArgs) Handles Label9.Click
        Login.Show()
        Me.hide()
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
            harga.Text = CStr(rd.Item("price"))
            deskripsi.Text = CStr(rd.Item("description"))
            stoknya = CInt(rd.Item("stock"))
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

    Private Sub Panel6_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel6.Paint

    End Sub



    Private Sub Panel9_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class