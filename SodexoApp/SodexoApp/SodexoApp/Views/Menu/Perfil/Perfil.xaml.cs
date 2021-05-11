using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Menu.Perfil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            load();
        }

        public async void load()
        {
            try
            {

                StringBuilder query = new StringBuilder();
                query.Append("select CtMae_Empleados.EmpleadoNombres,CtMae_Empleados.Cedula,CtMae_Empresas.EmpresaNombre,CtMae_EmpleadosCargos.CargoNombre,CtMae_Empleados.Celular ");
                query.Append("from CtMae_Empleados  ");
                query.Append("inner join CtMae_Empresas on CtMae_Empresas.EmpresaCodigo = CtMae_Empleados.EmpresaCodigo ");
                query.Append("inner join CtMae_EmpleadosCargos on CtMae_EmpleadosCargos.CargoId = CtMae_Empleados.CargoId ");
                query.Append("where EmpleadoCodigo='" + App.idUser + "' ");

                //String info = SiaApi.Query(query.ToString(), "010", 1);
                var info = await Task.FromResult<string>(SiaApi.Query(query.ToString(), "010", 1));                

                DataTable dtinfo = (DataTable)JsonConvert.DeserializeObject(info, (typeof(DataTable)));

                if (!string.IsNullOrWhiteSpace(info))
                {
                    TxName.Text = dtinfo.Rows[0]["EmpleadoNombres"].ToString().Trim();
                    TxDocument.Text = dtinfo.Rows[0]["Cedula"].ToString().Trim();
                    TxEmpresa.Text = dtinfo.Rows[0]["EmpresaNombre"].ToString().Trim();
                    TxCargo.Text = dtinfo.Rows[0]["CargoNombre"].ToString().Trim();
                    TxTel.Text = dtinfo.Rows[0]["Celular"].ToString().Trim();
                }
                else
                {
                    TxName.Text = "no hay datos";
                    TxDocument.Text = "no hay datos";
                    TxEmpresa.Text = "no hay datos";
                    TxCargo.Text = "no hay datos";
                    TxTel.Text = "no hay datos";
                }
            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "Error en load:" + w, "OK");
            }
        }





    }
}
