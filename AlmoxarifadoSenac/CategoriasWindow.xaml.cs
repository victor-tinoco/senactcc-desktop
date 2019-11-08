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
    /// Lógica interna para CategoriasWindow.xaml
    /// </summary>
    public partial class CategoriasWindow : Window
    {
        public CategoriasWindow()
        {
            InitializeComponent();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            CadCategorias cad = new CadCategorias();
            cad.ShowDialog();
            AtualizarDG();
        }
        private void AtualizarDG()
        {
            CategoriaRepositorio repo = new CategoriaRepositorio();
            dgCategorias.ItemsSource = repo.PesquisarCategoria(txtPesquisar.Text);
            dgCategorias.Columns[0].Visibility = Visibility.Hidden;
            dgCategorias.Columns[1].Width = 771;
            dgCategorias.IsReadOnly = true;
            dgCategorias.CanUserAddRows = false;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            AtualizarDG();
        }

        private void OpcaoExcluir_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Categoria categ = (Categoria)item.SelectedCells[0].Item;
                CategoriaRepositorio repos = new CategoriaRepositorio();
                EquipamentoRepositorio reposEquip = new EquipamentoRepositorio();
                var lista = reposEquip.PesquisarEquipamento("");
                bool erro = false;
                foreach (var equip in lista)
                {
                    if (equip.NomeCategoria == categ.Nome)
                    {
                        erro = true;
                        break;                        
                    }
                }
                if (erro == true) 
                {
                    MessageBox.Show("Existem equipamentos cadastrados com essa categoria, por tal motivo não será possível fazer a exclusão.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBoxResult resposta;
                    resposta = MessageBox.Show("Deseja realmente excluir esta Categoria?",
                        "Excluir", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (resposta == MessageBoxResult.Yes)
                    {
                        repos.excluir(categ.Id);
                        AtualizarDG();
                    }
                    else
                        e.Handled = true;
                }
            }
        }
        private void OpcaoAlterar_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Categoria categ = (Categoria)item.SelectedCells[0].Item;

                CategoriaRepositorio repos = new CategoriaRepositorio();

                CadCategorias window = new CadCategorias();

                window.CategoriaAlteracao = categ;

                window.ShowDialog();

                AtualizarDG();
            }
        }
    }
}
