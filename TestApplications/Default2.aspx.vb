
Partial Class TestApplications_Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim str As String = "sdv/dvds/vsdv"
        Dim arr As Array = str.Split("/")
        Dim a1 As String = arr(0)

    End Sub
End Class
