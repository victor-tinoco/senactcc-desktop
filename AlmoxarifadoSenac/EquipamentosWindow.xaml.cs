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
using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;

namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para EquipamentosWindow.xaml
    /// </summary>
    public partial class EquipamentosWindow : Window
    {

        public EquipamentosWindow()
        {
            InitializeComponent();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            CadEquipamentos cad = new CadEquipamentos();
            cad.ShowDialog();
            AtualizarDG();
        }

        private void AtualizarDG()
        {
            EquipamentoRepositorio repo = new EquipamentoRepositorio();
            dgEquipamentos.ItemsSource = repo.PesquisarEquipamento(txtPesquisar.Text);

            dgEquipamentos.AutoGenerateColumns = false;
            dgEquipamentos.IsReadOnly = true;
            dgEquipamentos.CanUserAddRows = false;
        }

        private void OpcaoPatrimonio_Click(object sender, RoutedEventArgs e)
        {

            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Equipamento equip = (Equipamento)item.SelectedCells[0].Item;

                EquipamentoRepositorio repos = new EquipamentoRepositorio();

                PatrimonioWindow window = new PatrimonioWindow();

                window.EquipamentoSelecionado = equip;

                window.ShowDialog();

                AtualizarDG();
            }
        }

        private void OpcaoAlterar_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;

            DataGrid item = (DataGrid)contextMenu.PlacementTarget;

            if (item.SelectedCells.Count > 0)
            {
                Equipamento equip = (Equipamento)item.SelectedCells[0].Item;

                EquipamentoRepositorio repos = new EquipamentoRepositorio();

                CadEquipamentos window = new CadEquipamentos();

                window.EquipamentoAlteracao = equip;

                window.ShowDialog();

                AtualizarDG();
            }
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
