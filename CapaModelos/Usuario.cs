using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaModelos
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string Fecha_Nac { get; set; }
        public string sexo { get; set; }
        public string telefono { get; set; }
        public string IdRol { get; set; }
        public string Rol { get; set; }
    }
}