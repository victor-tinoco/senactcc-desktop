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
    public class UsuarioRepositorio
    {
        public Usuario ConsultarPorEmail(string email)
        {
            Usuario usuario;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "SELECT id_usuario Id," +
                    "nm_usuario Nome,ds_usuario DsUsuario,ds_email Email," +
                    "id_tipo_usuario TipoUsuario,nmr_registro NumeroRegistro,fl_status Ativo " +
                    "From Usuario WHERE ds_usuario = @EMAIL";
                usuario = conn.QueryFirstOrDefault<Usuario>(
                     script, new { email });
            }
            return usuario;
        }
        public Usuario ConsultarPorID(int id)
        {
            Usuario usuario;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script =
                    "SELECT id_usuario Id," +
                    "nm_usuario Nome,ds_usuario DsUsuario,ds_email Email, " +
                    "id_tipo_usuario TipoUsuario,nmr_registro NumeroRegistro,fl_status Ativo " +
                    "From Usuario WHERE id_usuario = @ID";
                usuario = conn.QueryFirstOrDefault<Usuario>(
                     script, new { id });
            }
            return usuario;
        }
        public void InserirUsuario(Usuario usuario)
        {
            foreach (var user in PesquisarUsuario(""))
            {
                if (user.NumeroRegistro == usuario.NumeroRegistro)
                {
                    throw new Exception("Já existe um usuário com este número de registro.");
                }
            }

            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "insert into Usuario(nm_usuario, ds_usuario,id_tipo_usuario, nmr_registro, fl_status,ds_email)" +
                "values(@NOME, @LOGIN, @TIPO, @NUMERO, @STATUS,@EMAIL)";

                coon.Execute(script, new
                {
                    @NOME = usuario.Nome,
                    @LOGIN = usuario.DsUsuario,
                    @TIPO = usuario.TipoUsuario,
                    @NUMERO = usuario.NumeroRegistro,
                    @STATUS = usuario.Ativo,
                    @EMAIL = usuario.Email
                });
            }
        }

        public List<Usuario> PesquisarUsuario(string filtro)
        {
            List<Usuario> Usuario;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "select id_usuario Id,nm_usuario Nome,ds_usuario DsUsuario," +
                    "usuario.id_tipo_usuario TipoUsuario,nm_tipo_usuario DStipousuario," +
                    "nmr_registro NumeroRegistro,fl_status Ativo,ds_email Email " +
                    "from Usuario " +
                    "join " +
                    "TipoUsuario on Usuario.id_tipo_usuario = TipoUsuario.Id_tipo_usuario " +
                    "where nm_usuario like '%' + @FILTRO + '%'";

                Usuario = conn.Query<Usuario>(script, new { @FILTRO = filtro }).ToList();
            }
            return Usuario;
        }
        public void excluir(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "Delete Usuario WHERE id_usuario = @ID";
                conn.Execute(script, new { @ID = id });
            }
        }
        public void Alterar(Usuario usuario)
        {
            foreach (var user in PesquisarUsuario(""))
            {
                if (user.NumeroRegistro == usuario.NumeroRegistro && user.Id != usuario.Id)
                {
                    throw new Exception("Já existe um usuário com este número de registro");
                }
            }

            using (SqlConnection coon = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = "update Usuario set "+
                "nm_usuario = @NOME,ds_usuario = @LOGIN,id_tipo_usuario = @TIPO,nmr_registro = @NUMERO,fl_status = @ATIVO,ds_email = @EMAIL "+
                "where id_usuario = @ID";
                coon.Execute(script, new
                {
                    @NOME = usuario.Nome,
                    @LOGIN = usuario.DsUsuario,
                    @TIPO = usuario.TipoUsuario,
                    @NUMERO = usuario.NumeroRegistro,
                    @ID = usuario.Id,
                    @ATIVO = usuario.Ativo,
                    @EMAIL = usuario.Email
                });

            }
        }
    }
}
