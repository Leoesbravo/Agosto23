using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsuarioService" in both code and config file together.
    [ServiceContract]
    public interface IUsuarioService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SLWCF.Result GetAll(ML.Usuario usuario);

        [OperationContract]
        SLWCF.Result Add(ML.Usuario usuario);
    }
}
