using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void  Add()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingresa el nombre del nuevo usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingresa el apellido del nuevo usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el apellido materno del nuevo usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa la fecha de nacimiento del nuevo usuario");
            usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa la direccion del nuevo usuario");
            //usuario.Direccion = Console.ReadLine();
            Console.WriteLine("Ingrese el rol del nuevo usuario");
            //cuando voy a utilizar la propiedad por primera vez
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());

            string resultado = BL.Usuario.Add(usuario);
            Console.WriteLine(resultado);

        }
        public static void Update()
        {
            Console.WriteLine("ingresa el id del rol del usuario a editar");
            Console.WriteLine("ingresa el nuevo nombre del usuario");
        }
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if(result.Correct == true)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("ID: " + usuario.IdUsuario);
                    Console.WriteLine("Nombre: " + usuario.Nombre);
                    Console.WriteLine("ID Rol: " + usuario.Rol.IdRol);
                    Console.WriteLine("Rol: " + usuario.Rol.Nombre);
                    Console.WriteLine("--------------------------");
                }
            }
            else
            {
                Console.WriteLine("Error" + result.ErrorMessage);
            }
        }
        public static void GetById()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el id del Usuario a consultar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            ML.Result result = BL.Usuario.GetById(usuario.IdUsuario.Value);
            if(result.Correct == true)
            {
                //unboxing
                usuario = (ML.Usuario)result.Object;
                Console.WriteLine("ID: " +usuario.IdUsuario);
                Console.WriteLine("Nombre: " +usuario.Nombre);
            }
            else
            {
                Console.WriteLine("error" + result.ErrorMessage);
            }
        }

        public static void CargaMasivaTxt()
        {
            string file = @"C:\Users\digis\source\repos\Agosto23\PLMVC\Files\CargaMasivaUsuario.txt";

            if (File.Exists(file))
            {
                StreamReader streamReader = new StreamReader(file);

                string line = streamReader.ReadLine(); //SALTAR HEDEARS

                while ( (line = streamReader.ReadLine()) != null)
                {
                    string[] row = line.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = row[0].ToString();
                    usuario.ApellidoPaterno = row[1];

                    //BL.Usuario.AddEF(usuario);

                }

            }
        }
    }
}
