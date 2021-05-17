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
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
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
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
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

        private async void BtnConsultaUsuarioAlojamiento_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnConsultaUsuarioAlojamiento;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
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

        private async void BtnPrechekin_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnPrechekin;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.Check("Pre Chekin", "04"));
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "error al abrir:" + w, "OK");
            }
        }

        private async void Btnchekin_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnPrechekin;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.Check("Check-IN", "02"));
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta", "error al abrir:" + w, "OK");
            }
        }

        private void Btnchekout_Clicked(object sender, EventArgs e)
        {

        }

        
    }
}