using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighCharts_Basic.Domain
{
    public class Control
    {

        private string fecha_inicio;       
        private string fecha_final;
        private string periodo;

       



        public Control(string fecha_inicio, string fecha_final, string periodo)
        {
            this.fecha_inicio = fecha_inicio;
            this.fecha_final = fecha_final;
            this.periodo = periodo;

        }

        public string Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }


        public string Fecha_final
        {
            get { return fecha_final; }
            set { fecha_final = value; }
        }

        public string Fecha_inicio
        {
            get { return fecha_inicio; }
            set { fecha_inicio = value; }
        }

        

        



        
    }
}