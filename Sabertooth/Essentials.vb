Imports System.IO
Imports System.Net

Module Essentials
    Public Sub RSleep(ByRef iMilliSeconds As Integer)
        For i As Integer = 0 To iMilliSeconds / 5
            Application.DoEvents()
            System.Threading.Thread.Sleep(5)
        Next i
    End Sub

    Public Sub checkFolders()
        For Each pt As String In My.Settings.paths
            Try
                For Each fol As String In My.Settings.folders
                    Try
                        If Not Directory.Exists(pt & "\" & fol.Substring(3)) Then
                            Directory.CreateDirectory(pt & "\" & fol.Substring(3))
                        End If
                    Catch ex As Exception
                        Debug.Print(ex.ToString)
                    End Try
                Next

                If Not Directory.Exists(pt & "\Folder") Then
                    Try
                        Directory.CreateDirectory(pt & "\Folder")
                    Catch ex As Exception
                        Debug.Print(ex.ToString)
                    End Try

                    My.Settings.folders.Add("[R]Folder")
                    My.Settings.Save()
                    My.Settings.Reload()
                End If

                If Not Directory.Exists(pt & "\Other") Then
                    Try
                        Directory.CreateDirectory(pt & "\Other")
                    Catch ex As Exception
                        Debug.Print(ex.ToString)
                    End Try

                    My.Settings.folders.Add("[R]Other")
                    My.Settings.Save()
                    My.Settings.Reload()
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        Next
    End Sub

    Public Sub Unorganize(target As String)
        For Each fol As String In My.Settings.folders
            If fol = "Folder" Then
                Continue For
            End If

            For Each file As String In Directory.EnumerateFiles(target & "\" & fol.Substring(3), "*.*")
                Try
                    IO.File.Move(file, target & "\" & Path.GetFileName(file))
                Catch ex As Exception
                    Debug.Print(ex.ToString)
                End Try
            Next

            If Not Directory.EnumerateFiles(target & "\" & fol.Substring(3), "*.*").Count >= 1 Then
                Try
                    Directory.Delete(target & "\" & fol.Substring(3))
                Catch ex As Exception
                    Debug.Print(ex.ToString)
                End Try
            End If
        Next

        For Each fol As String In Directory.EnumerateDirectories(target & "\" & "Folder", "*")
            Try
                Directory.Move(fol, target & "\" & fol.Substring(fol.LastIndexOf("\") + 1))
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        Next

        If Not Directory.EnumerateDirectories(target & "\" & "Folder", "*").Count >= 1 Then
            Try
                Directory.Delete(target & "\" & "Folder")
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        End If
    End Sub

    Public Sub Organize(target As String, Optional fil As Boolean = True, Optional fol As Boolean = True)
        checkFolders()

        ' Organize Files ====

        If fil Then
            Try
                Dim flst As IEnumerable(Of String) = Directory.EnumerateFiles(target, "*.*")

                For Each fl As String In flst ' Get all file dirs
                    sortFile(fl, target)
                Next

                If My.Settings.recursive Then
                    For Each fold As String In My.Settings.folders
                        For Each fl As String In Directory.EnumerateFiles(target & "\" & fold.Substring(3), "*.*")
                            sortFile(fl, target)
                        Next
                    Next
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        End If

        ' Organize Folders ====

        If fol Then
            Try
                For Each fl As String In Directory.EnumerateDirectories(target, "*") ' Get all folder dirs
                    sortFolder(fl, target)
                Next

                If My.Settings.recursive Then
                    For Each fold As String In My.Settings.folders
                        For Each fl As String In Directory.EnumerateDirectories(target & "\" & fold.Substring(3), "*")
                            sortFolder(fl, target)
                        Next
                    Next
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub sortFile(fl As String, target As String)
        Dim ext As String = Path.GetExtension(fl).ToLower().Replace("ı", "i")
        Debug.Print(ext)
        Dim newpath As String = target & "\"
        Dim filename As String = Path.GetFileName(fl)

        If My.Settings.whiteList.Contains(ext) Then ' Check if extension is whitelisted
            Return
        End If

        If Not My.Settings.extensions.Contains(ext) Then ' Not filtered ==> Other
            newpath = newpath & "Other\" & filename
        Else                                             ' Filtered
            newpath = newpath & My.Settings.extensions(My.Settings.extensions.IndexOf(ext) + 1).Substring(3) & "\" & filename
        End If

        ' Try to move the file

        Try
            Debug.Print("======================================================================")
            Debug.Print("File:" & fl)
            Debug.Print("New Path:" & newpath)
            File.Move(fl, checkFileName(newpath, fl))
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Private Sub sortFolder(fl As String, target As String)
        Dim foldername As String = fl.Substring(fl.LastIndexOf("\") + 1)
        Dim newpath As String = target & "\Folder\"

        If My.Settings.folders.Contains("[R]" & foldername) Or My.Settings.folders.Contains("[C]" & foldername) Then ' Check if folder is whitelisted
            Return
        End If

        newpath = newpath & foldername

        ' Try to move the folder

        Try
            Debug.Print("======================================================================")
            Debug.Print("Folder:" & fl)
            Debug.Print("New Path:" & newpath)
            Directory.Move(fl, checkFolderName(newpath, fl))
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Public Function checkForUpdates() As String
        Dim lver As String = "0.0.0"
        Dim remoteUri As String = My.Settings.cfulink
        Dim fileName As String = Path.Combine(Path.GetTempPath, "ver.txt")
        Debug.Print(fileName)
        Dim myStringWebResource As String = Nothing
        Dim downloadLink As String = My.Settings.dlink

        Try
            If File.Exists(fileName) Then
                Try
                    File.Delete(fileName)
                Catch ex As Exception
                    Debug.Print(ex.ToString)
                End Try
            End If
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try

        Try
            My.Computer.Network.DownloadFile(remoteUri, fileName)

            Using reader As StreamReader = New StreamReader(fileName)
                lver = reader.ReadLine
                My.Settings.dlink = reader.ReadLine
                My.Settings.Save()
                My.Settings.Reload()

                Debug.Print(lver)
                Debug.Print(My.Settings.dlink)

                Console.WriteLine(lver)
            End Using
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try

        Return lver
    End Function

    Public Function checkFileName(targetPath As String, frompath As String) As String
        Dim newpath As String = targetPath

        If Not File.Exists(targetPath) Or targetPath = frompath Then
            Return targetPath
        End If

        Do
            Dim filename As String = Path.GetFileNameWithoutExtension(newpath)
            Dim extension As String = Path.GetExtension(newpath)
            Dim fpath As String = Path.GetDirectoryName(newpath)
            Dim fnum As Integer = 1

            If filename.Last = ")" And filename.Contains("(") Then
                fnum = Convert.ToInt32(filename.Substring(filename.LastIndexOf("(") + 1, filename.LastIndexOf(")") - filename.LastIndexOf("(") - 1)) + 1
                Debug.Print(fnum)
                filename = filename.Substring(0, filename.LastIndexOf("(")) & "(" & fnum & ")"
            Else
                filename = filename & "(" & fnum & ")"
            End If

            newpath = Path.Combine(fpath, filename & extension)
            Debug.Print(newpath)

            If Not File.Exists(newpath) Then
                Exit Do
            End If
        Loop

        Return newpath
    End Function

    Public Function checkFolderName(targetPath As String, frompath As String) As String
        Dim newpath As String = targetPath

        If Not Directory.Exists(targetPath) Or targetPath = frompath Then
            Return targetPath
        End If

        Do
            Dim foldername As String = newpath.Substring(newpath.LastIndexOf("\") + 1)
            Dim fpath As String = Path.GetDirectoryName(newpath)
            Dim fnum As Integer = 1

            If foldername.Last = ")" And foldername.Contains("(") Then
                fnum = Convert.ToInt32(foldername.Substring(foldername.LastIndexOf("(") + 1, foldername.LastIndexOf(")") - foldername.LastIndexOf("(") - 1)) + 1
                Debug.Print(fnum)
                foldername = foldername.Substring(0, foldername.LastIndexOf("(")) & "(" & fnum & ")"
            Else
                foldername = foldername & "(" & fnum & ")"
            End If

            newpath = Path.Combine(fpath, foldername)
            Debug.Print(newpath)

            If Not Directory.Exists(newpath) Then
                Exit Do
            End If
        Loop

        Return newpath
    End Function
End Module
