using Android.Content.Res;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }
        public static string idUser;
        public static string NameUser;
        public App()
        {
            //17.4.0.55
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzMDg4QDMxMzcyZTM0MmUzMGNrSUxWOHRyS1kxZU9GdWxLb0dVQVlUK2k5LzZVSkpRZC9mWktldlVJYmc9");
            InitializeComponent();
            //MainPage = new Login();
            //MainPage = new Views.Menu.Mantenimiento.Peticion();
            MainPage = new Views.Menu.Menu();   
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
