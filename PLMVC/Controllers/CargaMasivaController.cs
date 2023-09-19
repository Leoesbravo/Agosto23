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

            if (Session["pathExcel"] == null)
            {
                if (file != null)
                {

                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower(); //obteniendo la extension
                    string extesionValida = ConfigurationManager.AppSettings["TipoExcel"];

                    if (extensionArchivo == extesionValida)
                    {
                        string rutaproyecto = Server.MapPath("~/CargaMasiva/");
                        string filePath = rutaproyecto + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {

                            file.SaveAs(filePath);



                            //Session -C#

                            //Objeto    Vive hasta el fin de ejecucion
                            //SERVIDOR
                            //Guardar cualquier dato


                            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath;
                            ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    Session["pathExcel"] = filePath;
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
            else
            {
                string filepath = Session["pathExcel"].ToString();

                if( filepath != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filepath;
                    ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                    if (resultUsuarios.Correct)
                    {
                        ML.Result resultErrores = new ML.Result();
                        resultErrores.Objects = new List<object>();
                        foreach (ML.Usuario usuario in resultUsuarios.Objects)
                        {
                            ML.Result result1 = BL.Usuario.AddEF(usuario);
                            if (!result1.Correct)
                            {
                                string error = "Ocurrio un error al insertar el usuario con correo: " + usuario.Nombre + "el error fue" + result1.ErrorMessage;
                                resultErrores.Objects.Add(error);
                                
                            }
                            Session["pathExcel"] = null;
                        }
                        if(resultErrores.Objects.Count > 0)  
                        {
                            string pathTxt = Server.MapPath(@"~\Files\logErrores.txt");
                            using(StreamWriter writter = new StreamWriter(pathTxt))
                            {
                                foreach (string linea in resultErrores.Objects)
                                {
                                    writter.WriteLine(linea);
                                }
                            }
                        }
                    }

                }
                else
                {

                }


            }
            return View(result);
        }
    }
}