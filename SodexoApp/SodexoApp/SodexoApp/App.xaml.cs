//using Android.Content.Res;
using SodexoApp.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }
        public static string idUser = "1033796537";
        public static string NameUser = "Admon";
        
        public static int GroupId = 1;
        public static int ProyectId = 1;
        public static int BusinessId = 1;
        public static int ModulesId = 11;

        public static Dictionary<string, AccDir> Acc = new Dictionary<string, AccDir>();

        public App()
        {
            //17.4.0.55
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzMDg4QDMxMzcyZTM0MmUzMGNrSUxWOHRyS1kxZU9GdWxLb0dVQVlUK2k5LzZVSkpRZC9mWktldlVJYmc9");
            InitializeComponent();
            //MainPage = new Login();
            //MainPage = new Views.Menu.Mantenimiento.Peticion();
            MainPage = new Views.Menu.Menu();   
            //MainPage = new Views.Menu.Alojamiento.ConsultaVivienda();   
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
