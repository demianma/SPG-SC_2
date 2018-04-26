Imports System.Drawing.Imaging
Imports <xmlns="http://www.opengis.net/kml/2.2">

Module Auxiliar

    'verifica qual a rotacao da imagem
    Public Function RetornaRotacao(caminho As String)

        Dim imgLocal As String = caminho
        Dim img As Bitmap
        Dim properties As PropertyItem()
        Dim orientation As Short
        Dim orientantionDegrees As Short

        Try
            'verifica a rotacao da imagem
            img = Image.FromFile(imgLocal)
            properties = img.PropertyItems
            For Each prp As PropertyItem In properties
                If prp.Id = 274 Then
                    orientation = BitConverter.ToInt16(prp.Value, 0)
                    Select Case orientation
                        Case 1
                            orientantionDegrees = 0
                        Case 3
                            orientantionDegrees = 180
                        Case 6
                            orientantionDegrees = 90
                        Case 8
                            orientantionDegrees = 270
                    End Select
                End If
            Next
        Catch ex As Exception
        End Try

        Return orientantionDegrees

    End Function

    'coloca uma imagem em um picturebox
    Public Sub ColocarImagem(caminho As String, pb As PictureBox)

        If caminho = "" Or caminho = Nothing Or pb.Name = "" Or pb.Name = Nothing Then
            Exit Sub
        End If

        Dim imgLocal As String = caminho
        Dim img As Bitmap
        Dim properties As PropertyItem()
        Dim orientation As Short
        Dim rft As RotateFlipType = RotateFlipType.RotateNoneFlipNone

        Try
            'verifica a rotacao da imagem
            img = Image.FromFile(imgLocal)
            properties = img.PropertyItems
            For Each prp As PropertyItem In properties
                If prp.Id = 274 Then
                    orientation = BitConverter.ToInt16(prp.Value, 0)
                    Select Case orientation
                        Case 1
                            rft = RotateFlipType.RotateNoneFlipNone
                        Case 3
                            rft = RotateFlipType.Rotate180FlipNone
                        Case 6
                            rft = RotateFlipType.Rotate90FlipNone
                        Case 8
                            rft = RotateFlipType.Rotate270FlipNone
                    End Select
                End If
            Next

            'coloca no picbox
            With pb
                .Load(imgLocal)
                .SizeMode = PictureBoxSizeMode.Zoom
                .Image.RotateFlip(rft)
            End With

        Catch ex As Exception
            pb.Image = Nothing
        End Try

    End Sub

    'exporta dgvs
    Public Sub ExportaCSV(dgv As DataGridView)
        Dim headers = (From header As DataGridViewColumn In dgv.Columns.Cast(Of DataGridViewColumn)()
                       Select header.HeaderText).ToArray
        Dim rows = From row As DataGridViewRow In dgv.Rows.Cast(Of DataGridViewRow)()
                   Where Not row.IsNewRow
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
        Dim arquivo_temp As String = dgv.Name

        Using dialog As New SaveFileDialog
            With dialog
                .Title = "Salvar Arquivo"
                .Filter = "Arquivo de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*"
                .FileName = arquivo_temp
                '.ShowDialog()
            End With
            If dialog.ShowDialog() = DialogResult.OK Then
                Using sw As New IO.StreamWriter(dialog.OpenFile())
                    sw.WriteLine(String.Join(ControlChars.Tab, headers))
                    For Each r In rows
                        sw.WriteLine(String.Join(ControlChars.Tab, r))
                    Next
                    sw.Close()
                End Using
                MsgBox("Arquivo salvo com sucesso!")
            End If
        End Using
    End Sub

    'parsear kml
    Public Sub kmltoSQLite()

        Dim kml As String = "C:\Users\demian\Documents\03 - SPG-SC\Levantamento\Versão 2\basededados\sgpre_teste.kml"
        Dim xdata = XDocument.Load(kml)
        For Each p In xdata.Root.<Document>.<Folder>.<Placemark>
            'Console.WriteLine("Name: " & p.<name>.Value)
            'Console.WriteLine("Coordinates: " & p...<coordinates>.Value)
            'RunSQL("INSERT INTO kml (SGPRE, XYZ) VALUES ('" & p.<name>.Value & "', '" & p...<coordinates>.Value & "')")
            Console.WriteLine("INSERT INTO kml (SGPRE, XYZ) VALUES ('" & p.<name>.Value & "', '" & p...<coordinates>.Value & "')")

            'For Each c In p...<coordinates>
            '    Console.WriteLine("Coordinates: " & c.Value)
            'Next
        Next

        Console.ReadLine()
    End Sub

End Module
