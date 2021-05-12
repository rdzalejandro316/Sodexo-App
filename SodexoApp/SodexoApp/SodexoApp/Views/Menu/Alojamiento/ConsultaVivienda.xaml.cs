using Newtonsoft.Json;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                string query = "select (select RTRIM(CampamentoCodigo) as Codigo,RTRIM(CampamentoNombre) as Nombre from CtMae_Campamentos order by CampamentoCodigo for json PATH) as json;";
                LoadCombo(query, CbCampamento);
            }
            catch (Exception w)
            {
                DisplayAlert("error", "error OnAppearing()" + w, "OK");
            }
        }



        private async void LoadCombo(string query, SfComboBox comboBox)
        {
            try
            {
                var slowTask = Task<DataTable>.Factory.StartNew(() => SiaApi.LoadDatatable(query, SiaApi.cnemp));
                await slowTask;

                if (slowTask.Result.Rows.Count > 0)
                {
                    string json = slowTask.Result.Rows[0]["json"].ToString();                    
                    List<Generic> list = JsonConvert.DeserializeObject<List<Generic>>(json);
                    comboBox.DataSource = list;
                }
                else
                {
                    await DisplayAlert("error", "no ahi nada", "OK");
                }

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR : " + w);
            }
        }


        private void CbCampamento_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string campamento = (sender as SfComboBox).SelectedValue.ToString().Trim();

                    CbPuntosv.Clear();
                    CbVivienda.Clear();

                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(PuntoVentaCodigo) AS codigo,RTRIM(PuntoVentaNombre) AS nombre ");
                    query.Append("FROM CtMae_PuntosDeVenta  ");
                    query.Append("where CampamentoCodigo='" + campamento + "' ");
                    query.Append("order by PuntoVentaCodigo for json PATH) as json ");

                    LoadCombo(query.ToString(), CbPuntosv);
                }

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR CbCampamento_SelectionChanged: " + w);
            }
        }


        private void CbPuntosv_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string puntov = (sender as SfComboBox).SelectedValue.ToString().Trim();
                    CbVivienda.Clear();
                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(ViviendaCodigo) AS Codigo,RTRIM(ViviendaCodigo) AS Nombre ");
                    query.Append("FROM CtMae_Viviendas  ");
                    query.Append("where PuntoVentaCodigo='" + puntov + "' ");
                    query.Append("order by ViviendaCodigo for json PATH) as json ");


                    LoadCombo(query.ToString(), CbVivienda);
                }

            }
            catch (Exception w)
            {

                Console.WriteLine("ERRRRRRRRRROR CbPuntosv_SelectionChanged: " + w);
            }
        }


        private async void BtnConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
                #region validaciones

                if (CbCampamento.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione un campamento", "OK");
                    return;
                }

                if (CbPuntosv.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione un punto de venta", "OK");
                    return;
                }

                if (CbVivienda.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione una vivienda", "OK");
                    return;
                }
                #endregion

                string vivienda = CbVivienda.SelectedValue.ToString();
                SfBusyIndicator.IsBusy = true;

                var slowTask = Task<DataSet>.Factory.StartNew(() => LoadData(vivienda));
                await slowTask;

                if (slowTask.Result.Tables[0].Rows.Count > 0)
                {                 
                    GridConsulta.ItemsSource = slowTask.Result.Tables[0].DefaultView;
                    TxTotal.Text = slowTask.Result.Tables[0].Rows.Count.ToString();
                }
                else
                {                    
                    GridConsulta.ItemsSource = null;
                    TxTotal.Text = "0";
                }

               //tabView.SelectedIndex = 1;
                SfBusyIndicator.IsBusy = false;
            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR BtnConsultar_Clicked: " + w);
            }
        }


        public static DataSet LoadData(string casa)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(SiaApi.cnemp);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand("_ConsultaViviendaHistorico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vivienda", casa);
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




    }

    public class Generic
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

}