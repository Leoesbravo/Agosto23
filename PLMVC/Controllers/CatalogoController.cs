using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class CatalogoController : Controller
    {
        // GET: Catalogo
        public ActionResult Catalogo()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        public ActionResult GetDependiente(string numeroEmpleado)
        {
            ML.Result result = BL.Dependiente.GetByNumeroEmpleado(numeroEmpleado);
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Dependientes = result.Objects;
            return View(dependiente);
        }
        public ActionResult GetProductos(int idArea)
        {
            ML.Result result = BL.Producto.GetByIdArea(idArea);
            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;
            return View(producto);
        }
    }
}