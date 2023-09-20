using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SLWCF
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Correct { get; set; } //decir si funciono o no
        [DataMember]
        public string ErrorMessage { get; set; } //almacena un mensaje de error 
        [DataMember]
        public Exception Ex { get; set; } //almacena la exepcion completa
        [DataMember]
        public List<object> Objects { get; set; } //lista, permite almacenar muchos objetos
        [DataMember]
        public object Object { get; set; } //permite almacenar un objeto 
    }
}