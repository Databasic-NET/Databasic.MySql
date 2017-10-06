Imports MySql.Data.MySqlClient

Public Class SqlError
    Inherits Databasic.SqlError

    Public Property Level As String

    Public Sub New(mySqlError As Global.MySql.Data.MySqlClient.MySqlError)
        Me.Message = mySqlError.Message
        Me.Code = mySqlError.Code

        Me.Level = mySqlError.Level
    End Sub

End Class