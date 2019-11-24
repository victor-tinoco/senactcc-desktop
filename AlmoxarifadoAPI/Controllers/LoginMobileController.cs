using AlmoxarifadoAPI.Models;
using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AlmoxarifadoAPI.Controllers;

namespace AlmoxarifadoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class LoginMobileController : ApiController
    {
        LoginController controller = new LoginController();
        public IHttpActionResult Post(Login login)
        {
            UsuarioRepositorio repo = new UsuarioRepositorio();
            Usuario usuario;
            usuario = repo.ConsultarPorEmail(login.Usuario);
       
            if (usuario != null)
            {
                TokenGerado token = controller.createToken(login.Usuario, usuario.Id);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

    }
}
