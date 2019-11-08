using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSenacLib.Modelos
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdEquipamento { get; set; }
        public Equipamento Equipamento { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime Dia { get; set; }
        public TimeSpan DataHoraRetirada { get; set; }
        public TimeSpan DataHoraDevolucao { get; set; }
        public string StatusDevolucao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public Usuario UsuarioAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
