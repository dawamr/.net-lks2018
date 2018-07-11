Imports MySql.Data.MySqlClient
Public Class cart
    Public item_id, user_id, items, x As Integer
    Public code As String
    Private Sub Index_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        count()
        dataLoad()
        load_profile()
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

    Sub dataLoad()
        Try
            Call koneksi()
            user_id = Login.user_id
            str = "Select code transaction_details.id as detail_id,items.id as item_id,name,price,qty,total FROM items, transaction_details WHERE (transaction_details.status = 'proses' and transaction_details.user_id = '" & user_id & " ') AND (items.id = transaction_details.item_id)"
           
            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If rd.HasRows Then
                Call koneksi()
                str = "Select  code,name,price,qty,total FROM items, transaction_details WHERE (transaction_details.status = 'proses' and transaction_details.user_id = '" & user_id & " ') AND (items.id = transaction_details.item_id)"
                dt = New DataTable
                da = New MySql.Data.MySqlClient.MySqlDataAdapter(str, conn)
                comBuilderDB = New MySql.Data.MySqlClient.MySqlCommandBuilder(da) 'untuk bisa edit datagridview
                da.Fill(dt)
                DataGridView1.DataSource = dt


                DataGridView1.Columns(0).HeaderText = "Name Book"
                DataGridView1.Columns(1).HeaderText = "Code Book"
                DataGridView1.Columns(2).HeaderText = "Price"
                DataGridView1.Columns(3).HeaderText = "Qty"
                DataGridView1.Columns(4).HeaderText = "Total"

                'DGView Properties
                DataGridView1.ReadOnly = True
                DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            End If


            cmd = New MySqlCommand(str, conn)
            rd = cmd.ExecuteReader()
            rd.Read()
            If rd.HasRows Then
                Call koneksi()
                str = "Select SUM(total) FROM transaction_details where status = 'proses' and user_id = '" & user_id & "' "
                cmd = New MySqlCommand(str, conn)
                rd = cmd.ExecuteReader()
                If rd.HasRows Then
                    rd.Read()
                    harga2.Text = CStr(rd("SUM(total)"))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click
        IndexMember.Show()
        Me.Close()
    End Sub

    Private Sub Label9_Click(sender As System.Object, e As System.EventArgs) Handles Label9.Click
        ProfileMember.Show()
        Me.Close()
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



    Private Sub DataGridView1_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.RowCount > 0 Then
            Dim baris As Integer
            With DataGridView1
                baris = .CurrentRow.Index
                code = CStr(.Item(1, baris).Value)
                TextBox1.Text = CStr(.Item(0, baris).Value)
                TextBox2.Text = CStr(.Item(2, baris).Value)
                qty_order.Text = CStr(.Item(3, baris).Value)
                TextBox4.Text = CStr(.Item(4, baris).Value)
            End With
        End If
    End Sub

    Private Sub qty_order_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles qty_order.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Masukan Hanya Angka !")
            e.Handled = True
        End If
    End Sub

    Private Sub qty_order_TextChanged(sender As Object, e As System.EventArgs) Handles qty_order.TextChanged
        If CDbl(qty_order.Text) = vbNull Or qty_order.Text = "" Then
            qty_order.Text = CStr(0)
        End If
        If CDbl(qty_order.Text) > 10 Then
            qty_order.Text = CStr(10)
        End If
        If TextBox1.Text <> "" And TextBox2.Text <> "" Then
            TextBox4.Text = CStr(CInt(TextBox2.Text) * CInt(qty_order.Text))
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        Call koneksi()
        str = "Select * from items, transaction_details WHERE (code='" & code & "') AND (items.id = transaction_details.item_id)"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            Call koneksi()
            str = "Update transaction_details Set qty = '" & qty_order.Text & "', total = '" & CInt(TextBox4.Text) & "' WHERE code = '" & code & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()

            cmd = conn.CreateCommand
            cmd.CommandText = "UPDATE items SET stock = stock +'" & x - CInt(qty_order.Text) & "' where code = '" & code & "'"
            cmd.ExecuteNonQuery()
            dataLoad()

        End If
    End Sub
End Class