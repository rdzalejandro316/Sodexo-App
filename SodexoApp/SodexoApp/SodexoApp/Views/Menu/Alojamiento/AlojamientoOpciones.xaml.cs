using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Menu.Alojamiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlojamientoOpciones : ContentPage
    {
        public AlojamientoOpciones()
        {
            InitializeComponent();
        }
       

        private async void BtnMapaViviendas_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnMapaViviendas;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tien permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.MapaVivienda());
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "error al abrir:" + w, "OK");
            }
        }

        private async void BtnConsultaVivienda_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnConsultaVivienda;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tien permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.ConsultaVivienda());
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "error al abrir:" + w, "OK");
            }
        }

        private async void BtnConsultaUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnConsultaUsuario;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tien permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.ConsultaUsuario());
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "error al abrir:" + w, "OK");
            }
        }

        private void BtnPrechekin_Clicked(object sender, EventArgs e)
        {

        }

        private void Btnchekin_Clicked(object sender, EventArgs e)
        {

        }

        private void Btnchekout_Clicked(object sender, EventArgs e)
        {

        }



    }
}