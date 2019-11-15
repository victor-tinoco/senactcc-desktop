using AlmoxarifadoSenacLib.Modelos;
using AlmoxarifadoSenacLib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using static AlmoxarifadoSenacLib.Repositorios.AutenticacaoAD;
using AlmoxarifadoAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http.Cors;

namespace AlmoxarifadoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private TokenGerado createToken(string username, int id)
        {
            //Data do Token
            DateTime issuedAt = DateTime.UtcNow;

            //Tempo de expiraçao em dias
            DateTime expires = DateTime.UtcNow.AddDays(1);

            var tokenHandler = new JwtSecurityTokenHandler();

            //cria a identidade do usuário que será concedido acesso
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username.ToUpper()),
                new Claim("Id", id.ToString())
            });

            const string sec = "401b09eab3c013d4ca54922bb802beca108fd53181992b70a75ff2015d8bf37274290f90fb313759f1afbd03f44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //cria o token propriamente dito
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:65379", audience: "http://localhost:65379",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);

            TokenGerado tokenGenerado = new TokenGerado
            {
                Username = username.ToUpper(),
                Expires = token.ValidTo,
                Token = tokenHandler.WriteToken(token)
            };

            return tokenGenerado;
        }
        // POST: api/Login
        public IHttpActionResult Post(Login login)
        {
            bool loginvalido = false;
            UsuarioRepositorio repo = new UsuarioRepositorio();
            Usuario usuario;
            usuario = repo.ConsultarPorEmail(login.Usuario);
            Domain_Authentication domain = new Domain_Authentication(login.Usuario, login.Senha, System.Configuration.ConfigurationManager.AppSettings["Dominio"].ToString());
            if (usuario != null && domain.IsValid())
            {
                loginvalido = true;
            }
            if (loginvalido == true)
            {
                TokenGerado token = createToken(login.Usuario, usuario.Id);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
