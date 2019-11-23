using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AlmoxarifadoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class EquipamentoController : ApiController
    {

        // GET: api/Equipamento
        public IHttpActionResult Get(string filtro,string categoria,int iniciopag = 0,int fimpag = 0   )
        {
            try
            {
                EquipamentoRepositorio repo = new EquipamentoRepositorio();
                var equip = repo.ListadeEquipamentos(filtro,categoria,iniciopag,fimpag);
                return Ok(equip);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
   
        public IHttpActionResult Get(string filtro,string categoria)
        {
            try
            {

                EquipamentoRepositorio repo = new EquipamentoRepositorio();
                var equip = repo.Paginacao(filtro,categoria);
                return Ok(equip);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }


        // GET: api/Equipamento/5
        public IHttpActionResult Get(int Id)
        {
            try
            {
                EquipamentoRepositorio repo = new EquipamentoRepositorio();
                var equip = repo.ConsultarPorId(Id);
                return Ok(equip);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
