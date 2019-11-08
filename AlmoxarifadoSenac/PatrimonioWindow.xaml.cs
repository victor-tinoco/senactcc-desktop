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
    /// Lógica interna para PatrimonioWindow.xaml
    /// </summary>
    public partial class PatrimonioWindow : Window
    {
        public PatrimonioWindow()
        {
            InitializeComponent();
        }

        public Equipamento EquipamentoSelecionado { get; set; }
        public Patrimonio PatrimonioAlteracao { get; set; }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtualizarDG();
        }

        private void OpcaoAlterar_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Patrimonio patrimonio = (Patrimonio)item.SelectedCells[0].Item;

                PatrimonioAlteracao = patrimonio;

                txtSalvar.Text = PatrimonioAlteracao.NumeroPatrimonio.ToString();
            }
        }

        private void OpcaoAtivarDesativar_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Patrimonio patrimonio = (Patrimonio)item.SelectedCells[0].Item;

                PatrimonioAlteracao = patrimonio;

                PatrimonioAlteracao.Ativo = (PatrimonioAlteracao.Ativo == true) ? false : true;

                try
                {
                    PatrimonioRepositorio repos = new PatrimonioRepositorio();
                    repos.AlterarPatrimonio(PatrimonioAlteracao);
                    PatrimonioAlteracao = null;

                    AtualizarDG();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorre um erro. Mensagem original: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ConsultarCamposNulos())
            {
                PatrimonioRepositorio repos = new PatrimonioRepositorio();
                Patrimonio patrimonio = new Patrimonio();
                patrimonio.Equipamento = new Equipamento();

                try
                {
                    patrimonio.NumeroPatrimonio = int.Parse(txtSalvar.Text);
                    patrimonio.Ativo = true;
                    patrimonio.Equipamento = EquipamentoSelecionado;

                    if (PatrimonioAlteracao != null)
                    {
                        patrimonio.Id = PatrimonioAlteracao.Id;
                        repos.AlterarPatrimonio(patrimonio);
                        MessageBox.Show("Patrimônio alterado com sucesso.", "Alteração de Patrimônio", MessageBoxButton.OK);
                        PatrimonioAlteracao = null;
                    }
                    else
                    {
                            repos.InserirPatrimonio(patrimonio);
                            MessageBox.Show("Patrimônio cadastrado com sucesso.", "Cadastro de Patrimônio", MessageBoxButton.OK);
                    }

                    txtSalvar.Text = "";
                    AtualizarDG();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorre um erro. Mensagem original: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtSalvar.Text = "";
                    PatrimonioAlteracao = null;
                    return;
                }

            }
        }

        private bool ConsultarCamposNulos()
        {
            if (string.IsNullOrWhiteSpace(txtSalvar.Text))
            {
                MessageBox.Show("Os seguintes campos abaixo são obrigatórios:" + Environment.NewLine + Environment.NewLine + "Número do Patrimônio*", "Cadastro de Patrimônio", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            } else
            {
                return true;
            }

        }

        private void AtualizarDG()
        {
            PatrimonioRepositorio repo = new PatrimonioRepositorio();
            dgPatrimonios.ItemsSource = repo.PesquisarPatrimonio(txtPesquisar.Text, EquipamentoSelecionado.Id);
            dgPatrimonios.IsReadOnly = true;
            dgPatrimonios.AutoGenerateColumns = false;
            dgPatrimonios.CanUserAddRows = false;
            dgPatrimonios.IsReadOnly = true; // somente leitura (trava a alteração
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            AtualizarDG();
        }
    }
}
