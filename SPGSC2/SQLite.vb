Imports System.Data.SQLite

Module SQLite

    Public connection As SQLiteConnection
    Public database As String = "BaseDeDados.db"

    Sub connect()
        Try
            connection = New SQLiteConnection("Data Source=" & database)
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If
        Catch ex As Exception
            MsgBox("Erro ao conectar à base de dados.")
        End Try
    End Sub

    Public Sub RunSQL(ByVal sql As String)
        Try
            connect()
            Dim cmd As New SQLiteCommand
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            connection.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function ReadDataDT(ByVal sql As String) As DataTable
        Dim dt As New DataTable
        Try
            connect()
            Dim da As New SQLiteDataAdapter(sql, connection)
            da.Fill(dt)
            connection.Close()
            da.Dispose()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return dt
    End Function

    'Public Sub InsertRecord(ByVal sql As String)
    '    Try
    '        connect()
    '        Dim da As New SQLiteDataAdapter(sql, connection)
    '        connection.Close()
    '        da.Dispose()
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub
End Module
