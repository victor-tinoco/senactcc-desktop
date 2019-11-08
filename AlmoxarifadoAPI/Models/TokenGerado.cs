using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmoxarifadoAPI.Models
{
    public class TokenGerado
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public DateTime Expires { get; set; }
    }
}