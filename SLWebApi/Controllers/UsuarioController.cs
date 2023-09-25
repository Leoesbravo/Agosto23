using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Usuario usuario)
        {
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

        [Route("/{idUsuario}")]
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

        [Route("/{idUsuario}")]
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
