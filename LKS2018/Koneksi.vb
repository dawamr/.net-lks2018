Imports MySql.Data.MySqlClient
Module Module1

    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public str As String
    Public dt As New DataTable
    Public comBuilderDB As New MySql.Data.MySqlClient.MySqlCommandBuilder

    Sub koneksi()
        Try
            Dim str As String = "Server=localhost;user id=root;password=;database=lks2018"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Module