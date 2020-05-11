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
        }
        

        private async void BtnAlojamiento_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.MasterD.IsPresented = false;
                await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Alojamiento.AlojamientoOpciones());
            }
            catch (Exception w)
            {
                await DisplayAlert("errro","error al abrir:"+w,"OK");
            }
        }

        private async void BtnLavanderia_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.MasterD.IsPresented = false;
                await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Lavanderia.LavanderiaInfo());
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
                App.MasterD.IsPresented = false;
                await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Mantenimiento.Peticion());
            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }

        private async void BtnPerfil_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.MasterD.IsPresented = false;
                await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Menu.Perfil.Perfil());
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
                App.MasterD.IsPresented = false;
                await App.MasterD.Detail.Navigation.PushAsync(new SodexoApp.Views.Notificacion.Notification());                
            }
            catch (Exception w)
            {
                await DisplayAlert("errro", "error al abrir:" + w, "OK");
            }
        }



    }
}