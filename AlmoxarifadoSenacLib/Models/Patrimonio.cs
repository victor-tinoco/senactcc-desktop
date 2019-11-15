using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSenacLib.Modelos
{
    public class Patrimonio
    {
        public int Id { get; set; }
        public int IdEquipamento { get; set; }
        public Equipamento Equipamento { get; set; }
        public int NumeroPatrimonio { get; set; }
        public bool Ativo { get; set; }
        public PatrimonioAgendado patrimonioagendado { get; set; }
    }
}
