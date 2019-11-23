using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AlmoxarifadoAPI.Controllers
{

    public class AgendamentoController : ApiController
    {
        // GET: api/Agendamento
        public IHttpActionResult Get(DateTime dia, TimeSpan horaretirada,TimeSpan horadevolucao, int id)
        {
            try
            {
                AgendamentoRepositorio repo = new AgendamentoRepositorio();
                var agend = repo.ConsultarAgendamento(dia,horaretirada,horadevolucao, id);
                var QuantidadeDisponivel = 0;
          
                if(agend.Count> 0)
                {
                    QuantidadeDisponivel = agend.First().Quantidade;
                }
                else
                {
                    EquipamentoRepositorio repoEquip = new EquipamentoRepositorio();
                  
                    Equipamento equipamento = new Equipamento();
                    equipamento = repoEquip.ConsultarPorId(id);
                    QuantidadeDisponivel = equipamento.Quantidade;

                }
                return Ok(QuantidadeDisponivel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }


        // GET: api/Agendamento/5
        public IHttpActionResult Get(int ID)
        {
            try
            {
                AgendamentoRepositorio repo = new AgendamentoRepositorio();
                var agend = repo.PesquisarAgendamentoPorIDUser(ID);
                return Ok(agend);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }

        // POST: api/Agendamento
        public IHttpActionResult Post([FromBody]Agendamento agendamento )
        {
            var Id = "";
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                Id = claims.First(x => x.Type == "Id").Value;

            }
            agendamento.IdUsuario = Convert.ToInt32(Id);
            //agendamento.IdUsuario = 4;
            try
            {
                AgendamentoRepositorio Repo = new AgendamentoRepositorio();
                Repo.InserirAgendamento(agendamento);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
