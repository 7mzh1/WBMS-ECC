Public Structure cmbload
    Private descs As String
    Private values As String
    Public Sub New(ByVal names As String, ByVal ids As String)
        descs = names
        values = ids
    End Sub
    Public ReadOnly Property names() As String
        Get
            Return descs
        End Get
    End Property
    Public ReadOnly Property ids() As String
        Get
            Return values
        End Get
    End Property
End Structure