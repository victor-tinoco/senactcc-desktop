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
using System.Configuration;
using static AlmoxarifadoSenacLib.Repositorios.AutenticacaoAD;


namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioRepositorio repo = new UsuarioRepositorio();
            Usuario usuario;
            usuario = repo.ConsultarPorEmail(txtLogin.Text);

            Domain_Authentication domain = new Domain_Authentication(txtLogin.Text, txtSenha.Password, System.Configuration.ConfigurationManager.AppSettings["Dominio"].ToString());
            if(domain.IsValid()== true)
            {
               
            if (usuario != null && domain.IsValid())
            {
           
                if (usuario.TipoUsuario == 3)
                {
                    MessageBox.Show("Você não tem permissões de Administrador/Subadministrador.");
                }
                else
                {
                    Aplicacao.UsuarioLogado = usuario;

                    MenuWindow janela = new MenuWindow();
                    janela.Show();

                    Close();
                }
                }
               
            

            else
            {
                MessageBox.Show("Usuário ou Senha inválido.");
            }
        }
            else
            {
                MessageBox.Show("Não foi possível conectar ao Dominio. " +
                   "Verifique se as configurações estão corretas. ",
                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                MessageBoxResult resultado = MessageBox.Show("Deseja configurar o Dominio agora?",
                    "Configuração", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    ConfigureDominoWindow janela = new ConfigureDominoWindow();
                    janela.ShowDialog();

                    if (domain.IsValid() == false)
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
    }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
           
            if (Aplicacao.TestarConexao() == false)
            {
                MessageBox.Show("Não foi possível conectar ao banco de dados. " +
                    "Verifique se as configurações estão corretas. ",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                MessageBoxResult resultado = MessageBox.Show("Deseja configurar o banco de dados agora?",
                    "Configuração", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    ConfigWindow janela = new ConfigWindow();
                    janela.ShowDialog();

                    if (Aplicacao.TestarConexao() == false)
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
            
        }
    }
}
