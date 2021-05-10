using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Domain.Entities.Usuarios
{
    public class Telefone
    {
        public int CodigoPais { get; set; }
        public int CodigoCidade { get; set; }
        public int Numero { get; set; }
    }
}
