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

namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para ConfigureDominoWindow.xaml
    /// </summary>
    public partial class ConfigureDominoWindow : Window
    {
        public ConfigureDominoWindow()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Dominio"].Value = txtDominio.Text;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("Alterações efetuadas com sucesso.");
            Close();
        }
    }
}
