using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Integrantes
    {
        public static CD_Integrantes _instancia = null;

        private CD_Integrantes()
        {

        }

        public static CD_Integrantes Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Integrantes();
                }
                return _instancia;
            }
        }
        public List<Integrantes> ObtenerIntegrantes()
        {
            List<Integrantes> lista = new List<Integrantes>();
            Integrantes valor = new Integrantes();
            SqlDataReader dr;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerClientes", oConexion);
                //cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                oConexion.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    valor.IdResponsable= Guid.Parse(dr["Id"].ToString());
                    valor.Organizacion = dr["Organizacion"].ToString();
                    valor.NombreResponsable = dr["Nombre"].ToString();
                    valor.Telefono = dr["Telefono"].ToString();
                    valor.IdIntegrante = Guid.Parse(dr["IdIntegrante"].ToString());
                    valor.NombreIntegrante = dr["NombreIntegrante"].ToString();
                    valor.Fecha = dr["FechaCreacion"].ToString();
                    valor.Activo = dr["Activo"].ToString() == "1" ? false : true;
                    lista.Add(valor);
                    valor = new Integrantes();
                }
            }
            return lista;
        }
    }
}
