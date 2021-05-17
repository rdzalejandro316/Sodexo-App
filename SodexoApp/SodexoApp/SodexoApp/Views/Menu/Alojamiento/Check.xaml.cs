using Newtonsoft.Json;
using SodexoApp.Helpers;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.Shimmer;
using Syncfusion.XForms.TextInputLayout;
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
    public partial class Check : ContentPage
    {
        string concepto = "";

        public Check(string title, string con)
        {
            InitializeComponent();
            this.Title = title;
            concepto = con;
        }


        private void ContentPage_Appearing(object sender, EventArgs e)
        {

            try
            {
                LoadInicial();

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR ContentPage_Appearing(): " + w);
            }
        }

        public async void LoadInicial()
        {
            try
            {

                StringBuilder query = new StringBuilder();
                query.Append("select (select RTRIM(CampamentoCodigo) as Codigo,RTRIM(CampamentoNombre) as Nombre ");
                query.Append("from CtMae_Campamentos ");
                query.Append("order by CampamentoCodigo for json PATH) as json; ");


                var slowTask = LoadCombo(query.ToString());
                await slowTask;

                if (slowTask.IsCompleted)
                {
                    CbCampamento.DataSource = slowTask.Result;
                }

                loadStack.IsVisible = false;
                LoadGrid.IsVisible = true;
                foreach (SfShimmer shimmer in loadStack.Children) shimmer.IsActive = false;

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR LoadInicial(): " + w);
            }

        }

        private async Task<List<Generic>> LoadCombo(string query)
        {
            try
            {
                var slowTask = Task<DataTable>.Factory.StartNew(() => SiaApi.LoadDatatable(query, SiaApi.cnemp));
                await slowTask;

                List<Generic> list = new List<Generic>();
                if (slowTask.Result.Rows.Count > 0)
                {
                    string json = slowTask.Result.Rows[0]["json"].ToString();
                    list = JsonConvert.DeserializeObject<List<Generic>>(json);
                }
                else
                {
                    await DisplayAlert("error", "no tiene datos", "OK");
                }

                return list;

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR LoadCombo(): " + w);
                return null;
            }
        }

        private async void CbCampamento_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string campamento = (sender as SfComboBox).SelectedValue.ToString().Trim();

                    CbPuntosv.Clear();
                    CbVivienda.Clear();
                    CbHabitacion.Clear();
                    CbCama.Clear();

                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(PuntoVentaCodigo) AS codigo,RTRIM(PuntoVentaNombre) AS nombre ");
                    query.Append("FROM CtMae_PuntosDeVenta  ");
                    query.Append("where CampamentoCodigo='" + campamento + "' ");
                    query.Append("order by PuntoVentaCodigo for json PATH) as json ");

                    var slowTask = LoadCombo(query.ToString());
                    await slowTask;

                    if (slowTask.IsCompleted) CbPuntosv.DataSource = slowTask.Result;

                }

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR CbCampamento_SelectionChanged: " + w);
            }
        }


        private async void CbPuntosv_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string puntov = (sender as SfComboBox).SelectedValue.ToString().Trim();

                    CbVivienda.Clear();
                    CbHabitacion.Clear();
                    CbCama.Clear();

                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(ViviendaCodigo) AS Codigo,RTRIM(ViviendaCodigo) AS Nombre ");
                    query.Append("FROM CtMae_Viviendas  ");
                    query.Append("where PuntoVentaCodigo='" + puntov + "' ");
                    query.Append("order by ViviendaCodigo for json PATH) as json ");

                    var slowTask = LoadCombo(query.ToString());
                    await slowTask;

                    if (slowTask.IsCompleted) CbVivienda.DataSource = slowTask.Result;
                }

            }
            catch (Exception w)
            {

                Console.WriteLine("ERRRRRRRRRROR CbPuntosv_SelectionChanged: " + w);
            }
        }

        private async void CbVivienda_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string vivienda = (sender as SfComboBox).SelectedValue.ToString().Trim();
                    CbHabitacion.Clear();
                    CbCama.Clear();
                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(HabitacionCodigo) AS Codigo,RTRIM(HabitacionCodigo) AS Nombre ");
                    query.Append("FROM CtMae_ViviendasHabitaciones  ");
                    query.Append("where ViviendaCodigo='" + vivienda + "' ");
                    query.Append("order by ViviendaCodigo for json PATH) as json ");

                    var slowTask = LoadCombo(query.ToString());
                    await slowTask;
                    if (slowTask.IsCompleted) CbHabitacion.DataSource = slowTask.Result;
                }

            }
            catch (Exception w)
            {

                Console.WriteLine("ERRRRRRRRRROR CbPuntosv_SelectionChanged: " + w);
            }
        }

        private async void CbHabitacion_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {

                if ((sender as SfComboBox).SelectedIndex >= 0)
                {
                    string habitacion = (sender as SfComboBox).SelectedValue.ToString().Trim();
                    string vivienda = CbVivienda.SelectedValue.ToString().Trim();

                    CbCama.Clear();
                    StringBuilder query = new StringBuilder();
                    query.Append("select (SELECT  RTRIM(CamaCodigo) AS Codigo,RTRIM(CamaCodigo) AS Nombre ");
                    query.Append("FROM CtMae_ViviendaHabitacionCamas  ");
                    query.Append("where ViviendaCodigo='" + vivienda + "' and HabitacionCodigo='" + habitacion + "' ");
                    query.Append("order by ViviendaCodigo for json PATH) as json ");

                    var slowTask = LoadCombo(query.ToString());
                    await slowTask;
                    if (slowTask.IsCompleted) CbCama.DataSource = slowTask.Result;
                }

            }
            catch (Exception w)
            {

                Console.WriteLine("ERRRRRRRRRROR CbPuntosv_SelectionChanged: " + w);
            }
        }

        private async void BtnValidar_Clicked(object sender, EventArgs e)
        {
            try
            {

                EfectButton(true);

                #region validaciones

                if (CbCampamento.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione un campamento", "OK");
                    EfectButton(false);
                    return;
                }

                if (CbPuntosv.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione un punto de venta", "OK");
                    EfectButton(false);
                    return;
                }

                if (CbVivienda.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione una vivienda", "OK");
                    EfectButton(false);
                    return;
                }

                if (CbHabitacion.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione una habitacion", "OK");
                    EfectButton(false);
                    return;
                }

                if (CbCama.SelectedIndex < 0)
                {
                    await DisplayAlert("Alerta", "seleccione una cama", "OK");
                    EfectButton(false);
                    return;
                }

                string empresa = "";
                string cco = "";
                if (string.IsNullOrEmpty(TxEmpleado.Text))
                {
                    await DisplayAlert("Alerta", "ingrese el codigo del empleado", "OK");
                    EfectButton(false);
                    return;
                }
                else
                {

                    string query_emp = "select emp.EmpleadoCodigo,emp.EmpresaCodigo,cco.CCostoCodigo ";
                    query_emp += "from CtMae_Empleados as emp ";
                    query_emp += "inner join CtMae_Empresas as bussi on bussi.EmpresaCodigo = emp.EmpresaCodigo ";
                    query_emp += "inner join CtMae_CentroDeCostos as cco on cco.CCostoCodigo = bussi.CCostoCodigo ";
                    query_emp += "where emp.EmpleadoCodigo='" + TxEmpleado.Text.Trim() + "' ";

                    var DtEmpleado = SiaApi.LoadDatatableAsync(query_emp, SiaApi.cnemp);
                    await DtEmpleado;

                    if (DtEmpleado.IsCompleted)
                    {
                        if (DtEmpleado.Result.Rows.Count > 0)
                        {
                            empresa = DtEmpleado.Result.Rows[0]["EmpresaCodigo"].ToString().Trim();
                            cco = DtEmpleado.Result.Rows[0]["CCostoCodigo"].ToString().Trim();
                        }
                        else
                        {
                            await DisplayAlert("Alerta", $"el empleado:{TxEmpleado.Text} que ingreso no existe", "OK");
                            EfectButton(false);
                            return;
                        }
                    }
                }

                DateTime fi = new DateTime(TxFecIng.Date.Year, TxFecIng.Date.Month, TxFecIng.Date.Day, TxHorIng.Time.Hours, TxHorIng.Time.Minutes, 0);
                DateTime fs = new DateTime(TxFecSal.Date.Year, TxFecSal.Date.Month, TxFecSal.Date.Day, TxHorSal.Time.Hours, TxHorSal.Time.Minutes, 0);


                if (fs <= fi)
                {
                    await DisplayAlert("Alerta", "La fecha final debe ser mayor a la fecha inicial.....", "OK");
                    EfectButton(false);
                    return;
                }


                string fingreso = fi.ToString("dd/MM/yyyy");
                string fsalida = fs.ToString("dd/MM/yyyy");
                string vivienda = CbVivienda.SelectedValue.ToString().Trim();
                string habitacion = CbHabitacion.SelectedValue.ToString().Trim();
                string cama = CbCama.SelectedValue.ToString().Trim();
                string empleado = TxEmpleado.Text.Trim();
                string nota = string.IsNullOrEmpty(TxNota.Text) ? " ": TxNota.Text;
                int Cbcontingencia = ChContingencia.IsChecked == true ? 1 : 0;
                int CbVisitante = ChVisitante.IsChecked == true ? 1 : 0;


                #region valida que empleado no tenga un estado de ocupacion activa

                StringBuilder sbsql = new StringBuilder();
                sbsql.Append(" select doc.idrow,doc.EmpleadoCodigo,doc.EstadoCamaCodigo,doc.ViviendaCodigo,doc.HabitacionCodigo,doc.CamaCodigo from CtDoc_Viviendas as doc ");
                sbsql.Append(" inner join CtMae_Empleados as emp on emp.EmpleadoCodigo = doc.EmpleadoCodigo  ");
                sbsql.Append(" where ('" + fingreso + "' between doc.FechaIngreso and dateadd(day,-1,FechaSalida) ");
                sbsql.Append(" or '" + fsalida + "' between doc.FechaIngreso and dateadd(day,-1,FechaSalida))  ");
                sbsql.Append(" and doc.ViviendaCodigo = '" + vivienda + "' AND doc.HabitacionCodigo = '" + habitacion + "' ");
                sbsql.Append(" and doc.CamaCodigo = '" + cama + "' ");
                //Console.WriteLine("MIERDA:" + sbsql.ToString());

                var DtValidate = SiaApi.LoadDatatableAsync(sbsql.ToString(), SiaApi.cnemp);
                await DtValidate;

                if (DtValidate.IsCompleted)
                {
                    if (DtValidate.Result.Rows.Count > 0)
                    {
                        string codigo = DtValidate.Result.Rows[0]["EstadoCamaCodigo"].ToString().Trim();
                        if (codigo != "09")
                        {
                            string EmpleadoCodigo = DtValidate.Result.Rows[0]["EmpleadoCodigo"].ToString().Trim();
                            string idrow = DtValidate.Result.Rows[0]["idrow"].ToString().Trim();
                            string ViviendaCodigo = DtValidate.Result.Rows[0]["ViviendaCodigo"].ToString().Trim();
                            string HabitacionCodigo = DtValidate.Result.Rows[0]["HabitacionCodigo"].ToString().Trim();
                            string CamaCodigo = DtValidate.Result.Rows[0]["CamaCodigo"].ToString().Trim();

                            StringBuilder evento = new StringBuilder();
                            evento.Append("Empleado ya tiene registro de Ocupacion/Salida en la fecha seleccionada Empleado:" + EmpleadoCodigo + Environment.NewLine);
                            evento.Append("IdDocumento: " + idrow + " " + Environment.NewLine + " Vivienda:" + ViviendaCodigo + Environment.NewLine);
                            evento.Append("Habitacion:" + HabitacionCodigo + Environment.NewLine + "Cama:" + CamaCodigo);

                            await DisplayAlert("Ya existe registro en esta vivienda", evento.ToString(), "OK");
                            EfectButton(false);
                            return;
                        }
                    }
                }
                #endregion


                #region valida que empleado no tenga un estado de salida en el rango de fechas digitadas

                StringBuilder sqlemp = new StringBuilder();

                sqlemp.Append("select idrow,EmpleadoCodigo,ViviendaCodigo, HabitacionCodigo, CamaCodigo, FechaIngreso, FechaSalida, EstadoCamaCodigo ");
                sqlemp.Append("from CtDoc_Viviendas ");
                sqlemp.Append("where EmpleadoCodigo = '" + empleado + "' and EstadoCamaCodigo = '03' ");
                sqlemp.Append("and (('" + fingreso + "' between FechaIngreso and dateadd(day, -1, fechasalida) or '" + fsalida + "' between FechaIngreso and dateadd(day, -1, fechasalida)) ");
                sqlemp.Append("or (FechaIngreso between '" + fingreso + "' and '" + fsalida + "'  or dateadd(day,-1,FechaSalida) between '" + fingreso + "' and '" + fsalida + "'))");

                var DtValidateEmp = SiaApi.LoadDatatableAsync(sqlemp.ToString(), SiaApi.cnemp);
                await DtValidateEmp;

                if (DtValidateEmp.IsCompleted)
                {
                    if (DtValidateEmp.Result.Rows.Count > 0)
                    {
                        string EmpleadoCodigo = DtValidateEmp.Result.Rows[0]["EmpleadoCodigo"].ToString().Trim();
                        string idrow = DtValidateEmp.Result.Rows[0]["idrow"].ToString().Trim();
                        string ViviendaCodigo = DtValidateEmp.Result.Rows[0]["ViviendaCodigo"].ToString().Trim();
                        string HabitacionCodigo = DtValidateEmp.Result.Rows[0]["HabitacionCodigo"].ToString().Trim();
                        string CamaCodigo = DtValidateEmp.Result.Rows[0]["CamaCodigo"].ToString().Trim();
                        string FechaIngreso = DtValidateEmp.Result.Rows[0]["FechaIngreso"].ToString().Trim();
                        string FechaSalida = DtValidateEmp.Result.Rows[0]["FechaSalida"].ToString().Trim();

                        StringBuilder evento = new StringBuilder();
                        evento.Append("Empleado tiene un estado de Salida" + Environment.NewLine);
                        evento.Append("Empleado :" + EmpleadoCodigo + Environment.NewLine);
                        evento.Append("IdDocumento: " + idrow + " " + Environment.NewLine);
                        evento.Append("Vivienda:" + ViviendaCodigo + Environment.NewLine);
                        evento.Append("Habitacion:" + HabitacionCodigo + Environment.NewLine);
                        evento.Append("Cama:" + CamaCodigo);
                        evento.Append("Fecha Ingreso:" + FechaIngreso + Environment.NewLine);
                        evento.Append("Fecha Salida:" + FechaSalida + Environment.NewLine);

                        await DisplayAlert("Empleado con estado de Salida", evento.ToString(), "OK");
                        EfectButton(false);
                        return;
                    }

                }


                #endregion


                #endregion

                StringBuilder message = new StringBuilder();
                message.Append("Usted desea guardar el documento" + Environment.NewLine);
                message.Append("Empleado:" + empleado + Environment.NewLine);
                message.Append("Fecha Inicial:" + fingreso + Environment.NewLine);
                message.Append("Fecha Final:" + fsalida + Environment.NewLine);

                var result = await Application.Current.MainPage.DisplayAlert("Alert!", message.ToString(), "Si", "No");

                if (Convert.ToBoolean(result))
                {

                    using (SqlConnection connection = new SqlConnection(SiaApi.cnemp))
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {

                            StringBuilder query = new StringBuilder();
                            query.Append("INSERT INTO CtDoc_Viviendas ");
                            query.Append("(ViviendaCodigo,HabitacionCodigo,CamaCodigo,EstadoCamaCodigo,EmpleadoCodigo,EmpresaCodigo,CCostoCodigo,FechaRegistroIngreso,FechaIngreso,FechaRegistroSalida,FechaSalida,");
                            query.Append("IndVisitante,TipoDeRegistro,IndContingencia,Nota,cod_usu,TurnoCodigo) ");
                            query.Append(" VALUES ");
                            query.Append("(@ViviendaCodigo,@HabitacionCodigo,@CamaCodigo,@EstadoCamaCodigo,@EmpleadoCodigo,@EmpresaCodigo,@CCostoCodigo,@FechaRegistroIngreso,@FechaIngreso,@FechaRegistroSalida,@FechaSalida,");
                            query.Append("@IndVisitante,@TipoDeRegistro,@IndContingencia,@Nota,@cod_usu,@TurnoCodigo);");

                            cmd.CommandText = query.ToString();

                            cmd.Parameters.AddWithValue("@ViviendaCodigo", vivienda);
                            cmd.Parameters.AddWithValue("@HabitacionCodigo", habitacion);
                            cmd.Parameters.AddWithValue("@CamaCodigo", cama);
                            cmd.Parameters.AddWithValue("@EstadoCamaCodigo", concepto);
                            cmd.Parameters.AddWithValue("@EmpleadoCodigo", empleado);
                            cmd.Parameters.AddWithValue("@EmpresaCodigo", empresa);
                            cmd.Parameters.AddWithValue("@CCostoCodigo", cco);
                            cmd.Parameters.AddWithValue("@FechaRegistroIngreso", fi);
                            cmd.Parameters.AddWithValue("@FechaIngreso", fingreso);
                            cmd.Parameters.AddWithValue("@FechaRegistroSalida", fs);
                            cmd.Parameters.AddWithValue("@FechaSalida", fsalida);
                            cmd.Parameters.AddWithValue("@IndVisitante", CbVisitante);
                            cmd.Parameters.AddWithValue("@TipoDeRegistro", 0);
                            cmd.Parameters.AddWithValue("@IndContingencia", Cbcontingencia);
                            cmd.Parameters.AddWithValue("@Nota", nota);
                            cmd.Parameters.AddWithValue("@cod_usu", App.Alias);
                            cmd.Parameters.AddWithValue("@TurnoCodigo", "01");
                            await connection.OpenAsync();
                            SqlTransaction transaction = connection.BeginTransaction("TransactionDocVivienda");
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;


                            int exe = await cmd.ExecuteNonQueryAsync();
                            transaction.Commit();
                            if (exe > 0)
                            {
                                await DisplayAlert("Asignacion Vivienda", "Asignacion exitosa..", "OK");
                            }
                            else
                            {
                                await DisplayAlert("alerta", "no se registro ningun movimiento", "OK");
                            }

                            connection.Close();
                        }
                    }
                    
                }


                EfectButton(false);

            }
            catch (Exception w)
            {
                Console.WriteLine("ERRRRRRRRRROR BtnValidar_Clicked: " + w);
            }
        }


        public void EfectButton(bool flag)
        {
            if (flag)
            {
                TxBtn.Text = "Loading...";
                SfBusyIndicator.IsVisible = true;
                SfBusyIndicator.IsBusy = true;

                //foreach (var item in GridMain.Children)
                //{
                //    if (item is SfTextInputLayout) item.IsEnabled = false;                    
                //}
            }
            else
            {
                TxBtn.Text = "Guardar";
                SfBusyIndicator.IsVisible = false;
                SfBusyIndicator.IsBusy = false;

            }
        }


    }
}