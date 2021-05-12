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
    public partial class ConsultaUsuario : ContentPage
    {
        public ConsultaUsuario()
        {
            InitializeComponent();
        }

        private async void BtnConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
                #region validaciones

                if (string.IsNullOrWhiteSpace(TxEmpleado.Text))
                {
                    await DisplayAlert("Alerta", "seleccione un punto de venta", "OK");
                    return;
                }
                #endregion

                string empleado = TxEmpleado.Text;
                SfBusyIndicator.IsBusy = true;

                var slowTask = Task<DataSet>.Factory.StartNew(() => LoadData(empleado));
                await slowTask;

                if (slowTask.Result.Tables[1].Rows.Count > 0)
                {                    
                    GridConsulta.ItemsSource = slowTask.Result.Tables[1].DefaultView;
                    TxTotal.Text = slowTask.Result.Tables[1].Rows.Count.ToString();
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


        public static DataSet LoadData(string empleado)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(SiaApi.cnemp);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand("_ConsultaHistorialReserva", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empleado", empleado);
                cmd.Parameters.AddWithValue("@vivienda", "");
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
}