using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UsuarioService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UsuarioService.svc or UsuarioService.svc.cs at the Solution Explorer and start debugging.
    public class UsuarioService : IUsuarioService
    {
        public void DoWork()
        {
        }
        public Result GetAll(Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            SLWCF.Result resultWCF = new SLWCF.Result();
            resultWCF.Correct = result.Correct;
            resultWCF.ErrorMessage = result.ErrorMessage;
            resultWCF.Object = result.Object;
            resultWCF.Objects = result.Objects;
            resultWCF.Ex = result.Ex;

            return resultWCF;
        }
        public SLWCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            //SLWCF.Result resultwcf = new SLWCF.Result();
            //resultwcf.Correct = result.Correct;
            //return resultwcf;
            return new SLWCF.Result
            {
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex
            };

            //KnowType
        }
    }
}
