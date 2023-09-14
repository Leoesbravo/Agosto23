using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult Cargar()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        [HttpPost]
        public ActionResult Cargar(ML.Result result) 
        {
            HttpPostedFileBase file = Request.Files["Excel"];
            if (file != null)
            {

                string extensionArchivo = Path.GetExtension(file.FileName).ToLower(); //obteniendo la extension
                string extesionValida =  ConfigurationManager.AppSettings["TipoExcel"];       

                if (extensionArchivo == extesionValida)
                {
                    string filePath = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    if (!System.IO.File.Exists(filePath))
                    {

                        file.SaveAs(filePath);

                        //Session -C#
                        string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath;
                        ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                        if (resultUsuarios.Correct)
                        {
                            ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);
                            if (resultValidacion.Objects.Count == 0)
                            {
                               // resultValidacion.Correct = true;
                               // HttpContext.Session.SetString("PathArchivo", filePath);
                            }

                            return View(resultValidacion);
                        }
                        else
                        {
                            ViewBag.Message = "El excel no contiene registros";
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                }
            }
            else
            {
                ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
            }
            return View(result);
        }
    }
}