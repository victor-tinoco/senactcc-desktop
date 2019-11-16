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
    public class AgendamentoRepositorio
    {
        public List<Agendamento> PesquisarTodosAgendamento(string filtro)
        {
            List<Agendamento> agendamentos;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =

            "select  " +
            "id_usuario IdUsuario," +
            "Equipamento.id_equipamento IdEquipamento," +
            "dt_agendamento DataAgendamento," +
            "dthr_dia Dia," +
            "dthr_retirada DataHoraRetirada," +
            "dthr_devolucao  DataHoraDevolucao," +
            "ds_devolucao StatusDevolucao," +
            "id_usuario_alteracao IdUsuarioAlteracao, " +
            "count(nmr_patrimonio) Quantidade " +
            "from Equipamento " +
            "inner join " +
            "Patrimonio on Equipamento.id_equipamento = Patrimonio.id_equipamento " +
            "inner join " +
            "PatrimonioAgendado on Patrimonio.id_patrimonio = PatrimonioAgendado.id_patrimonio " +
            "left join " +
            "Agendamento on PatrimonioAgendado.id_agendamento = Agendamento.id_agendamento " +
            "left join " +
            "StatusDevolucao on  Agendamento.id_status_devolucao = StatusDevolucao.id_status_devolucao " +
            "where " +
            "(nm_equipamento like '%' + @FILTRO + '%' " +
            "OR  nmr_patrimonio like '%' + @FILTRO + '%') " +
            "group by " +
            "nm_equipamento," +
            "ds_equipamento," +
            "Equipamento.fl_status," +
            "dthr_dia," +
            "dthr_devolucao," +
            "ds_devolucao," +
            "dthr_retirada," +
            "id_usuario," +
            "dt_agendamento," +
            "Equipamento.id_equipamento," +
            "id_usuario_alteracao";

                agendamentos = conn.Query<Agendamento>(script, new { @FILTRO = filtro }).ToList();

                foreach (var agendamento in agendamentos)
                {
                    EquipamentoRepositorio repoEquip = new EquipamentoRepositorio();
                    agendamento.Equipamento = repoEquip.ConsultarPorId(agendamento.IdEquipamento);

                    UsuarioRepositorio repoUsuario = new UsuarioRepositorio();
                    agendamento.UsuarioAlteracao = repoUsuario.ConsultarPorID(agendamento.IdUsuarioAlteracao);
                    agendamento.Usuario = repoUsuario.ConsultarPorID(agendamento.IdUsuario);
                }

            }
            return agendamentos;
        }
       
        public List<Agendamento> PesquisarAgendamento2(string filtro, DateTime filtroinicio, DateTime filtrofim)
        {
            List<Agendamento> agendamentos;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =

            "select  " +
            "agendamento.id_agendamento Id," +
            "id_usuario IdUsuario," +
            "Equipamento.id_equipamento IdEquipamento," +
            "dt_agendamento DataAgendamento," +
            "dthr_dia Dia," +
            "dt_alteracao  DataAlteracao," +
            "dthr_retirada DataHoraRetirada," +
            "dthr_devolucao  DataHoraDevolucao," +
            "ds_devolucao StatusDevolucao," +
            "id_usuario_alteracao IdUsuarioAlteracao," +
            "count(nmr_patrimonio)  Quantidade " +
            "from Equipamento " +
            "inner join " +
            "Patrimonio on Equipamento.id_equipamento = Patrimonio.id_equipamento " +
            "inner join " +
            "PatrimonioAgendado on Patrimonio.id_patrimonio = PatrimonioAgendado.id_patrimonio " +
            "left join " +
            "Agendamento on PatrimonioAgendado.id_agendamento = Agendamento.id_agendamento " +
            "left join " +
            "StatusDevolucao on  Agendamento.id_status_devolucao = StatusDevolucao.id_status_devolucao " +
            "where " +
            "(nm_equipamento like '%' + @FILTRO + '%' " +
            "OR  nmr_patrimonio like '%' + @FILTRO + '%') " +
            "AND dthr_dia BETWEEN  @FILTROINICIO   AND   @FILTROFIM  " +
            "group by " +
            "nm_equipamento," +
            "ds_equipamento," +
            "Equipamento.fl_status," +
            "dthr_dia," +
            "dthr_devolucao," +
            "ds_devolucao," +
            "dthr_retirada," +
            "id_usuario," +
            "dt_agendamento," +
            "Equipamento.id_equipamento," +
            "id_Usuario_alteracao," +
            "dt_alteracao," +
            "agendamento.id_agendamento ";

                agendamentos = conn.Query<Agendamento>(script, new { @FILTRO = filtro, @FILTROINICIO = filtroinicio.Date, @FILTROFIM = filtrofim.Date.AddDays(1).AddSeconds(-1) }).ToList();

                foreach (var agendamento in agendamentos)
                {
                    EquipamentoRepositorio repoEquip = new EquipamentoRepositorio();
                    agendamento.Equipamento = repoEquip.ConsultarPorId(agendamento.IdEquipamento);

                    UsuarioRepositorio repoUsuario = new UsuarioRepositorio();
                    agendamento.Usuario = repoUsuario.ConsultarPorID(agendamento.IdUsuario);
                    agendamento.UsuarioAlteracao = repoUsuario.ConsultarPorID(agendamento.IdUsuarioAlteracao);
                }
            }

            return agendamentos;
        }

        public void Excluir(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Delete Agendamento WHERE id_agendamento = @ID";
                conn.Execute(script, new { @ID = id });
            }
        }
        public void Alterar(int idDevolucao,int IDusuarioAlteracao,int idAgendamento)
        {
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "update Agendamento set " +
                    "id_status_devolucao = @DEVOLUCAO,dt_alteracao = @ALTERACAO," +
                    "id_usuario_alteracao = @USUARIOALTERACAO " +
                    " WHERE id_agendamento = @ID ";
                coon.Execute(script, new
                {
                    @DEVOLUCAO = idDevolucao,
                    @ALTERACAO = DateTime.Now,
                    @USUARIOALTERACAO = IDusuarioAlteracao,
                    @ID = idAgendamento
                });

            }
        }

        public List<Agendamento> ConsultarAgendamento(DateTime dia, TimeSpan horaretirada,TimeSpan horadevolucao, int idEquip)
        {
           List <Agendamento> agendamento;

            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
               "SELECT " +
               "Equipamento.id_equipamento IdEquipamento, " +
               "dthr_retirada DataHoraRetirada, " +
              "dthr_devolucao DataHoraDevolucao , " +
              " dthr_dia Dia, " +
              "((select count(id_patrimonio) from Patrimonio join equipamento on patrimonio.id_equipamento = equipamento.id_equipamento where equipamento.id_equipamento = @IDEQUIPE and patrimonio.fl_status = 1) - " +
              "(select count(PatrimonioAgendado.id_agendamento) from PatrimonioAgendado join Agendamento on PatrimonioAgendado.id_agendamento = Agendamento.id_agendamento where dthr_dia = @DIA and ((dthr_retirada between @HORARETIRADA and @HORADEVOLUCAO) or (dthr_devolucao between @HORARETIRADA and @HORADEVOLUCAO)))) Quantidade " +
              "FROM Agendamento " +
              "left join " +
              "PatrimonioAgendado on Agendamento.id_agendamento = PatrimonioAgendado.id_agendamento " +
              "left join " +
              "Patrimonio on PatrimonioAgendado.id_patrimonio = Patrimonio.id_patrimonio " +
              "left join " +
              "Equipamento on Patrimonio.id_equipamento = Equipamento.id_equipamento " +
              "WHERE Equipamento.id_equipamento = @IDEQUIPE " +
              "and(dthr_dia = @DIA) " +
              "and((dthr_retirada between @HORARETIRADA and @HORADEVOLUCAO) " +
              "or(dthr_devolucao between @HORARETIRADA and @HORADEVOLUCAO))" +
              "GROUP BY " +
              "Equipamento.id_equipamento, " +
              "dthr_retirada, " +
              "dthr_devolucao, " +
              "dthr_dia ";

                agendamento = coon.Query<Agendamento>(
                     script, new { @IDEQUIPE = idEquip, @DIA = dia, @HORARETIRADA = horaretirada,@HORADEVOLUCAO = horadevolucao }).ToList();
            }
                return agendamento;
        }
        public void InserirAgendamento(Agendamento agendamento)
        {
            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {

                string script = "insert into Agendamento(id_usuario, dt_agendamento, dthr_dia, dthr_devolucao, dthr_retirada, id_status_devolucao, fl_status) " +
                    "values(@IDUSER, @DATAAGORA, @dia,@HORADEVOLUCAO,@HORARETIRADA, 2, 1) " +
                    "insert into PatrimonioAgendado(id_patrimonio, id_agendamento) " +
                    "select top(@quantidade) id_patrimonio, SCOPE_IDENTITY() " +
                    "from Patrimonio p " +
                    "    where id_equipamento =  @IDEQUIPE " +
                    "and p.id_patrimonio not in( " +
                    "select patrimonioAgendado.id_patrimonio " +
                    "from PatrimonioAgendado " +
                    "join Patrimonio on patrimonio.id_patrimonio = patrimonioAgendado.id_patrimonio " +
                    "join Equipamento on Patrimonio.id_equipamento = Equipamento.id_equipamento " +
                    "join Agendamento on PatrimonioAgendado.id_agendamento = Agendamento.id_agendamento " +
                    "where equipamento.id_equipamento =  @IDEQUIPE " +
                    "and dthr_dia = @dia " +
                    "and dthr_retirada = @retirada " +
                    "and dthr_devolucao = @devolucao) ";
                coon.Execute(script, new
                {
                    @IDUSER = agendamento.IdUsuario,
                    @DATAAGORA = agendamento.DataAgendamento,
                    @DIADOAGENDAMENTO = agendamento.Dia,
                    @HORADEVOLUCAO = agendamento.DataHoraDevolucao,
                    @HORARETIRADA = agendamento.DataHoraRetirada,
                    @IDEQUIPE = agendamento.IdEquipamento,
                    @quantidade = agendamento.Quantidade,
                    @dia = agendamento.Dia,
                    @retirada = agendamento.DataHoraRetirada,
                    @devolucao = agendamento.DataHoraDevolucao
                });


            }
        }
        public List<Agendamento> PesquisarAgendamentoPorIDUser(int ID)
        {
            List<Agendamento> agendamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =

            "select  " +
            "Usuario.id_usuario IdUsuario," +
            "nm_usuario NomeUsuario," +
            "nm_equipamento NomeEquipamento," +
            "nm_categoria Categoria," +
            "dthr_dia Dia," +
            "dthr_retirada DataHoraRetirada," +
            "dthr_devolucao  DataHoraDevolucao," +
            "ds_devolucao StatusDevolucao," +
            "count(nmr_patrimonio)  Quantidade " +
            "from Equipamento " +
            "inner join " +
            "Patrimonio on Equipamento.id_equipamento = Patrimonio.id_equipamento " +
            "inner join " +
            "Categoria on Equipamento.id_equipamento = Categoria.id_categoria " +
            "left join " +
            "PatrimonioAgendado on Patrimonio.id_patrimonio = PatrimonioAgendado.id_patrimonio " +
            "left join " +
            "Agendamento on PatrimonioAgendado.id_agendamento = Agendamento.id_agendamento " +
            "left join " +
            "StatusDevolucao on  Agendamento.id_status_devolucao = StatusDevolucao.id_status_devolucao " +
            "left join " +
            "Usuario on Agendamento.id_usuario = Usuario.id_usuario " +
            "where " +
            "Usuario.id_usuario = @IDUSER "+
            "group by " +
            "Usuario.id_usuario," +
            "nm_equipamento," +
            "ds_equipamento," +
            "nm_categoria," +
            "Equipamento.fl_status," +
            "dthr_dia," +
            "dthr_devolucao," +
            "ds_devolucao," +
            "dthr_retirada," +
            "nm_usuario";

                agendamento = conn.Query<Agendamento>(script, new { @IDUSER = ID}).ToList();
            }
            return agendamento;
        }
        public int ConsultarPorId(int Id)
        {
            int equipamento;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                "select id_agendamento,id_patrimonio " +
                "from PatrimonioAgendado " +
                "where id_agendamento = @ID ";
                equipamento = conn.QueryFirstOrDefault<int>(
                     script, new { Id });


            }
            return equipamento;
        }
        

    }
}
