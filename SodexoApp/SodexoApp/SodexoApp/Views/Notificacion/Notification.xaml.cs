using Newtonsoft.Json;
using Syncfusion.XForms.Shimmer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SodexoApp.Views.Notificacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notification : ContentPage
    {        
        public Notification()
        {
            InitializeComponent();
            
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            loadNotification();
        }

        public async void loadNotification() 
        {

            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;

                loadStack.IsVisible = true;
                LoadGrid.IsVisible = false;
                foreach (SfShimmer shimmer in loadStack.Children) shimmer.IsActive = true;
                


                var slowTask = Task<String>.Factory.StartNew(() => LoadData(source.Token), source.Token);
                await slowTask;

                if (!string.IsNullOrEmpty(((String)slowTask.Result)))
                {
                    var noti = new List<Notifications>();
                    String json = ((String)slowTask.Result);
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        noti.Add(new Notifications
                        {
                            Title = item["title"].ToString(),
                            Messagen = item["messagen"].ToString(),
                            fecha = DateTime.Now
                        }); ; 
                    }
                                        
                    listView.ItemsSource = noti;
                }


                loadStack.IsVisible = false;
                LoadGrid.IsVisible = true;
                foreach (SfShimmer shimmer in loadStack.Children) shimmer.IsActive = false;   

            }
            catch (Exception w)
            {
                await DisplayAlert("error","errro:"+w,"OK");
            }           
        }


        private String LoadData(CancellationToken cancellationToken)
        {

            String ret = "";

            //string query = "EXEC [dbo].[_ValUser] ";   
            //query += ""++",";
            //query += ""++",";
            //query += "'P'";

            try
            {
                ret = SiaApi.Query("select * from CtNotification", "-2",1);  
            }
            catch (Exception)
            {
                return null;
            }
            return ret;
        }

        class Notifications
        {
            public string Title { get; set; }
            public string Messagen { get; set; }
            public DateTime fecha { get; set; }            

        }        
    }
}