﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace PLMVC.Controllers
{
    public class UsuarioController : Controller
    {
        //public ActionResult GetAll()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Nombre = "";
        //    usuario.ApellidoPaterno = "";
        //    ML.Result result = BL.Usuario.GetAllEF(usuario);         
        //    usuario.Usuarios = result.Objects;
        //    return View(usuario);
        //}
        //public ActionResult GetAll()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Nombre = "";
        //    usuario.ApellidoPaterno = "";
        //    ServiceReferenceUser.UsuarioServiceClient usuarioWCF = new ServiceReferenceUser.UsuarioServiceClient();
        //    //lamando al servicio
        //    var result = usuarioWCF.GetAll(usuario);

        //    if (result.Correct)
        //    {
        //        usuario.Usuarios = result.Objects.ToList();
        //    }
        //    return View(usuario);
        //}
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            //lamando al servicio
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync($"usuario?nombre=&apellidopaterno=");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultUsuario in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultUsuario.ToString());
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            if (usuario.Nombre == null)
            {
                usuario.Nombre = "";
            }
            if (usuario.ApellidoPaterno == null)
            {
                usuario.ApellidoPaterno = "";
            }

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            usuario = new ML.Usuario();
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpGet]
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTask = client.GetAsync("usuario/" + IdUsuario);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        usuario = resultItemList;
                    }
                    usuario.Direccion.Estado.Pais.Paises = resultPais.Objects; 
                    usuario.Rol.Roles = resultRol.Objects;
                }
                return View(usuario);
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
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Imagen"];
                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirABase64(file);
                }
                if (usuario.IdUsuario == 0 || usuario.IdUsuario == null) //ADD
                {
                    //WCF
                    ServiceReferenceUser.UsuarioServiceClient usuarioWCF = new ServiceReferenceUser.UsuarioServiceClient();
                    //WCF
                    var result = usuarioWCF.Add(usuario);

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
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Estado.Pais.Paises = resultPais.Objects;
                //usuario.Direccion.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Estado.Pais.IdPais).Objects;
                return View(usuario);
            }
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
        public string ConvertirABase64(HttpPostedFileBase Foto)
        {
            //
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            string imagen = Convert.ToBase64String(data);
            return imagen;
        }

        public JsonResult ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(IdUsuario, Status);
            return Json(null);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            ML.Result result = BL.Usuario.GetByEmail(email);
            if (result.Correct)
            {
                //unboxing
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (password == usuario.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Login = true;
                    ViewBag.Mensaje = "Contraseña Incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                //no existe la cuenta
                ViewBag.Login = true;
                ViewBag.Mensaje = "No existe la cuenta ingresada";
                return PartialView("Modal");

            }
        }
    }
}


//REST 
//diferencias con SOAP
//como sea crea un servicio rest en .NET
//estructura de un archivo JSON
//Codigos de estado HTTP
//100 
//200 
//300
//400 
//500