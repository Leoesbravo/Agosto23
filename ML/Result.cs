using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; } //decir si funciono o no
        public string ErrorMessage { get; set; } //almacena un mensaje de error 
        public Exception Ex { get; set; } //almacena la exepcion completa
        public List<object> Objects { get; set; } //lista, permite almacenar muchos objetos
        public object Object { get; set; } //permite almacenar un objeto 
    }
}
