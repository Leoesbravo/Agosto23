using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PLMVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]//mostrar una vista o al un recurso
        public ActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAllEF();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpGet] //MOSTRAR4
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Estado = new ML.Estado();
            usuario.Direccion.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();


            if (IdUsuario != null) //UPdate
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                   
                }
            }
            else //Add
            {
                usuario.Direccion.Estado.Pais.Paises = resultPais.Objects; //pase mi lista de paises
                usuario.Rol.Roles = resultRol.Objects;
            }
            return View(usuario);
        }
        [HttpPost] 
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0) //ADD
            {
                ML.Result result = BL.Usuario.AddEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha completado el registro";
                }
                else
                {
                    ViewBag.Mensaje = "Error" + result.ErrorMessage;
                }
            }
            else //UPDATE
            {
                ML.Result result = BL.Usuario.AddEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha completado la actulización";
                }
                else
                {
                    ViewBag.Mensaje = "Error" + result.ErrorMessage;   
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
                ML.Result result = BL.Usuario.AddEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha completado el borrar";
                }
                else
                {
                    ViewBag.Mensaje = "Error" + result.ErrorMessage;
                }
            return PartialView("Modal");
        }
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
    }
}
