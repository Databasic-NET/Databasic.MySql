Imports Databasic.ActiveRecord

Public Class Resource
    Inherits Provider.Resource

    Public Overrides Function GetTableColumns(connection As Databasic.Connection, table As String) As List(Of String)
        Dim result As New List(Of String)
		Return Databasic.Statement.Prepare(
				"SELECT c.COLUMN_NAME
                FROM INFORMATION_SCHEMA.COLUMNS AS c
                WHERE
                    c.TABLE_SCHEMA = @database AND
                    c.TABLE_NAME = @table
                ORDER BY c.ORDINAL_POSITION",
				connection
			).FetchAll(New With {
				.database = connection.Provider.Database,
				.table = table
			}).ToList(Of String)()
	End Function

    Public Overrides Function GetLastInsertedId(transaction As Databasic.Transaction) As Object
		Return Databasic.Statement.Prepare("SELECT LAST_INSERT_ID()", transaction).FetchOne().ToInstance(Of Object)()
	End Function

    Public Overrides Function GetAll(
            connection As Databasic.Connection,
            columns As String,
            table As String,
            Optional offset As Int64? = Nothing,
            Optional limit As Int64? = Nothing,
            Optional orderByStatement As String = Database.DEFAUT_UNIQUE_COLUMN_NAME
        ) As Databasic.Statement
        Dim sql = $"SELECT {columns} FROM {table}"
        offset = If(offset, 0)
        limit = If(limit, 0)
        If limit > 0 Then
            sql += If(orderByStatement.Length > 0, " ORDER BY " + orderByStatement, "") +
                    $" LIMIT {If(limit = 0, "18446744073709551615", limit.ToString())} OFFSET {offset}"
        End If
		Return Databasic.Statement.Prepare(sql, connection).FetchAll()
	End Function

End Class