using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var query = context.PaisGetAll().ToList();
                    if (query != null)
                    {

                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Pais pais = new ML.Pais(registro.IdPais, registro.Nombre);
                            result.Objects.Add(pais);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
