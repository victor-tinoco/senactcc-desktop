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
    /// Lógica interna para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void BtnAgendamentos_Click(object sender, RoutedEventArgs e)
        {
            CarteiraWindow carteira = new CarteiraWindow();
            carteira.ShowDialog();
        }

        private void BtnEquipamentos_Click(object sender, RoutedEventArgs e)
        {
            EquipamentosWindow equips = new EquipamentosWindow();
            equips.ShowDialog();
        }

        private void BtnFuncionarios_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosWindow funcionarios = new FuncionariosWindow();
            funcionarios.ShowDialog();
        }

        private void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            CategoriasWindow categorias = new CategoriasWindow();
            categorias.ShowDialog();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            lblUsuario.Content = Aplicacao.UsuarioLogado.Nome;
        }
    }
}
