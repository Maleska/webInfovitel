using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Integrantes
    {
        public Guid IdResponsable { get; set; }
        public string Organizacion { get; set; }
        public string NombreResponsable { get; set; }
        public string Telefono { get; set; }
        public Guid IdIntegrante { get; set; }
        public string NombreIntegrante { get; set; }
        public string Fecha { get; set; }
        public bool Activo { get; set; }
    }
}
