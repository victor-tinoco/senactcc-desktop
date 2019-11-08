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
    /// Lógica interna para CadFuncionario.xaml
    /// </summary>
    public partial class CadFuncionario : Window
    {
        public Usuario FuncionarioAlteracao { get; set; }
        public CadFuncionario()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ConsultarCamposNulos())
            {
                Usuario user = new Usuario();
                UsuarioRepositorio repos = new UsuarioRepositorio();
                user.Nome = txtNome.Text;
                user.DsUsuario = txtUsuario.Text;
                user.Email = txtEmail.Text;
                user.NumeroRegistro = int.Parse(txtRegistro.Text);

                if (cbTipoUsuario.SelectionBoxItem.ToString() == "Administrador")
                {
                    user.TipoUsuario = 1;
                } else if (cbTipoUsuario.SelectionBoxItem.ToString() == "SubAdministrador")
                {
                    user.TipoUsuario = 2;
                } else if (cbTipoUsuario.SelectionBoxItem.ToString() == "Funcionario")
                {
                    user.TipoUsuario = 3;
                }

                user.Ativo = (tbAtivo.IsChecked == true) ? true : false;

                try
                {

                    if (FuncionarioAlteracao != null)
                    {
                        user.Id = FuncionarioAlteracao.Id;
                        repos.Alterar(user);
                        MessageBox.Show("Usuário alterado com sucesso.", "Alteração de Usuário", MessageBoxButton.OK);
                    }
                    else
                    {
                        repos.InserirUsuario(user);
                        MessageBox.Show("Usuário cadastrado com sucesso.", "Cadastro de Usuário", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorre um erro. Mensagem original: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Close();

            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            

            UsuarioRepositorio repos = new UsuarioRepositorio();

            if (FuncionarioAlteracao != null)
            {
                txtNome.Text = FuncionarioAlteracao.Nome;
                txtUsuario.Text = FuncionarioAlteracao.DsUsuario;
                txtEmail.Text = FuncionarioAlteracao.Email;
                txtRegistro.Text = FuncionarioAlteracao.NumeroRegistro.ToString();

                if (FuncionarioAlteracao.TipoUsuario == 1)
                { 
                    cbTipoUsuario.SelectedItem ="Administrador";
                }
                else if (FuncionarioAlteracao.TipoUsuario == 2)
                {
                    cbTipoUsuario.SelectedItem = "SubAdministrador";
                }
                else if (FuncionarioAlteracao.TipoUsuario == 3)
                {
                    cbTipoUsuario.SelectedItem = "Funcionario";
                }

                if (FuncionarioAlteracao.Ativo)
                    tbAtivo.IsChecked = true;
                else
                    tbAtivo.IsChecked = false;
            }

            List<string> itens = new List<string>();
            itens.Add("Administrador");
            itens.Add("SubAdministrador");
            itens.Add("Funcionario");
            cbTipoUsuario.ItemsSource = itens;
        }

        private bool ConsultarCamposNulos()
        {
            string erro = "";

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                erro += "Nome*" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                erro += "Usuario*" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtRegistro.Text))
            {
                erro += "Número de Registro*" + Environment.NewLine;
            }


            if (erro == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Os seguintes campos abaixo são obrigatórios:" + Environment.NewLine + Environment.NewLine + erro, "Cadastro de Funcionario", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
    }
}
