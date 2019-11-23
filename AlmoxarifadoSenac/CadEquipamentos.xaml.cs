using System;
using System.Collections.Generic;
using System.IO;
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
using draw = System.Drawing;
using draw2 = System.Drawing.Imaging;
using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using forms = System.Windows.Forms;

namespace AlmoxarifadoSenac
{
    /// <summary>
    /// Lógica interna para CadEquipamentos.xaml
    /// </summary>
    public partial class CadEquipamentos : Window
    {
        public Equipamento EquipamentoAlteracao { get; set; }

        public CadEquipamentos()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CategoriaRepositorio repos = new CategoriaRepositorio();
            var lista = repos.PesquisarCategoria("");
            cbCategoria.ItemsSource = lista;

            if (EquipamentoAlteracao != null)
            {

                txtTitulo.Text = EquipamentoAlteracao.Nome;
                txtDescricao.Text = EquipamentoAlteracao.Descricao;
                ImagemArray = EquipamentoAlteracao.SrcImagem;
                imgEquip.Source = ByteToImage(EquipamentoAlteracao.SrcImagem);

                CategoriaRepositorio repo = new CategoriaRepositorio();
                Categoria categoriaSelecionada = new Categoria();
                foreach (var categoria in lista)
                {
                    if (EquipamentoAlteracao.NomeCategoria == categoria.Nome)
                    {
                        categoriaSelecionada = categoria;
                        break;
                    }
                }

                cbCategoria.SelectedValue = categoriaSelecionada;
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ConsultarCamposNulos())
            {
                EquipamentoRepositorio repos = new EquipamentoRepositorio();
                Equipamento equip = new Equipamento();
                equip.Categoria = new Categoria();
                Categoria cat = (Categoria)cbCategoria.SelectedValue;

                equip.Nome = txtTitulo.Text;
                equip.Descricao = txtDescricao.Text;
                equip.Categoria.Id = cat.Id;
                equip.Ativo = true;
                equip.SrcImagem = ImagemArray;
                try
                {

                    if (EquipamentoAlteracao != null)
                    {
                        equip.Id = EquipamentoAlteracao.Id;
                        repos.Alterar(equip);
                        MessageBox.Show("Equipamento alterado com sucesso.", "Alteração de Equipamento", MessageBoxButton.OK);
                    }
                    else
                    {
                        repos.InserirEquipamento(equip);
                        MessageBox.Show("Equipamento cadastrado com sucesso.", "Cadastro de Equipamento", MessageBoxButton.OK);
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

        private bool ConsultarCamposNulos()
        {
            string erro = "";

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                erro += "Título*" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                erro += "Descrição*" + Environment.NewLine;
            }
            if (cbCategoria.SelectedItem == null)
            {
                erro += "Categoria*";
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
        public byte[] ImagemArray { get; set; }
        private void imagem_click(object sender, RoutedEventArgs e)
        {
            forms.OpenFileDialog dlg = new forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            Equipamento equip = new Equipamento();

            if (dlg.ShowDialog() == forms.DialogResult.OK)
            {
                var ms = new MemoryStream();
                string nome = dlg.FileName;
                ImagemArray = File.ReadAllBytes(nome);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(nome);
                bitmap.EndInit();

                imgEquip.Source = bitmap;
            }
        }
        public static ImageSource ToImageSource(draw.Image image, draw2.ImageFormat imageFormat)
        {
            BitmapImage bitmap = new BitmapImage();

            using (MemoryStream stream = new MemoryStream())
            {
                // Save to the stream
                image.Save(stream, imageFormat);

                // Rewind the stream
                stream.Seek(0, SeekOrigin.Begin);

                // Tell the WPF BitmapImage to use this stream
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            return bitmap;
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            if (imageData != null)
            {
                MemoryStream ms = new MemoryStream(imageData);
            
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
        }
        ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

    }
}
