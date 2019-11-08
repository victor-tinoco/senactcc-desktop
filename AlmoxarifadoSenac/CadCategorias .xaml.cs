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
    /// Lógica interna para CadEquipamentos.xaml
    /// </summary>
    public partial class CadCategorias : Window
    {
        public Categoria CategoriaAlteracao { get; set; }
        public CadCategorias()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ConsultarCamposNulos())
            {
                CategoriaRepositorio repos = new CategoriaRepositorio();
                Categoria categoria = new Categoria();

                categoria.Nome = txtTitulo.Text;

                try
                {

                    if (CategoriaAlteracao != null)
                    {
                        categoria.Id = CategoriaAlteracao.Id;
                        repos.Alterar(categoria);
                        MessageBox.Show("Categoria alterado com sucesso.", "Alteração de Equipamento", MessageBoxButton.OK);
                    }
                    else
                    {
                        repos.InserirCategoria(categoria);
                        MessageBox.Show("Categoria cadastrado com sucesso.", "Cadastro de Equipamento", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorre um erro. Mensagem original: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }

                Close();

            }
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {



            if (CategoriaAlteracao != null)
            {
                txtTitulo.Text = CategoriaAlteracao.Nome;
            }
        }
        private bool ConsultarCamposNulos()
        {
            string erro = "";

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                erro += "Título*" + Environment.NewLine;
            }
            if (erro == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Os seguintes campos abaixo são obrigatórios:" + Environment.NewLine + Environment.NewLine + erro, "Cadastro de Equipamento", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

    }
}
