using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace SLWCF
{
     [ServiceContract]// que la interfaz es parte de un servicio WCF
    public interface IServiceOperaciones
    {
        [OperationContract] //el metodo es parte del servicio WCF
        void DoWork();
        [OperationContract]
        int Sumar(int a, int b);
    }
}
