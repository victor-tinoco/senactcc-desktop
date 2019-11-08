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
    public class PatrimonioRepositorio
    {
        public List<Patrimonio> PesquisarTodosPatrimonio()
        {
            List<Patrimonio> patrimonios;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                   "select patrimonio.id_patrimonio Id, nmr_patrimonio NumeroPatrimonio,equipamento.id_equipamento IdEquipamento, patrimonio.fl_status Ativo " +
                   "from Patrimonio " +
                   "join Equipamento on Patrimonio.id_equipamento = Equipamento.id_equipamento ";

                patrimonios = conn.Query<Patrimonio>(script).ToList();

                foreach (var patrimonio in patrimonios)
                {
                    EquipamentoRepositorio repo = new EquipamentoRepositorio();
                    patrimonio.Equipamento = repo.ConsultarPorId(patrimonio.IdEquipamento);
                }
            }
            return patrimonios;
        }
        public List<Patrimonio> PesquisarPatrimonio(string filtro, int id)
        {
            List<Patrimonio> patrimonios;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                   "select patrimonio.id_patrimonio Id, nmr_patrimonio NumeroPatrimonio,equipamento.id_equipamento IdEquipamento, patrimonio.fl_status Ativo " +
                   "from Patrimonio " +
                   "join Equipamento on Patrimonio.id_equipamento = Equipamento.id_equipamento " +
                   "where equipamento.id_equipamento = @ID " +
                   "AND nmr_patrimonio like '%'+ @FILTRO + '%'";

                patrimonios = conn.Query<Patrimonio>(script, new { @FILTRO = filtro, @ID = id }).ToList();

                foreach (var patrimonio in patrimonios)
                {
                    EquipamentoRepositorio repo = new EquipamentoRepositorio();
                    patrimonio.Equipamento = repo.ConsultarPorId(patrimonio.IdEquipamento);
                }
            }
            return patrimonios;
        }
        public void excluir(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Delete patrimonio WHERE id_patrimonio = @ID";
                conn.Execute(script, new { @ID = id });
            }
        }
        public void InserirPatrimonio(Patrimonio patrimonio)
        {
            foreach (var patr in PesquisarTodosPatrimonio())
            {
                if (patr.NumeroPatrimonio == patrimonio.NumeroPatrimonio)
                {
                    throw new Exception("Já existe cadastrado este número de patrimônio.");
                }
            }

            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Insert into patrimonio (nmr_patrimonio,id_equipamento,fl_status) " +
                    "Values (@NUMERO,@IDEQUIPAMENTO,@ATIVO)";
                coon.Execute(script, new
                {
                    @NUMERO = patrimonio.NumeroPatrimonio,
                    @IDEQUIPAMENTO = patrimonio.Equipamento.Id,
                    @ATIVO = patrimonio.Ativo
                });
            }
        }
        public void AlterarPatrimonio(Patrimonio patrimonio)
        {
            foreach (var patr in PesquisarTodosPatrimonio())
            {
                if (patr.NumeroPatrimonio == patrimonio.NumeroPatrimonio && patr.Id != patrimonio.Id)
                {
                    throw new Exception("Já existe cadastrado este número de patrimônio.");
                }
            }
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "update patrimonio set nmr_patrimonio = @NUMERO, fl_status = @ATIVO where id_patrimonio = @ID";
                coon.Execute(script, new
                {
                    @NUMERO = patrimonio.NumeroPatrimonio,
                    @ATIVO = patrimonio.Ativo,
                    @ID = patrimonio.Id
                });
            }
        }
    }
}
