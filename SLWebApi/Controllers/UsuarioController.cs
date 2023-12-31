﻿using System.Net;
using System.Web.Http;
using System.Web.Routing;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [Route("{nombre?}/{apellidopaterno?}")]
        [HttpGet]
        public IHttpActionResult GetAll(string nombre, string apellidopaterno)
        {
            if (nombre == null) nombre = "";
            if (apellidopaterno == null) apellidopaterno = "";
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = nombre;
            usuario.ApellidoPaterno = apellidopaterno;
            
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}")]
        [HttpGet]
        public IHttpActionResult GetById(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idUsuario}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetById(idUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idUsuario}")]
        [HttpPut]
        //Diferencia entre URL y URI
        //Investigar metodos Asincronos y sincronos
        public IHttpActionResult Update(int idUsuario, [FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = idUsuario;
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
