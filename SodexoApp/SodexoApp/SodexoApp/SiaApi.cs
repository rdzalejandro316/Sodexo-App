using SodexoApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SodexoApp
{
    public class SiaApi
    {
        //String ApiKey = "d86a3242d704b0bef502cb8431df74a4";
        static String ApiURL = "http://portal.siasoft.slm.cloud:4443/rest/api/sql";
        //public static SiaApi Api = new SiaApi("http://portal.siasoft.slm.cloud:4443/rest/api/sql");
        public static string cnacceso = "Data Source=45.169.253.67,8358;Initial Catalog = acceso; User ID = SiasoftAppAdmon; Password=SiaAppQ1w2e3r4/*/";
        public static string cnemp = "Data Source=45.169.253.67,8358;Initial Catalog = RCServicios_Emp010; User ID = SiasoftAppAdmon; Password=SiaAppQ1w2e3r4/*/";
        public static string cnsiapp = "Data Source=45.169.253.67,8358;Initial Catalog = RCServicios_SiaApp; User ID = SiasoftAppAdmon; Password=SiaAppQ1w2e3r4/*/";

        public enum AccesoEnum
        {
            BtnAlojamiento = 1362,
            BtnAlimentacion = 1363,
            BtnLavanderia = 1364,
            BtnQR = 1365,
            BtnMantenimiento = 1366,
            BtnNotification = 1368,
            BtnConfig = 1368,

            BtnMapaViviendas = 1369,
            BtnConsultaVivienda = 1370,
            BtnConsultaUsuarioAlojamiento = 1371,
            BtnPrechekin = 1372,
            Btnchekin = 1373,
            Btnchekout = 1374,

        }

        public SiaApi(String URL, String Login = "", String Pass = "")
        {
            ApiURL = URL;
        }


        public static String Query(String Query, string emp, int idprojecto)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "SiasoftApi / SLM (Rest Api 1.0)");
            string Json = "";
            if (emp.Trim() != "") Json = "{\"query\":\"" + Query + "\",\"environ\":\"" + "" + emp + "\",\"ID\":\"" + "" + idprojecto + "\" }";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = "";
            try
            {
                byte[] reqString = Encoding.Default.GetBytes(Json);
                Byte[] byteResp = client.UploadData(ApiURL, "POST", reqString);
                response = System.Text.Encoding.UTF8.GetString(byteResp);
                if (response.Contains("SqlException"))
                {
                    return "false";
                }
            }
            catch (Exception)
            {
                //throw;
                response = "false";
            }
            return response;
        }



        public static DataTable LoadDatatable(string query, string conn)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection connection = new SqlConnection(conn))
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                    return table;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<DataTable> LoadDatatableAsync(string query, string conn)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection connection = new SqlConnection(conn))
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                    return table;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static SqlDataReader LoadDataReader(string query, string conn)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static void LoadAccesos()
        {
            try
            {
                StringBuilder acceso = new StringBuilder();
                acceso.Append("select AccesosId,lRun,lNew,lEdit,lDelete,lSearch,lRenum,lPrint,lExport ");
                acceso.Append("from Seg_AccProjectBusinessModulesGroups  ");
                acceso.Append($"where ProjectId={App.ProyectId} and BusinessId={App.BusinessId} and ModulesId={App.ModulesId} and GroupId={App.GroupId} ");

                App.Acc.Clear();


                using (SqlConnection connection = new SqlConnection(cnsiapp))
                {
                    SqlCommand command = new SqlCommand(acceso.ToString(), connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            App.Acc.Add(
                                reader["AccesosId"].ToString(),
                                new AccDir(
                                    Convert.ToBoolean(reader["lRun"].ToString()),
                                    Convert.ToBoolean(reader["lNew"].ToString()),
                                    Convert.ToBoolean(reader["lEdit"].ToString()), 
                                    Convert.ToBoolean(reader["lDelete"].ToString()), 
                                    Convert.ToBoolean(reader["lSearch"].ToString()), 
                                    Convert.ToBoolean(reader["lRenum"].ToString()), 
                                    Convert.ToBoolean(reader["lPrint"].ToString()), 
                                    Convert.ToBoolean(reader["lExport"].ToString())
                                    )
                                );
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRROR LoadAccesos():" + w);
            }
        }








    }

}
