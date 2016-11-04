using HighCharts_Basic.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HighCharts_Basic
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        [WebMethod()]
        public static string getInformationControl() {

            string conn = WebConfigurationManager.ConnectionStrings["ControlConnectionString"].ToString();
            ControlBusiness controlB = new ControlBusiness(conn);
            string info = controlB.getInformationControl();
            return info;
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           DateTime fecha_actual = DateTime.Now;
           DateTime.Now.AddDays(-1 * ((int)DateTime.Now.DayOfWeek - 1));

            DateTime fecha_inicio = new DateTime(2016,10, 31);
            TimeSpan ts = fecha_actual - DateTime.Now.AddDays(-1 * ((int)DateTime.Now.DayOfWeek - 1));
            int diferencia_dias = ts.Days;

            Label1.Text = "" + diferencia_dias;
        }
    }
}