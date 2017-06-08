Imports System.Data.Common
Imports MySql.Data.MySqlClient

Public Class Transaction
    Inherits Databasic.Transaction
    Public Overrides Property Instance As DbTransaction
        Get
            Return Me._instance
        End Get
        Set(value As DbTransaction)
            Me._instance = value
        End Set
    End Property
    Private _instance As MySqlTransaction
End Class