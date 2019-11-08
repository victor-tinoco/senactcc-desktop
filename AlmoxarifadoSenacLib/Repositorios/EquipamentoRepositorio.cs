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
    public class EquipamentoRepositorio
    {
        public void InserirEquipamento(Equipamento equipamento)
        {
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Insert Into Equipamento " +
                    "(nm_equipamento,ds_equipamento,id_categoria,fl_status,img_caminho) " +
                     "Values (@NOME,@DSEQUIPAMENTO,@CATEGORIA,@STATUS,@IMAGEM)";

                coon.Execute(script, new
                {
                    @NOME = equipamento.Nome,
                    @DSEQUIPAMENTO = equipamento.Descricao,
                    @CATEGORIA = equipamento.Categoria.Id,
                    @STATUS = true,
                    @IMAGEM = equipamento.SrcImagem

                });

            }

        }
        public List<Equipamento> PesquisarEquipamento(string filtro)
        {
            List<Equipamento> Equipamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "select " +
                "id_equipamento Id,nm_equipamento Nome,nm_categoria NomeCategoria, ds_equipamento Descricao, img_caminho SrcImagem," +
                "(select count(id_patrimonio) from patrimonio where patrimonio.id_equipamento = equipamento.id_equipamento and fl_status = 1) Quantidade from Equipamento " +
                "inner join " +
                "Categoria on Equipamento.id_categoria = Categoria.id_categoria " +
                "where nm_equipamento like  '%' + @FILTRO + '%' ";

                Equipamento = conn.Query<Equipamento>(script, new { @FILTRO = filtro }).ToList();
            }
            return Equipamento;
        }



        public Equipamento ConsultarPorNome(string nome)
        {
            Equipamento equipamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "SELECT id_equipamento Id," +
                    "nm_equipamento nome,ds_equipamento Descricao," +
                    "id_categoria categoria,fl_status Ativo " +
                    "From Equipamento WHERE nome = @NOME";
                equipamento = conn.QueryFirstOrDefault<Equipamento>(
                     script, new { nome });
            }
            return equipamento;
        }

        public Equipamento ConsultarPorId(int Id)
        {
            Equipamento equipamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "select " +
                "id_equipamento Id,nm_equipamento Nome,nm_categoria NomeCategoria, ds_equipamento Descricao, img_caminho SrcImagem," +
                "(select count(id_patrimonio) from patrimonio where patrimonio.id_equipamento = equipamento.id_equipamento and fl_status = 1) Quantidade from Equipamento " +
                "inner join " +
                "Categoria on Equipamento.id_categoria = Categoria.id_categoria " +
                "where id_equipamento = @Id ";
                equipamento = conn.QueryFirstOrDefault<Equipamento>(
                     script, new { Id });

                Repositorios.CategoriaRepositorio repo = new CategoriaRepositorio();
                equipamento.Categoria = repo.ConsultarPorId(equipamento.IdCategoria);

            }
            return equipamento;
        }
        public void Excluir(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Delete Equipamento WHERE id_equipamento = @ID";
                conn.Execute(script, new { @ID = id });
            }
        }
        public void Alterar(Equipamento equipamento)
        {
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "update Equipamento set " +
                    "nm_equipamento = @NOME,ds_equipamento = @DESCRICAO," +
                    "id_categoria = @CATEGORIA,fl_status = @STATUS, img_caminho = @IMAGEM" +
                    " WHERE id_equipamento = @ID ";
                coon.Execute(script, new
                {
                    @NOME = equipamento.Nome,
                    @DESCRICAO = equipamento.Descricao,
                    @CATEGORIA = equipamento.Categoria.Id,
                    @STATUS = equipamento.Ativo,
                    @ID = equipamento.Id,
                    @IMAGEM = equipamento.SrcImagem
                });

            }
        }
        public List<Equipamento> ListadeEquipamentos(string filtro, string categoria, int iniciopag, int fimpag)
        {
            if(filtro == null)
            {
                filtro = "";
            }
            if(categoria == null)
            {
                categoria = "";
            }
            List<Equipamento> Equipamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "select " +
                "id_equipamento Id,nm_equipamento Nome,nm_categoria NomeCategoria, ds_equipamento Descricao, img_caminho SrcImagem," +
                "(select count(id_patrimonio) from patrimonio where patrimonio.id_equipamento = equipamento.id_equipamento and fl_status = 1) Quantidade from Equipamento " +
                "inner join " +
                "Categoria on Equipamento.id_categoria = Categoria.id_categoria " +
                "where nm_equipamento like  '%' + @FILTRO + '%'" +
                "and nm_categoria like  '%' + @CATEGORIA + '%' " +
                "order by id_equipamento asc " +
                "OFFSET @INICIOPAG rows " +
                "fetch next @FIMPAG rows only ";

                Equipamento = conn.Query<Equipamento>(script, new { @FILTRO = filtro, @CATEGORIA = categoria, @FIMPAG = fimpag, @INICIOPAG = iniciopag }).ToList();
            }
            return Equipamento;
        }
        public int Paginacao()
        {
            int paginacao;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " select count(id_equipamento) from Equipamento ";
                paginacao = conn.QueryFirstOrDefault<int>(script);
            }
            return paginacao;

        }
    }
}
