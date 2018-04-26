<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Foto
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Foto))
        Me.PictureBox_FotoPreview = New System.Windows.Forms.PictureBox()
        Me.Button_AbrirArquivo = New System.Windows.Forms.Button()
        Me.Button_CopiarImagem = New System.Windows.Forms.Button()
        CType(Me.PictureBox_FotoPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox_FotoPreview
        '
        Me.PictureBox_FotoPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox_FotoPreview.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox_FotoPreview.Name = "PictureBox_FotoPreview"
        Me.PictureBox_FotoPreview.Size = New System.Drawing.Size(179, 149)
        Me.PictureBox_FotoPreview.TabIndex = 0
        Me.PictureBox_FotoPreview.TabStop = False
        '
        'Button_AbrirArquivo
        '
        Me.Button_AbrirArquivo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button_AbrirArquivo.Location = New System.Drawing.Point(0, 0)
        Me.Button_AbrirArquivo.Name = "Button_AbrirArquivo"
        Me.Button_AbrirArquivo.Size = New System.Drawing.Size(84, 23)
        Me.Button_AbrirArquivo.TabIndex = 1
        Me.Button_AbrirArquivo.Text = "Abrir Original"
        Me.Button_AbrirArquivo.UseVisualStyleBackColor = False
        '
        'Button_CopiarImagem
        '
        Me.Button_CopiarImagem.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button_CopiarImagem.Location = New System.Drawing.Point(90, 0)
        Me.Button_CopiarImagem.Name = "Button_CopiarImagem"
        Me.Button_CopiarImagem.Size = New System.Drawing.Size(84, 23)
        Me.Button_CopiarImagem.TabIndex = 2
        Me.Button_CopiarImagem.Text = "Copiar Imagem"
        Me.Button_CopiarImagem.UseVisualStyleBackColor = False
        '
        'Foto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(179, 147)
        Me.Controls.Add(Me.Button_CopiarImagem)
        Me.Controls.Add(Me.Button_AbrirArquivo)
        Me.Controls.Add(Me.PictureBox_FotoPreview)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Foto"
        Me.Text = "Visualização de Foto"
        CType(Me.PictureBox_FotoPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox_FotoPreview As PictureBox
    Friend WithEvents Button_AbrirArquivo As Button
    Friend WithEvents Button_CopiarImagem As Button
End Class
