using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceUsuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceUsuario.svc or ServiceUsuario.svc.cs at the Solution Explorer and start debugging.
    public class ServiceUsuario : IServiceUsuario
    {
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
