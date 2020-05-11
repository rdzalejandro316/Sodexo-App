using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SodexoApp
{
    public class SiaApi
    {
        String ApiKey = "d86a3242d704b0bef502cb8431df74a4";
        static String ApiURL = "http://portal.siasoft.slm.cloud:4443/rest/api/sql";

        //public static SiaApi Api = new SiaApi("http://portal.siasoft.slm.cloud:4443/rest/api/sql");

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
            catch (Exception e)
            {
                //throw;
                response = "false";
            }
            return response;
        }





    }

}
