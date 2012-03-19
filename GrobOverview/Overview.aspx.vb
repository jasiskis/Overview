Imports System.Web
Public Class Overview

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function fazAlgo() As Integer
        Return 1
    End Function
End Class