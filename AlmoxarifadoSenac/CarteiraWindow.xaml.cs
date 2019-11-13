using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using control = System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using Excel = Microsoft.Office.Interop.Excel;
using forms = System.Windows.Forms;


namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para CarteiraWindow.xaml
    /// </summary>
    public partial class CarteiraWindow : Window
    {
        public List<Agendamento> lista = new List<Agendamento>();
        public CarteiraWindow()
        {
            InitializeComponent();
        }
        private void AtualizarDG()
        {
            AgendamentoRepositorio repo = new AgendamentoRepositorio();
            lista = repo.PesquisarAgendamento2(txtPesquisar.Text, dpDataInicio.SelectedDate.Value, dpDataFim.SelectedDate.Value);
            dgCarteira.ItemsSource = lista;
            dgCarteira.IsReadOnly = true;
            dgCarteira.CanUserAddRows = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtualizarDG();
        }

        private void OpcaoAlterar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpcaoExcluir_Click(object sender, RoutedEventArgs e)
        {


        }
        private void VerificarPatrimonio_Click(object sender, RoutedEventArgs e)
        {
            control.MenuItem menuItem = (control.MenuItem)sender;

            control.ContextMenu contextMenu = (control.ContextMenu)menuItem.Parent;

            control.DataGrid item = (control.DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Agendamento equip = (Agendamento)item.SelectedCells[0].Item;

               AgendamentoRepositorio repos = new AgendamentoRepositorio();

                PatrimoniosAgendamento window = new PatrimoniosAgendamento();

                window.AgendamentoSelecionado = equip;

                window.ShowDialog();

                AtualizarDG();
            }

        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dpDataInicio.SelectedDate = DateTime.Now;
            dpDataFim.SelectedDate = DateTime.Now;
            AtualizarDG();
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtPesquisar.Text = "";
            dpDataInicio.SelectedDate = DateTime.Now;
            dpDataFim.SelectedDate = DateTime.Now;
            AtualizarDG();
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {

            forms.SaveFileDialog folderDlg = new forms.SaveFileDialog();
            folderDlg.Filter = "Excel files (*.xls)|*.xls";
            folderDlg.FilterIndex = 2;
            folderDlg.RestoreDirectory = true;

            if (folderDlg.ShowDialog() == forms.DialogResult.OK)
            {

                // Inicia o componente Excel
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //Cria uma planilha temporária na memória do computador
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                int linha = 1;
                xlWorkSheet.Cells[linha, 1] = "Usuario";
                xlWorkSheet.Cells[linha, 2] = "Equipamento";
                xlWorkSheet.Cells[linha, 3] = "Categoria";
                xlWorkSheet.Cells[linha, 4] = "Quantidade";
                xlWorkSheet.Cells[linha, 5] = "Data que Agendou";
                xlWorkSheet.Cells[linha, 6] = "Dia do Agendaemnto";
                xlWorkSheet.Cells[linha, 7] = "Hora de Retirada";
                xlWorkSheet.Cells[linha, 8] = "Hora de Devoluçâo";
                xlWorkSheet.Cells[linha, 9] = "Status de Devolução";
                xlWorkSheet.Cells[linha, 10] = "Data da Ultima Alteração";
                xlWorkSheet.Cells[linha, 11] = "Usuario da Ultima Alteração";
                foreach (var item in lista)
                {
                    linha++;
                    //incluindo dados
                    xlWorkSheet.Cells[linha, 1] = item.Usuario.Nome;
                    xlWorkSheet.Cells[linha, 2] = item.Equipamento.Nome;
                    xlWorkSheet.Cells[linha, 3] = item.Equipamento.NomeCategoria;
                    xlWorkSheet.Cells[linha, 4] = item.Quantidade;
                    xlWorkSheet.Cells[linha, 5] = item.DataAgendamento;
                    xlWorkSheet.Cells[linha, 6] = item.Dia;
                    xlWorkSheet.Cells[linha, 7] = item.DataHoraRetirada.ToString();
                    xlWorkSheet.Cells[linha, 8] = item.DataHoraDevolucao.ToString();
                    xlWorkSheet.Cells[linha, 9] = item.StatusDevolucao;
                    xlWorkSheet.Cells[linha, 10] = item.DataAlteracao;
                    xlWorkSheet.Cells[linha, 11] = (item.UsuarioAlteracao == null) ? "" : item.UsuarioAlteracao.Nome;


                }



                //Salva o arquivo de acordo com a documentação do Excel.
                xlWorkBook.SaveAs(folderDlg.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }

        }
    }
}

