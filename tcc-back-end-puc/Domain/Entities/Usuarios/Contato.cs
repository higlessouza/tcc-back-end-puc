using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Domain.Entities.Usuarios
{
    public class Contato
    {
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public Telefone Telefone { get; set; }
    }
}
