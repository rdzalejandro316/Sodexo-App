using Newtonsoft.Json;
using SodexoApp.Helpers;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.Shimmer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Menu.PQRS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        public Formulario()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {

            try
            {
                LoadInicial();
            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR ContentPage_Appearing(): " + w);
            }
        }

        public async void LoadInicial()
        {
            try
            {

                StringBuilder query = new StringBuilder();
                query.Append("select (select RTRIM(EmpresaCodigo) as Codigo,RTRIM(EmpresaNombre) as Nombre ");
                query.Append("from CtMae_Empresas ");
                query.Append("order by EmpresaCodigo for json PATH) as json; ");


                var slowTask = LoadCombo(query.ToString());
                await slowTask;

                if (slowTask.IsCompleted)
                {
                    CbEmpresa.DataSource = slowTask.Result;
                }

                loadStack.IsVisible = false;
                LoadGrid.IsVisible = true;
                foreach (SfShimmer shimmer in loadStack.Children) shimmer.IsActive = false;

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR LoadInicial(): " + w);
            }

        }

        private async Task<List<Generic>> LoadCombo(string query)
        {
            try
            {
                var slowTask = Task<DataTable>.Factory.StartNew(() => SiaApi.LoadDatatable(query, SiaApi.cnemp));
                await slowTask;

                List<Generic> list = new List<Generic>();
                if (slowTask.Result.Rows.Count > 0)
                {
                    string json = slowTask.Result.Rows[0]["json"].ToString();
                    list = JsonConvert.DeserializeObject<List<Generic>>(json);
                }
                else
                {
                    await DisplayAlert("error", "no tiene datos", "OK");
                }

                return list;

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR LoadCombo(): " + w);
                return null;
            }
        }


        
        private async void CbCampamento_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                //if ((sender as SfComboBox).SelectedIndex >= 0)
                //{
                //    string campamento = (sender as SfComboBox).SelectedValue.ToString().Trim();

                //    CbPuntosv.Clear();
                //    CbVivienda.Clear();
                //    CbHabitacion.Clear();


                //    StringBuilder query = new StringBuilder();
                //    query.Append("select (SELECT  RTRIM(CCostoCodigo) AS codigo,RTRIM(CCostoNombre) AS nombre ");
                //    query.Append("FROM CtMae_CentroDeCostos  ");
                //    query.Append("where CCostoCodigo='" + campamento + "' ");
                //    query.Append("order by PuntoVentaCodigo for json PATH) as json ");

                //    var slowTask = LoadCombo(query.ToString());
                //    await slowTask;

                //    if (slowTask.IsCompleted) CbPuntosv.DataSource = slowTask.Result;

                }

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR CbCampamento_SelectionChanged: " + w);
            }
        }

        private void CbPuntosv_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        private void CbVivienda_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        private void CbHabitacion_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }



        private void BtnValidar_Clicked(object sender, EventArgs e)
        {

        }

        
    }
}