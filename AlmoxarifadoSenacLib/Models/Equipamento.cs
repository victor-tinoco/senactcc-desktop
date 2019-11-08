using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSenacLib.Modelos
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] SrcImagem { get; set; }
        public int Quantidade { get; set; }
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public string NomeCategoria { get; set; }
        public bool Ativo { get; set; }
    }
}
