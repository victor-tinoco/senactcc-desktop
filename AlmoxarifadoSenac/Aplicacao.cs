using AlmoxarifadoSenacLib.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmoxarifadoSenacLib.Repositorios;

namespace AlmoxarifadoSenac
{
    public class Aplicacao
    {
        public static Usuario UsuarioLogado { get; set; }
        
        public static bool TestarConexao()
        {
            string connString = Conexao.ConsultarConexao();

            SqlConnection conexao = new SqlConnection(connString);

            try
            {
                conexao.Open();
                conexao.Close();
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
