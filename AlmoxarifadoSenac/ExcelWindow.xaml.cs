using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;


namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para ExcelWindow.xaml
    /// </summary>
    public partial class ExcelWindow : Window
    {
        public List<Agendamento> lista = new List<Agendamento>();

        public ExcelWindow()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {


            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
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
                    xlWorkSheet.Cells[linha, 3] = item.Equipamento.Categoria.Nome;
                    xlWorkSheet.Cells[linha, 4] = item.Quantidade;
                    xlWorkSheet.Cells[linha, 5] = item.DataAgendamento;
                    xlWorkSheet.Cells[linha, 6] = item.Dia;
                    xlWorkSheet.Cells[linha, 7] = item.DataHoraRetirada.ToString();
                    xlWorkSheet.Cells[linha, 8] = item.DataHoraDevolucao.ToString();
                    xlWorkSheet.Cells[linha, 9] = item.StatusDevolucao;
                    xlWorkSheet.Cells[linha, 10] = item.DataAlteracao;
                    xlWorkSheet.Cells[linha, 11] = item.UsuarioAlteracao.Nome;


                }



                //Salva o arquivo de acordo com a documentação do Excel.
                xlWorkBook.SaveAs(folderDlg.SelectedPath +"/"+ txtNomeExcel.Text + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                //o arquivo foi salvo na pasta Meus Documentos.
                System.Windows.MessageBox.Show("Concluído. Verifique em " + folderDlg.SelectedPath + "\\" + txtNomeExcel.Text);
            }
        }
    }
}


