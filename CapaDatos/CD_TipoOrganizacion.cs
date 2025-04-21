using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TipoOrganizacion
    {
        public static CD_TipoOrganizacion _instancia = null;

        private CD_TipoOrganizacion()
        {

        }

        public static CD_TipoOrganizacion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_TipoOrganizacion();
                }
                return _instancia;
            }
        }

        public List<TipoOrganizacion> GetTipoOrganizacion()
        {
            List<TipoOrganizacion> lista = new List<TipoOrganizacion>();
            TipoOrganizacion valor = new TipoOrganizacion();
            SqlDataReader dr;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT [Id], [Nombre],[Active] FROM [TipoOrganizacion] where active = 'true'", oConexion);
                //cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = System.Data.CommandType.Text;
                oConexion.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    valor.Id = int.Parse(dr["Id"].ToString());
                    valor.Nombre = dr["Nombre"].ToString();
                    valor.Activo = dr["Active"].ToString() == "1" ? false : true;
                    lista.Add(valor);
                    valor = new TipoOrganizacion();
                }
            }
            return lista;
        }
    }
}
