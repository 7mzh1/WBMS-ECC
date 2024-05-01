Module connparam_release
    Public datasource As String
    Public username As String
    Public paswwd As String
    Public Sub setparams()
        datasource = gbcode
        username = "accts"
        paswwd = "accts"

    End Sub
    Public Sub dev_set()
        datasource = "alq"
        username = "accts"
        paswwd = "accts"
    End Sub
End Module
