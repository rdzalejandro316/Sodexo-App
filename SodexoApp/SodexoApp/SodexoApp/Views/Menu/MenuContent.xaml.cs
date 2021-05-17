using SodexoApp.Helpers;
using Syncfusion.XForms.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuContent : ContentPage
    {
        public MenuContent()
        {
            InitializeComponent();
            SiaApi.LoadAccesos();
        }
        

        private async void BtnAlojamiento_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnAlojamiento;
                
                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {                    
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new Alojamiento.AlojamientoOpciones());
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alerta","error al abrir:"+w,"OK");
            }
        }
        private async void BtnAlimentacion_Clicked(object sender, EventArgs e)
        {
            try
            {

                int idacceso = (int)SiaApi.AccesoEnum.BtnAlimentacion;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permisos para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Alimentacion.AlimentacionOpciones());
                }

            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }
        private async void BtnLavanderia_Clicked(object sender, EventArgs e)
        {
            try
            {
                int idacceso = (int)SiaApi.AccesoEnum.BtnLavanderia;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Lavanderia.LavanderiaOpciones());
                }

                
            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }



        private async void BtnMantenimiento_Clicked(object sender, EventArgs e)
        {
            try
            {
                int idacceso = (int)SiaApi.AccesoEnum.BtnMantenimiento;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Mantenimiento.Peticion());
                }


            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }

        private async void BtnQR_Clicked(object sender, EventArgs e)
        {
            try
            {
                int idacceso = (int)SiaApi.AccesoEnum.BtnQR;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Mantenimiento.Peticion());
                }


            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }

        private async void BtnConfig_Clicked(object sender, EventArgs e)
        {
            try
            {
                int idacceso = (int)SiaApi.AccesoEnum.BtnConfig;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Mantenimiento.Peticion());
                }


            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }


        private async void BtnNotification_Clicked(object sender, EventArgs e)
        {
            try
            {
                int idacceso = (int)SiaApi.AccesoEnum.BtnNotification;

                if (!SiaConfig.ValidAcceso(idacceso.ToString()))
                {
                    await DisplayAlert("Alerta", $"El usuario {App.NameUser} no tiene permiso para esta opcion", "OK");
                    return;
                }
                else
                {
                    App.MasterD.IsPresented = false;
                    await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Notificacion.Notification());
                }


            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }            
        }

             

       
    }
}