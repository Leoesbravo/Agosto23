using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DL
{
    public class Conexion
    {
        //traernos la cadena de conexion
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LEscogidoProgramacionNCapas"].ConnectionString.ToString();
        }
    }
}
