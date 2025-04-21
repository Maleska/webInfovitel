using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TipoContacto
    {
        public static CD_TipoContacto _instancia = null;

        private CD_TipoContacto()
        {

        }

        public static CD_TipoContacto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_TipoContacto();
                }
                return _instancia;
            }
        }

        public List<TipoContacto> GetTipoContacto()
        {
            List<TipoContacto> lista = new List<TipoContacto>();
            TipoContacto valor = new TipoContacto();
            SqlDataReader dr;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("SELECT [Id], [TipoContacto],[Activo] FROM [TipoContacto]", oConexion);
                //cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = System.Data.CommandType.Text;

                oConexion.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    valor.Id = int.Parse(dr["Id"].ToString());
                    valor.Nombre = dr["TipoContacto"].ToString();
                    valor.Activo = dr["Activo"].ToString() == "1"? false:true;
                    lista.Add(valor);
                    valor = new TipoContacto();
                }
            }
            return lista;
        }

        public  List<PersonalContacto> GetPersonalContacto(string id)
        {
            List<PersonalContacto> lista = new List<PersonalContacto>();
            PersonalContacto valor = new PersonalContacto();
            SqlDataReader dr;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_datoscontacto", oConexion);
                cmd.Parameters.AddWithValue("id", Guid.Parse(id));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                oConexion.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    valor.Id = int.Parse(dr["Id"].ToString());
                    valor.Nombre = dr["Nombre"].ToString();
                    valor.Telefono = dr["Telefono"].ToString();
                    valor.Contacto = dr["TipoContacto"].ToString();
                    lista.Add(valor);
                    valor = new PersonalContacto();
                }
            }
            return lista;
        }

    }
}
