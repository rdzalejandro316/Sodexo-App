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
    public partial class MenuItems : ContentPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public MenuItems()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            try
            {

                menuList = new List<MasterPageItem>();
                menuList.Add(new MasterPageItem() { Title = "Inicio", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Pre Check-in", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Check-in", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Check-out", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Encuestas", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Capacitacion", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Consultar Empleado", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                menuList.Add(new MasterPageItem() { Title = "Cerrar Cession", IconSource = "cateting.png", TargetType = typeof(Views.Menu.Menu) });
                navigationDrawerList.ItemsSource = menuList;

                TxName.Text = App.NameUser;
                TxCC.Text = App.idUser;
            }
            catch (Exception w)
            {
                DisplayAlert("aalert", "error:" + w, "OK");
            }
        }


        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (MasterPageItem)e.SelectedItem;
                Type page = item.TargetType;

                if (item.Title == "Inicio")
                {
                    //Application.Current.MainPage = new NavigationPage(new Views.Menu());
                }
                else
                {
                    await App.MasterD.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(page));
                }
                App.MasterD.IsPresented = false;
            }
            catch (Exception w)
            {
                await DisplayAlert("error en inicio", "errro:" + w, "OK");
            }
        }


    }


    public class MasterPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }


}