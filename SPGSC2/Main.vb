
'--------------------------------------------------------------
'COMPILAR SEMPRE EM X86 PRA FUNCIONAR NOS COMPUTADORES ANTIGOS!
'--------------------------------------------------------------

Public Class Main

    Public Property dgv_Trechos_rowid As Integer = 0
    'Private Property img_location As String = "C:\Users\demian\Documents\03 - SPG-SC\Levantamento\Fotos\"
    Private Property img_location As String = "Fotos\"

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'kmltoSQLite()
        Inicializacao()

    End Sub

    Private Sub Inicializacao()

        'monta dgv_Campo
        With dgv_Campo
            .DataSource = SQLite.ReadDataDT(
                "SELECT dados._rowid_ as ID, entorno.ENTORNO AS Entorno, dados.* 
                 FROM dados LEFT JOIN entorno ON dados.URBANIZADO = entorno.TIPO")
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns("SENTIDO").Visible = False
            .Columns("URBANIZADO").Visible = False
            .Columns("DATAEHORA").Visible = False
            .Columns("QUANTIDADE").Visible = False
            .Columns("ID").Width = 30
            .Columns("RODOVIA").HeaderText = "Sigla"
            .Columns("KM").HeaderText = "Km"
            .Columns("FAIXA").HeaderText = "Faixa"
            .Columns("PAVIMENTO").HeaderText = "Pavimento"
            .Columns("FAIXADOMINIO").HeaderText = "Faixa de Domínio"
            .Columns("SINALHORIZONTAL").HeaderText = "Sinalização Horizontal"
            .Columns("SINALVERTICAL").HeaderText = "Sinalização Vertical"
            .Columns("DRENAGEM").HeaderText = "Drenagem"
            .Columns("OBS").HeaderText = "Observações"
            .Columns("DRENAGEM").HeaderText = "Drenagem"
            .Columns("OBRA1TIPO").HeaderText = "O.A. 1 Tipo"
            .Columns("OBRA1NOME").HeaderText = "O.A. 1 Nome"
            .Columns("OBRA1PROBLEMA").HeaderText = "O.A. 1 Defeito"
            .Columns("OBRA1ESTADOGERAL").HeaderText = "O.A. 1 Estado"
            .Columns("OBRA1FOTO").HeaderText = "O.A. 1 Foto"
            .Columns("OBRA2TIPO").HeaderText = "O.A. 2 Tipo"
            .Columns("OBRA2NOME").HeaderText = "O.A. 2 Nome"
            .Columns("OBRA2PROBLEMA").HeaderText = "O.A. 2 Defeito"
            .Columns("OBRA2ESTADOGERAL").HeaderText = "O.A. 2 Estado"
            .Columns("OBRA2FOTO").HeaderText = "O.A. 2 Foto"
            .Columns("OBRA3TIPO").HeaderText = "O.A. 3 Tipo"
            .Columns("OBRA3NOME").HeaderText = "O.A. 3 Nome"
            .Columns("OBRA3PROBLEMA").HeaderText = "O.A. 3 Defeito"
            .Columns("OBRA3ESTADOGERAL").HeaderText = "O.A. 3 Estado"
            .Columns("OBRA3FOTO").HeaderText = "O.A. 3 Foto"
            .Columns("ELEMENTOANALISADO").HeaderText = "Ocorrência"
            .Columns("DEFEITOENCONTRADO").HeaderText = "Defeito Encontrado"
            .Columns("OUTROS").HeaderText = "Outros Detalhes"
            .Columns("DETALHAMENTO").HeaderText = "Detalhes"
            .Columns("FOTO1").HeaderText = "Foto 1"
            .Columns("FOTO2").HeaderText = "Foto 2"
            .Columns("FOTO3").HeaderText = "Foto 3"
            .Columns("SGPRE").HeaderText = "Código PRE"
            .Columns("JURIS").HeaderText = "Jurisprudência"
        End With

        'monta dgv_SGPRE
        With dgv_SGPRE
            .DataSource = SQLite.ReadDataDT("select * from SGPRE")
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns("SIGLA").Visible = False
            .Columns("SGPRE").HeaderText = "Código SGPRE"
            .Columns("KMINICIAL").HeaderText = "Km Inicial"
            .Columns("KMFINAL").HeaderText = "Km Final"
            .Columns("EXT").HeaderText = "Extensão"
            .Columns("INICIO").HeaderText = "Início"
            .Columns("FIM").HeaderText = "Fim"
            .Columns("SITFIS").HeaderText = "Situação Física"
            .Columns("SIGLAP").HeaderText = "Sigla"
            .Columns("JURIS").HeaderText = "Jurisprudência"
            .Columns("OBS").HeaderText = "Observações"
        End With

        'monta dgv_notas
        With dgv_Avaliacao
            .DataSource = SQLite.ReadDataDT("select * from notas")
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns("SGPRE").HeaderText = "Código PRE"
            .Columns("PAVN").HeaderText = "Pavimento"
            .Columns("FXDN").HeaderText = "Faixa de Domínio"
            .Columns("SIHN").HeaderText = "Sinalização Horizontal"
            .Columns("SIVN").HeaderText = "Sinalização Vertical"
            .Columns("DREN").HeaderText = "Drenagem"
            .Columns("OAEN").HeaderText = "Obras de Arte"
            .Columns("NOTA").HeaderText = "Nota Geral"
            .Columns("CONCEITO").HeaderText = "Conceito Geral"
        End With

        'lanca a lista de rodovias
        Dim dt_ListaRodovias As DataTable = SQLite.ReadDataDT("SELECT DISTINCT RODOVIA FROM dados")
        With lbx_ListaRodovias
            .Items.Clear()
            .DisplayMember = "RODOVIA"
            .DataSource = dt_ListaRodovias
        End With
    End Sub

    'preenche dgv_Trechos ao clicar em uma rodovia
    Private Sub lbx_ListaRodovias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
        lbx_ListaRodovias.MouseClick

        lbl_Instrucoes.Visible = False

        limpaTudo()

        Try
            Dim rodoviaSelec As String = lbx_ListaRodovias.SelectedItem.Item("RODOVIA")
            Dim sqline As String = String.Format("
            SELECT dados._rowid_, dados.KM, dados.SGPRE, dados.PAVIMENTO, entorno.ENTORNO, 
                   dados.FAIXA, dados.FAIXADOMINIO, dados.SINALHORIZONTAL, dados.SINALVERTICAL, 
                   dados.DRENAGEM, dados.JURIS,
                   qtdFotoOA.QTD AS OA, 
                   qtdFotoOP.QTD AS OP
            FROM dados
            LEFT JOIN entorno
	            ON dados.URBANIZADO = entorno.TIPO
            LEFT JOIN qtdFotoOA 
	            ON dados._rowid_ = qtdFotoOA._rowid_
            LEFT JOIN qtdFotoOP
	            ON dados._rowid_ = qtdFotoOP._rowid_
            WHERE dados.RODOVIA = '{0}'", rodoviaSelec)

            With dgv_Trechos
                .DataSource = SQLite.ReadDataDT(sqline)
                .Columns("rowid").Visible = False
                .Columns("JURIS").Visible = False
                .RowHeadersVisible = False
                .AllowUserToResizeRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToAddRows = False
                .MultiSelect = False
                .ReadOnly = True
                .ScrollBars = ScrollBars.Vertical
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .BackgroundColor = Color.White
                .GridColor = Color.LightGray
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

                .Columns("KM").Width = 50
                .Columns("SGPRE").Width = 100
                .Columns("PAVIMENTO").Width = 75
                .Columns("FAIXADOMINIO").Width = 75
                .Columns("SINALHORIZONTAL").Width = 75
                .Columns("SINALVERTICAL").Width = 75
                .Columns("DRENAGEM").Width = 75
                .Columns("ENTORNO").Width = 75
                .Columns("FAIXA").Width = 75
                .Columns("OA").Width = 31
                .Columns("OP").Width = 31

                .Columns("KM").HeaderText = "Km"
                .Columns("SGPRE").HeaderText = "Código PRE"
                .Columns("PAVIMENTO").HeaderText = "Pavimento"
                .Columns("FAIXADOMINIO").HeaderText = "Faixa de Domínio"
                .Columns("SINALHORIZONTAL").HeaderText = "Sinalização Horizontal"
                .Columns("SINALVERTICAL").HeaderText = "Sinalização Vertical"
                .Columns("DRENAGEM").HeaderText = "Drenagem"
                .Columns("ENTORNO").HeaderText = "Entorno"
                .Columns("FAIXA").HeaderText = "Faixa"
            End With

            For Each row As DataGridViewRow In dgv_Trechos.Rows
                If row.Cells("JURIS").Value.ToString = "Municipal" Then
                    row.DefaultCellStyle.ForeColor = Color.Red
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            Next

            dgv_Trechos.ClearSelection()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'set dgv_Trechos_rowid - qdo usa o mouse
    Private Sub dgv_Trechos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Trechos.CellClick
        dgv_Trechos_rowid = dgv_Trechos.CurrentRow.Cells(0).Value
        lbl_Sets()
    End Sub

    'set dgv_Trechos_rowid - qdo usa o teclado
    Private Sub dgv_Trechos_KeyUp(sender As Object, e As KeyEventArgs) Handles dgv_Trechos.KeyUp
        dgv_Trechos_rowid = dgv_Trechos.CurrentRow.Cells(0).Value
        lbl_Sets()
    End Sub

    'ajusta labels/imagens do Main e variaveis inicio/fim trecho
    Private Sub lbl_Sets()

        Dim sqline As String = String.Format("SELECT dados.*, sgpre.INICIO, sgpre.FIM, entorno.ENTORNO,
                                                notas.PAVN,  notas.FXDN, notas.SIHN, notas.SIVN,notas.DREN,
                                                notas.OAEN, notas.NOTA, notas.CONCEITO
                                              FROM dados
                                              LEFT JOIN entorno 
                                                ON dados.URBANIZADO = entorno.TIPO 
                                              LEFT JOIN sgpre 
                                                ON dados.SGPRE = sgpre.SGPRE
                                              LEFT JOIN notas
                                                ON dados.SGPRE = notas.SGPRE 
                                              WHERE dados._rowid_ = '{0}'", dgv_Trechos_rowid)

        Dim dt As DataTable = SQLite.ReadDataDT(sqline)

        lbl_Descricao.Text = dt.Rows(0)("INICIO").ToString & " - " & dt.Rows(0)("FIM").ToString
        lbl_Juris.Text = dt.Rows(0)("JURIS").ToString

        'grupo detalhes do km
        lbl_SGPRESelec.Text = dt.Rows(0)("SGPRE").ToString
        lbl_Faixa.Text = dt.Rows(0)("FAIXA").ToString
        lbl_Pavimento.Text = dt.Rows(0)("PAVIMENTO").ToString
        lbl_FxDominio.Text = dt.Rows(0)("FAIXADOMINIO").ToString
        lbl_SinalHorizontal.Text = dt.Rows(0)("SINALHORIZONTAL").ToString
        lbl_SinalVertical.Text = dt.Rows(0)("SINALVERTICAL").ToString
        lbl_Drenagem.Text = dt.Rows(0)("DRENAGEM").ToString
        lbl_Entorno.Text = dt.Rows(0)("ENTORNO").ToString
        lbl_Obs.Text = dt.Rows(0)("OBS").ToString
        lbl_Registro.Text = dgv_Trechos_rowid

        'grupo ocorrencias pontuais
        lbl_OPElemento.Text = dt.Rows(0)("ELEMENTOANALISADO").ToString
        lbl_OPDefeito.Text = dt.Rows(0)("DEFEITOENCONTRADO").ToString
        lbl_OPDetalhes.Text = dt.Rows(0)("DETALHAMENTO").ToString
        lbl_OPOutros.Text = dt.Rows(0)("OUTROS").ToString

        'grupo obras de arte
        lbl_OA1Tipo.Text = dt.Rows(0)("OBRA1TIPO").ToString
        lbl_OA1Nome.Text = dt.Rows(0)("OBRA1NOME").ToString
        lbl_OA1Estado.Text = dt.Rows(0)("OBRA1ESTADOGERAL").ToString
        lbl_OA1Problema.Text = dt.Rows(0)("OBRA1PROBLEMA").ToString
        lbl_OA2Tipo.Text = dt.Rows(0)("OBRA2TIPO").ToString
        lbl_OA2Nome.Text = dt.Rows(0)("OBRA2NOME").ToString
        lbl_OA2Estado.Text = dt.Rows(0)("OBRA2ESTADOGERAL").ToString
        lbl_OA2Problema.Text = dt.Rows(0)("OBRA2PROBLEMA").ToString
        lbl_OA3Tipo.Text = dt.Rows(0)("OBRA3TIPO").ToString
        lbl_OA3Nome.Text = dt.Rows(0)("OBRA3NOME").ToString
        lbl_OA3Estado.Text = dt.Rows(0)("OBRA3ESTADOGERAL").ToString
        lbl_OA3Problema.Text = dt.Rows(0)("OBRA3PROBLEMA").ToString

        'grupo notas
        lbl_NotaConceito.Text = dt.Rows(0)("CONCEITO").ToString
        lbl_NotaTrecho.Text = dt.Rows(0)("NOTA").ToString
        lbl_NotaPavimento.Text = dt.Rows(0)("PAVN").ToString
        lbl_NotaFaixa.Text = dt.Rows(0)("FXDN").ToString
        lbl_NotaSinHoriz.Text = dt.Rows(0)("SIHN").ToString
        lbl_NotaSinVert.Text = dt.Rows(0)("SIVN").ToString
        lbl_NotaOA.Text = dt.Rows(0)("OAEN").ToString
        lbl_NotaDrenagem.Text = dt.Rows(0)("DREN").ToString

        'coloca um traço nas labels vazias
        tracaLabels()

        'remove imgs velhas
        Dim pbx() As PictureBox
        pbx = New PictureBox() {pbx_OA1, pbx_OA2, pbx_OA3, pbx_OP1, pbx_OP2, pbx_OP3}

        For Each p As PictureBox In pbx
            p.Image = Nothing
        Next

        'coloca imagens novas
        ColocarImagem(img_location & dt.Rows(0)("OBRA1FOTO").ToString, pbx_OA1)
        ColocarImagem(img_location & dt.Rows(0)("OBRA2FOTO").ToString, pbx_OA2)
        ColocarImagem(img_location & dt.Rows(0)("OBRA3FOTO").ToString, pbx_OA3)
        ColocarImagem(img_location & dt.Rows(0)("FOTO1").ToString, pbx_OP1)
        ColocarImagem(img_location & dt.Rows(0)("FOTO2").ToString, pbx_OP2)
        ColocarImagem(img_location & dt.Rows(0)("FOTO3").ToString, pbx_OP3)

        'cria os handlers das imagens 
        For Each p As PictureBox In pbx
            RemoveHandler p.Click, AddressOf img_Handlers
            If p.Image IsNot Nothing Then
                AddHandler p.Click, AddressOf img_Handlers
            End If
        Next

    End Sub

    'preenche label de campo vazio com "-" e limpa as imagens
    Private Sub tracaLabels()
        Dim labels() As Label = {lbl_Registro, lbl_SGPRESelec, lbl_Faixa, lbl_Pavimento,
                                     lbl_FxDominio, lbl_SinalHorizontal, lbl_SinalVertical, lbl_Drenagem, lbl_Entorno,
                                     lbl_Obs, lbl_OPDefeito, lbl_OPDetalhes, lbl_OPElemento, lbl_OPOutros, lbl_OA1Tipo,
                                     lbl_OA1Nome, lbl_OA1Estado, lbl_OA1Problema, lbl_OA2Tipo, lbl_OA2Nome, lbl_OA2Estado,
                                     lbl_OA2Problema, lbl_OA3Tipo, lbl_OA3Nome, lbl_OA3Estado, lbl_OA3Problema,
                                     lbl_NotaConceito, lbl_NotaDrenagem, lbl_NotaFaixa, lbl_NotaOA, lbl_NotaPavimento,
                                     lbl_NotaSinHoriz, lbl_NotaSinVert, lbl_NotaTrecho}
        Try
            For Each lbl As Label In labels
                If lbl.Text.Length < 1 Or lbl.Text = Nothing Then
                    lbl.Text = "-"
                End If
            Next

            If lbl_NotaOA.Text = "0,00" Then
                lbl_NotaOA.Text = "- "
            End If

        Catch e As Exception
            Console.WriteLine("ERRO tracaLabels(): " & e.Message)
        End Try
    End Sub

    'limpa todos os labels com "-", fotos e tira handlers
    Private Sub limpaTudo()
        Dim labels() As Label = {lbl_Registro, lbl_SGPRESelec, lbl_Faixa, lbl_Pavimento,
                                     lbl_FxDominio, lbl_SinalHorizontal, lbl_SinalVertical, lbl_Drenagem, lbl_Entorno,
                                     lbl_Obs, lbl_OPDefeito, lbl_OPDetalhes, lbl_OPElemento, lbl_OPOutros, lbl_OA1Tipo,
                                     lbl_OA1Nome, lbl_OA1Estado, lbl_OA1Problema, lbl_OA2Tipo, lbl_OA2Nome, lbl_OA2Estado,
                                     lbl_OA2Problema, lbl_OA3Tipo, lbl_OA3Nome, lbl_OA3Estado, lbl_OA3Problema,
                                     lbl_NotaConceito, lbl_NotaDrenagem, lbl_NotaFaixa, lbl_NotaOA, lbl_NotaPavimento,
                                     lbl_NotaSinHoriz, lbl_NotaSinVert, lbl_NotaTrecho}
        Dim pbx() As PictureBox
        pbx = New PictureBox() {pbx_OA1, pbx_OA2, pbx_OA3, pbx_OP1, pbx_OP2, pbx_OP3}
        Try
            For Each lbl As Label In labels
                lbl.Text = "-"
            Next

            lbl_Descricao.Text = Nothing
            lbl_Juris.Text = Nothing

            For Each p As PictureBox In pbx
                p.Image = Nothing
                RemoveHandler p.Click, AddressOf img_Handlers
            Next
        Catch e As Exception
            Console.WriteLine("ERRO limpaTudo(): " & e.Message)
        End Try

    End Sub

    'handler fotos
    Private Sub img_Handlers(sender As Object, e As EventArgs)
        Dim pic As PictureBox = DirectCast(sender, PictureBox)
        Try
            If pic IsNot Nothing Then
                Dim openFoto As New Foto(pic.ImageLocation.ToString)
                openFoto.Show()
                openFoto = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine("pic_Click() erro: " & ex.Message)
        End Try
    End Sub

    'botao Abrir
    Private Sub ButtonAbrirRegistro_Click(sender As Object, e As EventArgs) Handles ButtonAbrirRegistro.Click

        TabControl.SelectedTab = TabPage_DadosBrutos
        dgv_Campo.ClearSelection()
        dgv_Campo.FirstDisplayedScrollingRowIndex = dgv_Trechos_rowid - 1
        dgv_Campo.Rows(dgv_Trechos_rowid - 1).Selected = True

    End Sub

    'autoajuste das janelas
    Public Sub AjustaJanela()
        TabControl.Width = Width - 36
        TabControl.Height = Height - 60

        dgv_Campo.Width = TabControl.Width - 24
        dgv_SGPRE.Width = TabControl.Width - 24
        dgv_Avaliacao.Width = TabControl.Width - 24

        dgv_Campo.Height = TabControl.Height - 72
        dgv_SGPRE.Height = TabControl.Height - 72
        dgv_Avaliacao.Height = TabControl.Height - 72

        btn_ExportaAvaliacao.Top = dgv_Avaliacao.Top + dgv_Avaliacao.Height + 10
        btn_ExportaDados.Top = dgv_Campo.Top + dgv_Campo.Height + 10
        btn_ExportaSGPRE.Top = dgv_SGPRE.Top + dgv_SGPRE.Height + 10

        btn_ExportaAvaliacao.Left = dgv_Avaliacao.Left
        btn_ExportaDados.Left = dgv_Campo.Left
        btn_ExportaSGPRE.Left = dgv_SGPRE.Left

    End Sub

    'detecta ajuste das janelas
    Private Sub Principal_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        AjustaJanela()
    End Sub

    'botao exporta avaliacao
    Private Sub btn_ExportaAvaliacao_Click(sender As Object, e As EventArgs) Handles btn_ExportaAvaliacao.Click
        ExportaCSV(dgv_Avaliacao)
    End Sub

    'botao exporta campo
    Private Sub btn_ExportaDados_Click(sender As Object, e As EventArgs) Handles btn_ExportaDados.Click
        ExportaCSV(dgv_Campo)
    End Sub

    'botao exporta sgpre
    Private Sub btn_ExportaSGPRE_Click(sender As Object, e As EventArgs) Handles btn_ExportaSGPRE.Click
        ExportaCSV(dgv_SGPRE)
    End Sub


    'teste
    'Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
    '    Dim myGraphics As Graphics = PictureBox5.CreateGraphics
    '    Dim myPen As Pen = New Pen(Brushes.Red, 20)
    '    myPen.DashStyle = Drawing2D.DashStyle.Solid
    '    myGraphics.DrawLine(myPen, 36, 60, TabControl.Width - 36, TabControl.Height - 60)
    'End Sub
End Class