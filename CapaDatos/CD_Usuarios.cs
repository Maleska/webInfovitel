using CapaModelos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public static CD_Usuarios _instancia = null;

        private CD_Usuarios()
        {

        }

        public static CD_Usuarios Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Usuarios();
                }
                return _instancia;
            }
        }

        public List<Usuarios> GetUsers(string id)
        {
            List<CapaModelos.Usuarios> lista = new List<CapaModelos.Usuarios>();
            CapaModelos.Usuarios valor = new CapaModelos.Usuarios();
            SqlDataReader dr;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDatos",oConexion);
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                oConexion.Open();

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    valor.Nombres = dr["Nombre"].ToString();
                    valor.ApellidoM = dr["Apellido_Materno"].ToString();
                    valor.ApellidoP = dr["Apellido_Paterno"].ToString();
                    valor.FechaNac = dr["FechaNac"].ToString();
                    valor.Sexo = dr["sexo"].ToString();
                    valor.Id = int.Parse(dr["Id"].ToString());
                    lista.Add(valor);
                }
            }
         return lista;
        }
        public List<DatosMedicos>GetDataMedical(string id)
        {
            List<DatosMedicos> lista = new List<DatosMedicos>();
            DatosMedicos valor = new DatosMedicos();
            SqlDataReader dr;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_DataMedical", oConexion);
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                oConexion.Open();

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    valor.TipoSangre = dr["TipoSangre"].ToString();
                    valor.Alergias = dr["Alergias"].ToString();
                    valor.AlergiasMedicamento = dr["AlergiaMedicamento"].ToString();
                    valor.Enfermedades = dr["Enfermedades"].ToString();
                    valor.Cirugias= dr["Cirugias"].ToString();
                    valor.TomaMedicamento = dr["TomaMedicamentos"].ToString();
                    valor.Medicamentos = dr["Medicamentos"].ToString();
                    lista.Add(valor);
                }
            }
            return lista;
        }

        public string Guardar(Usuarios objeto)
        {
            int valor ;
            string sReturn = string.Empty;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_NuevoUsuario", oConexion);
                cmd.Parameters.AddWithValue("nombre", objeto.Nombres);
                cmd.Parameters.AddWithValue("apellidop", objeto.ApellidoP);
                cmd.Parameters.AddWithValue("apellidom", objeto.ApellidoM);
                cmd.Parameters.AddWithValue("fechanac", objeto.FechaNac);
                cmd.Parameters.AddWithValue("sexo", objeto.Sexo);
                cmd.Parameters.AddWithValue("telefono", objeto.Telefono);
                cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                oConexion.Open();

                cmd.ExecuteNonQuery();
                valor = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                if (valor == 1)
                {
                    sReturn = "Registro guardado correctamente";
                }
                else
                {
                    sReturn = "No se guardo el registro";
                }
                //dr = cmd.ExecuteReader();

                //if (dr.Read())
                //{
                //    valor.TipoSangre = dr["TipoSangre"].ToString();
                //    valor.Alergias = dr["Alergias"].ToString();
                //    valor.AlergiasMedicamento = dr["AlergiaMedicamento"].ToString();
                //    valor.Enfermedades = dr["Enfermedades"].ToString();
                //    valor.Cirugias = dr["Cirugias"].ToString();
                //    valor.TomaMedicamento = dr["TomaMedicamentos"].ToString();
                //    valor.Medicamentos = dr["Medicamentos"].ToString();
                //    lista.Add(valor);
                //}
            }

            return sReturn;
        }

        public bool ValidaUsuario(string user, string pass)
        {
            bool sReturn = false;
            string url = "http://192.52.242.81:8080/Service1.svc/validUser?user="+ user+"&password="+pass+"";

            using (var httpclient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    //request.Headers.TryAddWithoutValidation(name: "SessionId", value: Globals.sLLogin.SessionId);
                    ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    //request.Headers.GetCookies("B1SESSION");
                    ///*request.Headers.Add("Cookie", $"B1SESSION=" + Globals.sLLogin.SessionId);
                    //request.Headers.Add("ROUTEID", ".node1");*/
                    //request.Content = content;
                    //request.Headers.TryAddWithoutValidation(name: "", value: "");
                    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpclient.SendAsync(request).Result;

                    string resultado = response.Content.ReadAsStringAsync().GetAwaiter().GetResult().ToString(); 
                    //(string)esponse.Content.ReadAsStringAsync().Result.ToString();

                    //JObject jsonObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    if (Convert.ToString(resultado) != "\"\"")
                    {
                        sReturn = true;
                    }
                    else
                    {
                        sReturn = false;
                    }
                    
                }
            }
            return sReturn;
        }
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public Usuario obtenerUsuario(string user ,string pass)
        {
            string url = "http://192.52.242.81:8080/Service1.svc/obtenerUsuario?user=" + user + "&password=" + pass + "";
            Usuario usuario = new Usuario();
            using (var httpclient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    //request.Headers.TryAddWithoutValidation(name: "SessionId", value: Globals.sLLogin.SessionId);
                    ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    //request.Headers.GetCookies("B1SESSION");
                    ///*request.Headers.Add("Cookie", $"B1SESSION=" + Globals.sLLogin.SessionId);
                    //request.Headers.Add("ROUTEID", ".node1");*/
                    //request.Content = content;
                    //request.Headers.TryAddWithoutValidation(name: "", value: "");
                    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpclient.SendAsync(request).Result;

                    //var resultado = JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().Result);

                    //XDocument doc = XDocument.Parse(resultado);

                    JObject jsonObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    var resultado = (dynamic)jsonObject;
                    usuario.Id = resultado["Id"];
                    usuario.IdRol = resultado["IdRol"];
                    usuario.Rol = resultado["Rol"];
                    usuario.Fecha_Nac = resultado["Fecha_Nac"];
                    usuario.Nombre = resultado["Nombre"];
                    usuario.apellidoM = resultado["apellidoM"];
                    usuario.apellidoP = resultado["apellidoP"];
                    usuario.sexo = resultado["sexo"];
                    usuario.telefono = resultado["telefono"];
                    
                    //if (response.StatusCode == HttpStatusCode.OK) 
                    //{
                    //    sReturn = true;
                    //}
                    //else
                    //{
                    //    sReturn = false;
                    //}
                    //usuario 
                   //foreach (KeyValuePair<string, JToken> jToken in jsonObject)
                   // {

                   //     Usuario.Add(new Usuario
                   //     {
                   //         Id = jToken.Key,
                   //         User = jToken.Value["user"].ToString(),
                   //         Password = jToken.Value["password"].ToString(),
                   //         Website = jToken.Value["website"].ToString()

                   //     });

                   // }

                }
            }
            return usuario;
        }
    }
}
