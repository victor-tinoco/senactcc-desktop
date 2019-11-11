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
    public class CategoriaController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        public IHttpActionResult Get()
        {
            var filtro = "";
            try
            {

                CategoriaRepositorio repo = new CategoriaRepositorio();
                var Categoria = repo.PesquisarCategoria(filtro);
                return Ok(Categoria);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }


        }
    }
}
