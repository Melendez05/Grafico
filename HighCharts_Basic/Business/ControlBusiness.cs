using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace HighCharts_Basic.Business
{
    public class ControlBusiness
    {
        private string connectionString;
        private HighCharts_Basic.Data.ControlData control;

        public ControlBusiness(string conn)
        {
            this.connectionString = conn;
            control = new HighCharts_Basic.Data.ControlData(this.connectionString);
        }

        public string getInformationControl()
        {
            return control.getInformationControl();
            //return control.informacionGrafico();
        }

    }
}