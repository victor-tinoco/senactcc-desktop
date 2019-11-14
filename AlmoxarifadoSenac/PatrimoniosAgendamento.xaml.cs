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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para PatrimoniosAgendamento.xaml
    /// </summary>
    public partial class PatrimoniosAgendamento : Window
    {
        public Agendamento AgendamentoSelecionado { get; set; }
        public PatrimoniosAgendamento()
        {
            InitializeComponent();
        }
        private void AtualizarDG()
        {
            PatrimonioRepositorio repo = new PatrimonioRepositorio();
            dgPatrimonios.ItemsSource = repo.PesquisarPatrimonioPorAgendamento(txtPesquisar.Text, AgendamentoSelecionado.Id);
            dgPatrimonios.IsReadOnly = true;
            dgPatrimonios.AutoGenerateColumns = false;
            dgPatrimonios.CanUserAddRows = false;
            dgPatrimonios.IsReadOnly = true; // somente leitura (trava a alteração
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
                AtualizarDG();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            AtualizarDG();
        }
    }
}
