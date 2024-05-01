Imports SAP.Middleware.Connector


Public Class sap_cfg
    Implements IDestinationConfiguration


    Public Function ChangeEventsSupported() As Boolean Implements IDestinationConfiguration.ChangeEventsSupported
        Return False
    End Function

    Public Event ConfigurationChanged(destinationName As String, args As RfcConfigurationEventArgs) Implements IDestinationConfiguration.ConfigurationChanged

    Public Function GetParameters(destinationName As String) As RfcConfigParameters Implements IDestinationConfiguration.GetParameters
        Dim parms As New RfcConfigParameters

        Select Case destinationName

            Case "AGD"

                'parms.Add(RfcConfigParameters.AppServerHost, "172.18.19.225") 'Dev
                'parms.Add(RfcConfigParameters.AppServerHost, "172.18.19.237") 'Quality
                parms.Add(RfcConfigParameters.AppServerHost, "172.18.19.237") '550
                'parms.Add(RfcConfigParameters.AppServerHost, "172.18.19.230") '600
                'parms.Add(RfcConfigParameters.SystemNumber, "00") 'Dev
                'parms.Add(RfcConfigParameters.SystemNumber, "01") 'Quality
                parms.Add(RfcConfigParameters.SystemNumber, "01") '550
                'parms.Add(RfcConfigParameters.SystemNumber, "02") '600
                'parms.Add(RfcConfigParameters.SystemID, "AGD") 'Dev
                'parms.Add(RfcConfigParameters.SystemID, "AGQ") 'Quality
                parms.Add(RfcConfigParameters.SystemID, "AGQ") '550
                'parms.Add(RfcConfigParameters.SystemID, "AGP") '600
                'parms.Add(RfcConfigParameters.Client, "200") 'Dev
                'parms.Add(RfcConfigParameters.Client, "450") 'Quality
                parms.Add(RfcConfigParameters.Client, "550") '550
                'parms.Add(RfcConfigParameters.Client, "600") '600
                parms.Add(RfcConfigParameters.User, "connector")
                parms.Add(RfcConfigParameters.Password, "Pa55w0rd")
                parms.Add(RfcConfigParameters.Language, "EN")
                parms.Add(RfcConfigParameters.PoolSize, "5")
                'parms.Add(RfcConfigParameters.MaxPoolSize, "300")
                'parms.Add(RfcConfigParameters.IdleTimeout, "240")
                parms.Add(RfcConfigParameters.PeakConnectionsLimit, "300")
                parms.Add(RfcConfigParameters.ConnectionIdleTimeout, "240")
                'parms.Add(RfcConfigParameters.GatewayHost, "agddb01")

            Case Else

        End Select

        Return parms
    End Function
End Class
