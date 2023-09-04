using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int idPais)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var query = context.EstadoGetByIdPais(idPais).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.Nombre = obj.Nombre;
                            estado.IdEstado = obj.IdEstado;
                            result.Objects.Add(estado);
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
