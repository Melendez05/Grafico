using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using HighCharts_Basic.Domain;

namespace HighCharts_Basic.Data
{
    public class ControlData
    {
        private string connectionString;

        private int periodo = 3;
        private int mes_actual = DateTime.Now.Month;
        private int anno_actual = DateTime.Now.Year;
        private DateTime fecha_actual = DateTime.Now;
        private int cantidadIncio = 0, cantidad75 = 0, cantidad90 = 0, cantidad100 = 0, cantidadM100 = 0;

        public ControlData(string conn)
        {
            this.connectionString = conn;
        }


        public string getInformationControl()
        {

            //Conexión a la base de datos
            SqlConnection sqlConn = new SqlConnection(this.connectionString);
            //Abrimos la conexión
            sqlConn.Open();
            //Llamamos al procedimiento almacenado
            SqlCommand cmd = new SqlCommand("infoGrafico", sqlConn);
            //Configuramos el tipo
            cmd.CommandType = CommandType.StoredProcedure;
            //Objeto data reader
            SqlDataReader objReader = cmd.ExecuteReader();
            string infor = "";
            while (objReader.Read())
            {
                infor += objReader.GetString(0) + "," + objReader.GetInt32(1) + ";";
            }
            sqlConn.Close();
            return infor;
        }


        public string informacionGrafico()
        {
            //Simular información de los controles
            ArrayList controles = new ArrayList();
            ArrayList mesAsignado = new ArrayList();
            ArrayList periodos = new ArrayList();
            periodos.Add("3");
            periodos.Add("2");
            periodos.Add("1");
            Random rnd = new Random();
            for (int i = 0; i < 32; i++)
            {
                if (i % 2 == 0)
                {
                    mesAsignado.Add(rnd.Next(1, 12));
                    mesAsignado.Add(rnd.Next(1, 12));
                    controles.Add(periodos[0]);
                    controles.Add(periodos[2]);
                }
                else
                {
                    controles.Add(periodos[1]);
                    mesAsignado.Add(rnd.Next(1, 12));
                }
            }
            //--------------------------------------



            for (int j = 0; j < controles.Count; j++)
            {

                if (controles[j].Equals("1"))
                {
                    calculoPeriodosSemanales();

                }
                else if (controles[j].Equals("2"))
                {
                    periodo = 1;
                    if (mes_actual > periodo)
                    {
                        while (periodo < mes_actual)
                        {
                            periodo++;
                        }
                        calculoPeriodosMensuales(periodo, (int)mesAsignado[j]);
                    }
                    else
                    {
                        calculoPeriodosMensuales(periodo, (int)mesAsignado[j]);
                    }
                }
                else if (controles[j].Equals("3"))
                {
                    periodo = 3;
                    if (mes_actual > periodo)
                    {
                        while (periodo < mes_actual)
                        {
                            periodo += 3;
                        }
                        calculoPeriodosTrimestrales(periodo, (int)mesAsignado[j]);
                    }
                    else
                    {
                        calculoPeriodosTrimestrales(periodo, (int)mesAsignado[j]);
                    }
                }

            }

            return "Asignados recientes," + cantidadIncio + ";Estado 75%," + cantidad75 +
                ";Estado 90%," + cantidad90 + ";Estado 100%," +
                cantidad100 + ";Estado +100%," + cantidadM100;
        }

        public void calculoPeriodosTrimestrales(int periodo, int mes_asignado)
        {
            int dias_mes_inicio = DateTime.DaysInMonth(anno_actual, periodo -= 2);
            int dias_mes_medio = DateTime.DaysInMonth(anno_actual, periodo += 1);
            int dias_mes_final = DateTime.DaysInMonth(anno_actual, periodo += 1);
            int total_dias = dias_mes_inicio + dias_mes_medio + dias_mes_final;
            //DateTime fecha_inicio = new DateTime(anno_actual,periodo-2, 1);
            DateTime fecha_inicio;
            TimeSpan ts;
            int mes_inicio_trimestre = periodo -= 2;
            if (mes_asignado < mes_inicio_trimestre)
            {
                fecha_inicio = new DateTime(anno_actual, mes_asignado, 1);
                ts = fecha_actual - fecha_inicio;
            }
            else
            {
                fecha_inicio = new DateTime(anno_actual, periodo -= 2, 1);
                ts = fecha_actual - fecha_inicio;
            }
            int diferencia_dias = ts.Days;
            obtenerCantidades(diferencia_dias, total_dias);
        }

        public void calculoPeriodosMensuales(int periodo, int mes_asignado)
        {

            int total_dias = DateTime.DaysInMonth(anno_actual, periodo);
            //DateTime fecha_inicio = new DateTime(anno_actual,periodo, 1);
            //TimeSpan ts = fecha_actual - fecha_inicio;
            DateTime fecha_inicio;
            TimeSpan ts;
            if (mes_asignado < periodo)
            {
                fecha_inicio = new DateTime(anno_actual, mes_asignado, 1);
                ts = fecha_actual - fecha_inicio;
            }
            else
            {
                fecha_inicio = new DateTime(anno_actual, periodo, 1);
                ts = fecha_actual - fecha_inicio;
            }
            int diferencia_dias = ts.Days;

            obtenerCantidades(diferencia_dias, total_dias);
        }

        public void calculoPeriodosSemanales()
        {

            DateTime.Now.AddDays(-1 * ((int)DateTime.Now.DayOfWeek - 1));
            TimeSpan ts = fecha_actual - DateTime.Now.AddDays(-1 * ((int)DateTime.Now.DayOfWeek - 1));
            int diferencia_dias = ts.Days;
            int total_dias = 5;
            obtenerCantidades(diferencia_dias, total_dias);

        }

        public void obtenerCantidades(int diferencia_dias, int total_dias)
        {
            if (diferencia_dias <= (total_dias * 0.25))
            {
                cantidadIncio++;
            }
            else if (diferencia_dias <= (total_dias * 0.75))
            {
                cantidad75++;
            }
            else if (diferencia_dias <= (total_dias * 0.90))
            {
                cantidad90++;
            }
            else if (diferencia_dias == total_dias)
            {
                cantidad100++;
            }
            else if (diferencia_dias > total_dias)
            {
                cantidadM100++;
            }
            else if (diferencia_dias > (total_dias * 0.90))
            {
                cantidad90++;
            }
        }




    }
}