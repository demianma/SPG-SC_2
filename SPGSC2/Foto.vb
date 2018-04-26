Public Class Foto

    Public pic
    Public caminho As String

    'abre uma nova janela com a imagem dentro e lança botões de
    'abrir e copiar a imagem

    Public Sub New(imagem As String)

        ' This call is required by the designer.
        InitializeComponent()

        Try
            'definir tamanho da janela
            caminho = imagem
            Top = 10
            Button_AbrirArquivo.Left = 10
            Button_AbrirArquivo.Top = 10
            Button_CopiarImagem.Left = Button_AbrirArquivo.Width + 10
            Button_CopiarImagem.Top = 10

            Dim rotacao = RetornaRotacao(imagem)
            Select Case rotacao
                Case 0
                    PictureBox_FotoPreview.Height = Screen.PrimaryScreen.Bounds.Height * 0.7
                    PictureBox_FotoPreview.Width = PictureBox_FotoPreview.Height * 1.33
                Case 180
                    PictureBox_FotoPreview.Height = Screen.PrimaryScreen.Bounds.Height * 0.7
                    PictureBox_FotoPreview.Width = PictureBox_FotoPreview.Height * 1.33
                Case 90
                    PictureBox_FotoPreview.Height = Screen.PrimaryScreen.Bounds.Height * 0.7
                    PictureBox_FotoPreview.Width = PictureBox_FotoPreview.Height * 1 / 1.33
                Case 270
                    PictureBox_FotoPreview.Height = Screen.PrimaryScreen.Bounds.Height * 0.7
                    PictureBox_FotoPreview.Width = PictureBox_FotoPreview.Height * 1 / 1.33
            End Select

            'lancar imagem no picbox
            ColocarImagem(imagem, PictureBox_FotoPreview)

        Catch ex As Exception
            MsgBox("Ocorreu um erro ao abrir a imagem.")
            Exit Sub
        End Try
    End Sub

    Private Sub Button_AbrirArquivo_Click(sender As Object, e As EventArgs) Handles Button_AbrirArquivo.Click

        Process.Start(caminho)

    End Sub

    Private Sub Button_CopiarImagem_Click(sender As Object, e As EventArgs) Handles Button_CopiarImagem.Click

        Clipboard.SetImage(PictureBox_FotoPreview.Image)
        Button_CopiarImagem.Text = "Copiado!"

    End Sub
End Class