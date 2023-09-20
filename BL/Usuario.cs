using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;

namespace BL
{

    public class Usuario
    {
        public static string Add(ML.Usuario usuario)
        {
            string resultado = "";
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {

                    SqlCommand cmd = new SqlCommand("UsuarioAdd", context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[6];

                    collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
                    collection[3].Value = usuario.FechaNacimiento;
                    collection[4] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    collection[4].Value = usuario.Direccion;
                    collection[5] = new SqlParameter("@IdRol", SqlDbType.Int);
                    collection[5].Value = usuario.Rol.IdRol;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = "registro exitoso";
                    }
                    else
                    {
                        resultado = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Direccion FROM Usuario";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsurio = new DataTable();

                    adapter.Fill(tablaUsurio);

                    if (tablaUsurio.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tablaUsurio.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.Nombre = row[6].ToString();
                            usuario.Nombre = row[1].ToString();

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Direccion FROM Usuario WHERE IdUsuario = @IdUsuario";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = IdUsuario;

                    cmd.Parameters.AddRange(collection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsurio = new DataTable();

                    adapter.Fill(tablaUsurio);

                    if (tablaUsurio.Rows.Count > 0)
                    {
                        DataRow row = tablaUsurio.Rows[0];

                        ML.Usuario usuarioResult = new ML.Usuario();
                        usuarioResult.IdUsuario = int.Parse(row[0].ToString());
                        usuarioResult.Nombre = row[1].ToString();

                        //boxing
                        result.Object = usuarioResult;

                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetByIdEF(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var registro = context.UsuarioGetById(IdUsuario).FirstOrDefault();
                    if (registro != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = registro.IdUsuario;
                        usuario.Nombre = registro.Nombre;
                        usuario.ApellidoPaterno = registro.ApellidoMaterno;
                        usuario.ApellidoPaterno = registro.ApellidoMaterno;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = registro.IdRol.Value;
                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    //string query = "SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Direccion FROM Usuario";
                    string query = "UsuarioGetAll";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsurio = new DataTable();

                    adapter.Fill(tablaUsurio);

                    if (tablaUsurio.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tablaUsurio.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = int.Parse(row[6].ToString());

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            //Modificacion jose
            var x = 0;
            ML.Result result = new ML.Result();
            try
            {
                //modificacion de prueba
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.FechaNacimiento, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, filasAfectadas,mensaje, usuario.Imagen);


                    if ((int)filasAfectadas.Value == 2)
                    //if (query == 2)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAllEF( ML.Usuario usuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var query = context.UsuarioGetAll(usuario.Nombre, usuario.ApellidoPaterno).ToList();
                    if (query != null)
                    {

                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Usuario usuarioObj = new ML.Usuario();
                            usuarioObj.IdUsuario = registro.IdUsuario;
                            usuarioObj.Nombre = registro.Nombre;
                            usuarioObj.ApellidoPaterno = registro.ApellidoPaterno;
                            usuarioObj.Rol = new ML.Rol();
                            usuarioObj.Rol.IdRol = registro.IdRol;
                            usuarioObj.Rol.Nombre = registro.NombreRol;
                            usuarioObj.Imagen = registro.Imagen;
                            usuarioObj.Status = registro.status.Value;
                            result.Objects.Add(usuarioObj);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByEmail(string email)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var query = context.UsuarioGetByEmail(email).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        //boxing
                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    DLEF.Usuario usuarioEntity = new DLEF.Usuario();

                    usuarioEntity.Nombre = usuario.Nombre;
                    usuarioEntity.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioEntity.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioEntity.IdRol = usuario.Rol.IdRol;

                    context.Usuarios.Add(usuarioEntity);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAllLINQ()
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var query = (from Users in context.Usuarios
                                 join Rols in context.Rols on Users.IdRol equals Rols.IdRol
                                 select new
                                 {
                                     IdUsuario = Users.IdUsuario,
                                     Nombre = Users.Nombre,
                                     ApellidoPaterno = Users.ApellidoPaterno,
                                     ApellidoMaterno = Users.ApellidoMaterno,
                                     IdRol = Users.IdRol,
                                     NombreRol = Rols.Nombre
                                 });

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.Nombre = registro.Nombre;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = registro.IdRol.Value;
                            usuario.Rol.Nombre = registro.NombreRol;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result ChangeStatus(int IdUsuario, bool Status)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DLEF.LEscogidoProgramacionNCapasAgosto2023Entities context = new DLEF.LEscogidoProgramacionNCapasAgosto2023Entities())
                {
                    var rowsAffetted = context.UsuarioChangeStatus(IdUsuario, Status);

                    if (rowsAffetted > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result LeerExcel(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>(); //almacena los registros incompletos
                int i = 1; //guarda el numero de la fila
                foreach (ML.Usuario usuario in usuarios)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido materno  ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido paterno  ";
                    }

                    if (error.Mensaje != null) //si tuvo algun error
                    {
                        result.Objects.Add(error); // agregar a la lista de errores
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
    }
}
