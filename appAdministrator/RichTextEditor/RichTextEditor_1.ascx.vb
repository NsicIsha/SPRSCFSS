
Partial Class RichTextEditor_RichTextEditor_1
    Inherits System.Web.UI.UserControl

    Public Property txtRichTextBox_1() As String
        Get
            Return txtRich_1.Text
        End Get
        Set(ByVal value As String)
            txtRich_1.Text = value
        End Set
    End Property

End Class
