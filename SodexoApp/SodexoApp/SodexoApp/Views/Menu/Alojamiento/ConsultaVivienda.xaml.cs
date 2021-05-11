using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Menu.Alojamiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaVivienda : ContentPage
    {
        public ConsultaVivienda()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            try
            {
                LoadPv();
            }
            catch (Exception w)
            {
                DisplayAlert("error", "error OnAppearing()" + w, "OK");
            }
        }

        private async void LoadPv()
        {
            try
            {
                var slowTask = Task<DataTable>.Factory.StartNew(() => SiaApi.LoadDatatable("select (select Id_pventa,Nom_pventa  from PuntoVentas for json PATH) as json",SiaApi.cnacceso));
                await slowTask;

                if (slowTask.Result.Rows.Count > 0)
                {
                    string json = slowTask.Result.Rows[0]["json"].ToString();
                    List<Pventas> list = JsonConvert.DeserializeObject<List<Pventas>>(json);
                    CbPuntosv.DataSource = list;
                }
                else
                {
                    await DisplayAlert("error", "no ahi nada", "OK");
                }


            }
            catch (Exception w)
            {
                await DisplayAlert("error", "error en la consulta:" + w, "OK");
            }
        }



        private async void BtnConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
                #region validaciones

                if (CbPuntosv.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione un punto de venta", "OK");
                    return;
                }
                #endregion

                string idPv = CbPuntosv.SelectedValue.ToString();
                SfBusyIndicator.IsBusy = true;

                var slowTask = Task<DataSet>.Factory.StartNew(() => LoadData(idPv));
                await slowTask;

                if (slowTask.Result.Tables[0].Rows.Count > 0)
                {

                    byte[] setFile = (byte[])slowTask.Result.Tables[0].Rows[0]["n"];
                    string json = SiaConfig.Decompress(setFile);
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
                    GridConsulta.ItemsSource = dataSet.Tables[0].DefaultView;
                    TxTotal.Text = dataSet.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    GridConsulta.ItemsSource = null;
                    TxTotal.Text = "0";
                }

                SfBusyIndicator.IsBusy = false;
            }
            catch (Exception w)
            {
                await DisplayAlert("error", "error en la consulta:" + w, "OK");
            }
        }

        public static DataSet LoadData(string pv)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(SiaApi.cnacceso);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand("Reporte_MapaDeViviendaOcupacion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pventas", pv);
                da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public class Pventas
        {
            public int Id_pventa { get; set; }
            public string Nom_pventa { get; set; }
        }




    }
}