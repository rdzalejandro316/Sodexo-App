using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SodexoApp.Helpers
{
    class CustomGridStyle : DataGridStyle
    {
        public CustomGridStyle()
        {
        }

        public override Color GetBorderColor()
        {
            return Color.Gray;
        }

        public override Color GetHeaderForegroundColor()
        {
            return Color.White;
        }

        public override Color GetHeaderBackgroundColor()
        {
            return Color.FromHex("373737");
        }
        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.Horizontal;
        }

        public override Color GetSelectionBackgroundColor()
        {
            return Color.FromHex("7F2A295C");
        }


    }
}
