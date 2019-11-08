using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSenacLib.Modelos
{

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DsUsuario { get; set; }
        public string DStipousuario { get; set; }
        public int TipoUsuario { get; set; }
        public string Email { get; set; }
        public int NumeroRegistro { get; set; }
        public bool Ativo { get; set; }
    }

}
