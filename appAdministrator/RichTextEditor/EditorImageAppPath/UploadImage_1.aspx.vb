Imports System
Imports System.Drawing
Imports System.IO
Imports System.IO.File
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math

Partial Class RichTextEditor_EditorImageAppPath_UploadImage_1
    Inherits System.Web.UI.Page

    'Dim pathstr As String
    'Dim currentDir, currentDirParent, fileSaveDir, fileSaveDir1, fileSaveDir2, fileSaveDir3, fromPage, fileSaveTempDir As String
    'Dim newFileName, ImageDir, Banner_Image As String
    'Dim directorySeparatorChar As Char = Path.DirectorySeparatorChar
    'Dim flgadd, flgedit, flgdelete, flgpublish, flglive, flgproperties As Integer
    'Dim moduleid As Integer
    'Dim cnt, ct, cntr, colcnt As Integer
    'Dim operation_str As String
    'Dim role, SaveLocation As String
    'Dim uid, access, operationid As Integer
    'Dim ImageTag As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("USERCMSinfo") Is Nothing Then
        '    Response.Redirect("Error_admin.aspx")
        'End If

        'btnCancel.Attributes.Add("onclick", "return funImageControlClose_1();")

        'Dim objoperation As New operation
        'Dim objmodule As New modules
        'Dim usrmodule As New Modules
        'For Each usrmodule In usrmodule.getModule
        '    Dim btnname As String
        '    Dim id As Integer
        '    btnname = (usrmodule.fModule)
        '    id = usrmodule.fModuleId
        '    Dim chkmodulename As Integer
        '    chkmodulename = btnname.ToLower().Contains("site")
        '    If (chkmodulename = -1) Then
        '        moduleid = id
        '    End If
        'Next

        'role = UCase(Request.Cookies("USERCMSinfo")("Userid"))

        'Dim usrinform1 As New user
        'Dim usrroleid1 As String
        'For Each usrinform1 In usrinform1.getUserInfoById(role)
        '    usrroleid1 = usrinform1.fRoleId.ToString
        'Next

        'operation_str = ""

        'For Each objoperation In objoperation.getOperation()
        '    Dim out, flag As String
        '    flag = "false"
        '    out = objmodule.getPermissionByRoleModuleid(usrroleid1, moduleid)
        '    Dim arr1 As Array
        '    arr1 = Split(out, ",")
        '    ct = UBound(arr1)
        '    cntr = 0
        '    ct = ct + 1
        '    Dim str As String

        '    If out <> "" Then

        '        While (cntr < ct)

        '            If arr1(cntr) = objoperation.fOperationId Then
        '                flag = "true"
        '                operation_str = operation_str & "," & objoperation.fOperation

        '            End If
        '            cntr = cntr + 1
        '        End While
        '    End If
        'Next


        'If operation_str.ToLower.Contains("add") Then
        '    flgadd = 1
        'End If

        'If operation_str.ToLower.Contains("edit") Then
        '    flgedit = 1
        'End If

        'If operation_str.ToLower.Contains("delete") Then
        '    flgdelete = 1
        'End If

        'If operation_str.ToLower.Contains("publish") Then
        '    flgpublish = 1
        'End If

        'If operation_str.ToLower.Contains("live") Then
        '    flglive = 1
        'End If

        'If operation_str.ToLower.Contains("properties") Then
        '    flgproperties = 1
        'End If



        'pathstr = Request.QueryString("path")

        'uid = Convert.ToInt32(UCase(Request.Cookies("USERCMSinfo")("Userid")))


        'funShowImages()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        'Dim filename As String = ""
        'Dim fileSaveTempDir As String = ""
        'Dim newFileName As String = ""

        'If (flgadd <> 1 And uid <> 0) Then
        '    ClientScript.RegisterStartupScript(GetType(String), "Alert", "<script language='javascript'>alert('You Are Not Authorised For Add Operation !!');</script>")
        'ElseIf (flgadd = 1 Or uid = 0) Then
        '    'Start---------------------------------------------------------
        '    'Upload Image
        '    If Not (flupldImage.PostedFile Is Nothing) Then
        '        fileSaveTempDir = ReadFile()

        '        Dim postedFile = flupldImage.PostedFile
        '        filename = Path.GetFileNameWithoutExtension(postedFile.FileName)

        '        If filename <> "" Then
        '            Dim contentType As String = postedFile.ContentType
        '            Dim contentLength As Integer = postedFile.ContentLength
        '            newFileName = filename
        '            newFileName &= DateTime.Now.Year
        '            newFileName &= DateTime.Now.Month
        '            newFileName &= DateTime.Now.Day
        '            newFileName &= "_"
        '            newFileName &= DateTime.Now.Hour
        '            newFileName &= DateTime.Now.Minute
        '            newFileName &= DateTime.Now.Second
        '            newFileName &= "_"
        '            newFileName &= DateTime.Now.Millisecond

        '            newFileName &= Path.GetExtension(postedFile.FileName)

        '            Dim strSaveaspath As String = Server.MapPath(".") & "\../../" & fileSaveTempDir & newFileName
        '            postedFile.SaveAs(strSaveaspath)


        '            Dim obj As New Descimage
        '            obj.addDescImage(newFileName, pathstr, uid)
        '        End If
        '        'Upload Image
        '        'End---------------------------------------------------------

        '        Dim ImagePath As String = ""
        '        Dim ImageAlt As String = ""
        '        Dim ImageAlignment As String = ""
        '        Dim ImageWidth As Integer = 0
        '        Dim ImageHeight As Integer = 0
        '        Dim ImageBorder As Integer = 0
        '        Dim ImageHSpave As Integer = 0
        '        Dim ImageVSpace As Integer = 0

        '        Try
        '            ImagePath = ReadFile("sd") & newFileName
        '        Catch ex As Exception
        '            ImagePath = ""
        '        End Try

        '        Try
        '            ImageAlt = txtAlt.Text.ToString()
        '        Catch ex As Exception
        '            ImageAlt = ""
        '        End Try

        '        Try
        '            ImageAlignment = ddlAlignment.SelectedValue
        '        Catch ex As Exception
        '            ImageAlignment = ""
        '        End Try

        '        Try
        '            ImageWidth = CInt(txtWidth.Text)
        '        Catch ex As Exception
        '            ImageWidth = 0
        '        End Try

        '        Try
        '            ImageHeight = CInt(txtHeight.Text)
        '        Catch ex As Exception
        '            ImageHeight = 0
        '        End Try

        '        Try
        '            ImageBorder = CInt(txtBorder.Text)
        '        Catch ex As Exception
        '            ImageBorder = 0
        '        End Try

        '        Try
        '            ImageHSpave = CInt(txtHSpace.Text)
        '        Catch ex As Exception
        '            ImageHSpave = 0
        '        End Try

        '        Try
        '            ImageVSpace = CInt(txtVSpace.Text)
        '        Catch ex As Exception
        '            ImageVSpace = 0
        '        End Try




        '        ImageTag = ""

        '        ImageTag = "<img src='" & ImagePath
        '        ImageTag &= "' alt='" & ImageAlt
        '        ImageTag &= "' style='"
        '        ImageTag &= "text-align:" & ImageAlignment & "; "
        '        If ImageWidth <> 0 Then
        '            ImageTag &= "width:" & ImageWidth & "px; "
        '        End If
        '        If ImageHeight <> 0 Then
        '            ImageTag &= "height:" & ImageHeight & "px; "
        '        End If
        '        'ImageTag &= "border-width:" & ImageBorder & "px; "
        '        ImageTag &= "left:" & ImageHSpave & "px; "
        '        ImageTag &= "top:" & ImageVSpace & "px;'"
        '        ImageTag &= " border='" & ImageBorder & "px'"
        '        ImageTag &= " />"
        '    End If
        'End If





        'If (Not Page.ClientScript.IsStartupScriptRegistered("alert")) Then
        '    If ImageTag = "" Then
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "funImageNotSelect_1();", True)
        '    Else
        '        Dim strImageTag As String
        '        strImageTag = "funImageSelect_1(""" & ImageTag & """);"
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", strImageTag, True)
        '    End If

        'End If
    End Sub

    Function ReadFile() As String
        'Dim FileName As String
        ''Add Reference System.web
        'FileName = System.Web.HttpContext.Current.Request.MapPath("./ImagePath.txt")
        Dim Contents As String
        Contents = ""
        'If File.Exists(FileName) Then
        '    Dim objstramreader As StreamReader
        '    objstramreader = File.OpenText(FileName)
        '    Contents = objstramreader.ReadToEnd
        '    objstramreader.Close()
        'End If
        Return Contents
    End Function

    Function ReadFile(ByVal ForShowImage As String) As String
        'Dim FileName As String
        ''Add Reference System.web
        'FileName = System.Web.HttpContext.Current.Request.MapPath("./ImagePathSet.txt")
        Dim Contents As String
        Contents = ""
        'If File.Exists(FileName) Then
        '    Dim objstramreader As StreamReader
        '    objstramreader = File.OpenText(FileName)
        '    Contents = objstramreader.ReadToEnd
        '    objstramreader.Close()
        'End If
        Return Contents
    End Function

    Sub funShowImages()
        'Dim obj As New Descimage
        ''AddHandler addnew.Click, New EventHandler(AddressOf AddGroup)
        'Dim row As TableRow
        'Dim cell As TableCell
        'Dim headcell As TableCell
        'Dim headcell1 As TableCell
        'Dim headcell2 As TableCell
        'Dim headcell3 As TableCell
        'Dim headcell4 As TableCell
        'Dim headrow As TableRow
        'Dim counter As Integer

        'counter = 0
        'If counter = 0 Then
        '    headrow = New TableRow
        '    headcell = New TableCell
        '    headcell.Text = "<b>S.No</b><br>"
        '    headcell.Width = 10
        '    headrow.Cells.Add(headcell)

        '    headcell1 = New TableCell
        '    headcell1.Text = "<b>Image</b><br>"
        '    headcell1.Font.Size = 10
        '    headcell1.Width = 100
        '    headrow.Cells.Add(headcell1)

        '    headcell2 = New TableCell
        '    headcell2.Text = "<b>Name</b><br>"
        '    headcell2.Font.Size = 10
        '    headcell2.Width = 40

        '    headrow.Cells.Add(headcell2)

        '    headcell3 = New TableCell
        '    headcell3.Text = "<b>Delete</b><br>"
        '    headcell3.Font.Size = 10
        '    headcell3.Width = 20
        '    If flgdelete = 1 Or role = 0 Then
        '        headrow.Cells.Add(headcell3)
        '    End If
        '    tblimage.Rows.Add(headrow)
        '    headrow.BackColor = Color.FromName("#a2b2ce")
        'End If


        ''Dim obj As New Homegroups
        'Dim imagename As TableCell
        'Dim deletecell As TableCell
        'Dim sn As TableCell
        'Dim img As TableCell

        'counter = counter + 1
        'For Each obj In obj.getDescImageId(pathstr)
        '    Dim Imgname As String
        '    Imgname = obj.fFilename
        '    headrow = New TableRow
        '    sn = New TableCell
        '    sn.Text = counter
        '    imagename = New TableCell
        '    deletecell = New TableCell
        '    Dim btnhireDelete As New LinkButton
        '    btnhireDelete.CommandArgument = obj.fImageId & "|" & Imgname
        '    btnhireDelete.Text = "Delete"
        '    btnhireDelete.CssClass = "leftlink_1"
        '    btnhireDelete.Attributes("onclick") = "javascript:return " & _
        '             "confirm('Are you sure you want to delete ?')"
        '    AddHandler btnhireDelete.Click, New EventHandler(AddressOf DeleteDescImage)
        '    deletecell.Font.Bold = False
        '    deletecell.Controls.Add(btnhireDelete)
        '    img = New TableCell
        '    If InStr(UCase(Imgname), "HTTP://") Then
        '        img.Text = "<img src=" & Imgname & " height='50' width='50' style='cursor:pointer;' onclick='funImageHoverInsert_1(""" & Imgname & """)'>"
        '    Else
        '        img.Text = "<img src=../../" & ReadFile() & Imgname & " height='50' width='50' style='cursor:pointer;' onclick='funImageHoverInsert_1(""" & ReadFile() & Imgname & """)'>"
        '    End If
        '    imagename.Text = Imgname
        '    headrow.Cells.Add(sn)
        '    headrow.Cells.Add(img)
        '    headrow.Cells.Add(imagename)
        '    If flgdelete = 1 Or role = 0 Then
        '        headrow.Cells.Add(deletecell)
        '    End If
        '    tblimage.Rows.Add(headrow)
        '    counter = counter + 1
        'Next
    End Sub

    Protected Sub DeleteDescImage(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim delid As Integer

        'uid = Convert.ToInt32(UCase(Request.Cookies("USERCMSinfo")("Userid")))

        'If (flgdelete <> 1 And uid <> 0) Then
        '    ClientScript.RegisterStartupScript(GetType(String), "Alert", "<script language='javascript'>alert('You Are Not Authorised For Delete Operation !!');</script>")
        'ElseIf (flgdelete = 1 Or uid = 0) Then
        '    Dim obj As New Descimage
        '    Dim id As Integer
        '    Dim btnDelete As LinkButton
        '    btnDelete = CType(sender, LinkButton)
        '    Dim a As Array
        '    Dim str As String
        '    str = btnDelete.CommandArgument.ToString
        '    a = Split(str, "|")
        '    id = Convert.ToInt32(a(0))
        '    Dim filename As String
        '    filename = a(1)
        '    'id = btnDelete.CommandArgument
        '    obj.DeleteDescImage(id, uid)

        '    If InStr(UCase(filename), "HTTP://") Then
        '        SaveLocation = filename
        '    Else
        '        SaveLocation = ReadFile() & filename
        '    End If


        '    IO.File.Delete(Server.MapPath(".") & "\../../" & SaveLocation)

        '    Response.Redirect(Request.FilePath() & "?path=" & Request("path"))
        'End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    End Sub
End Class
