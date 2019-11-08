using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AlmoxarifadoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class AgendamentoController : ApiController
    {
        // GET: api/Agendamento
        public IHttpActionResult Get(DateTime dia, TimeSpan horaretirada,TimeSpan horadevolucao, int id)
        {
            try
            {
                AgendamentoRepositorio repo = new AgendamentoRepositorio();
                var agend = repo.ConsultarAgendamento(dia,horaretirada,horadevolucao, id);
                return Ok(agend);
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
