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
    /// Lógica interna para FuncionariosWindow.xaml
    /// </summary>
    public partial class FuncionariosWindow : Window
    {
        public FuncionariosWindow()
        {
            InitializeComponent();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            CadFuncionario cad = new CadFuncionario();
            cad.ShowDialog();
            AtualizarDG();
        }
        private void AtualizarDG()
        {
            UsuarioRepositorio repo = new UsuarioRepositorio();
            dgFuncionarios.ItemsSource = repo.PesquisarUsuario(txtPesquisar.Text);

            dgFuncionarios.AutoGenerateColumns = false;
            dgFuncionarios.IsReadOnly = true;
            dgFuncionarios.CanUserAddRows = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AtualizarDG();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
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
                Usuario user = (Usuario)item.SelectedCells[0].Item;

                UsuarioRepositorio repos = new UsuarioRepositorio();

                CadFuncionario window = new CadFuncionario();

                window.FuncionarioAlteracao = user;

                window.ShowDialog();

                AtualizarDG();
            }
        }
        private void OpcaoExcluir_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Usuario user = (Usuario)item.SelectedCells[0].Item;
                UsuarioRepositorio repos = new UsuarioRepositorio();
                AgendamentoRepositorio reposAgend = new AgendamentoRepositorio();
                var lista = reposAgend.PesquisarTodosAgendamento("");
                bool erro = false;
                foreach (var agend in lista)
                {
                    agend.Usuario = new Usuario();
                    agend.UsuarioAlteracao = new Usuario();

                    if (agend.Usuario.Id == user.Id || agend.UsuarioAlteracao.Id == user.Id)
                    {
                        erro = true;
                        break;
                    }

                }
                if (erro == true)
                {
                    MessageBox.Show("Este usuario ja possui um agendamento, por tal motivo não será possível fazer a exclusão.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    MessageBoxResult resposta;
                    resposta = MessageBox.Show("Deseja realmente excluir este Usuario?",
                        "Excluir", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (resposta == MessageBoxResult.Yes)
                    {
                        repos.excluir(user.Id);
                        AtualizarDG();
                    }
                    else
                        e.Handled = true;
                }

            }
        }
    }
}
