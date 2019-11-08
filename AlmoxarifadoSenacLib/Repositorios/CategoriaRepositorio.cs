using AlmoxarifadoSenacLib.Modelos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSenacLib.Repositorios
{
    public class CategoriaRepositorio
    {
        public void InserirCategoria(Categoria categoria)
        {
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Insert into Categoria (nm_categoria) " +
                    "Values (@NOME)";
                coon.Execute(script, new
                {
                    @NOME = categoria.Nome
                });
            }
        }
        public Categoria ConsultarPorNome(string nome)
        {
            Categoria categoria;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "select nm_categoria Nome from categoria where nm_categoria = @NOME";
                categoria = conn.QueryFirstOrDefault<Categoria>(
                     script, new { nome });
            }
            return categoria;
        }
        public Categoria ConsultarPorId(int Id)
        {
            Categoria categoria;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "select id_categoria Id, nm_categoria Nome from categoria where id_categoria = @Id";
                categoria = conn.QueryFirstOrDefault<Categoria>(
                     script, new { Id });
            }
            return categoria;
        }
        public List<Categoria> PesquisarCategoria (string filtro)
        {
            List<Categoria> categoria;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Select id_categoria Id,nm_categoria Nome " +
                "from categoria where nm_categoria like '%' + @FILTRO+'%'";
                categoria = conn.Query<Categoria>(script, new { @FILTRO = filtro }).ToList();
            }
            return categoria;
        }

        public void excluir(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Delete categoria WHERE id_Categoria = @ID";
                conn.Execute(script, new { @ID = id });
            }
        }
        public void Alterar(Categoria categoria)
        {
            if (ConsultarPorNome(categoria.Nome) != null)
            {
                throw new Exception("Categoria ja Existe.");
            }
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "update categoria set nm_categoria = @NOME where id_categoria = @ID";
                coon.Execute(script, new
                {
                    @NOME = categoria.Nome,
                    @ID = categoria.Id
                });

            }
        }
    }
}
