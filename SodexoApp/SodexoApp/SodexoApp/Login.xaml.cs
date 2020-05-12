using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SodexoApp.Views.Menu;
using System.Data;
using Newtonsoft.Json;
using Syncfusion.XForms.PopupLayout;

namespace SodexoApp
{
    [DesignTimeVisible(false)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void BtnSession_Clicked(object sender, EventArgs e)
        {
            try
            {


                if (string.IsNullOrEmpty(Tx_codigo.Text))
                {

                    SfPopupLayout popupLayout = new SfPopupLayout();
                    Label a = new Label();
                    a.Text = "aa";
                    popupLayout.Content = a;
                    popupLayout.Show();


                    await DisplayAlert("no hay nada", "deb de haber un codigo", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(Tx_pass.Text))
                {
                    await DisplayAlert("no hay nada", "debe ingresar un password", "OK");
                    return;
                }

                string codigo = Tx_codigo.Text;
                if (codigo.Length < 6 || codigo.Length > 15)
                {
                    await DisplayAlert("no hay nada", "La longitud de codigo debe ser entre 6 a 15 caracteres...", "OK");
                    return;
                }

                string pass = Tx_pass.Text;
                if (pass.Length < 4 || pass.Length > 15)
                {
                    await DisplayAlert("no hay nada", "Password es requerido ó longitud minima es de 4 caracteres", "OK");
                    return;
                }


                StringBuilder sp = new StringBuilder();
                sp.Append("EXEC [dbo].[_ValUser] ");
                sp.Append("'" + Tx_codigo.Text + "',");
                sp.Append("'" + Tx_pass.Text + "',");
                sp.Append("'P'");

                String ret = SiaApi.Query(sp.ToString(), "-2", 1);
                

                if (string.IsNullOrEmpty(ret))
                {
                    await DisplayAlert("no hay nada", "no hay nada", "OK");
                }
                else
                {                    
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(ret, (typeof(DataTable)));
                    if (dt.Rows.Count > 0)
                    {
                        int valid = Convert.ToInt32(dt.Rows[0]["estado"]);

                        if (valid == 1)
                        {
                            

                            StringBuilder s = new StringBuilder();
                            s.Append("EXEC [dbo].[_SeleUser] ");
                            s.Append("@DNI='" + Tx_codigo.Text + "',");                            
                            s.Append("@TipoIngreso='P'");
                            String info = SiaApi.Query(s.ToString(), "-2", 1);
                            DataTable dtinfo = (DataTable)JsonConvert.DeserializeObject(info, (typeof(DataTable)));
                            if (dtinfo.Rows.Count>0)
                            {
                                App.NameUser = dtinfo.Rows[0]["nom1"].ToString().Trim();
                                App.idUser = dtinfo.Rows[0]["dni"].ToString().Trim();
                            }
                            
                            await Navigation.PushModalAsync(new Views.Menu.Menu());
                        }
                        else
                        {
                            await DisplayAlert("alerta", "usuario o contraseña incorrecta", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("alerta", "no hay nada", "OK");
                    }


                }

            }
            catch (Exception w)
            {
                await DisplayAlert("alert", "erro logion:" + w, "OK");
            }
        }




    }
}
