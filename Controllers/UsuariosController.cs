using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace apiOXXO.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    [HttpGet("CheckUsrPass/{Usr_ID}/{Usr_Pass}")]
    public Usuarios CheckUsrId_Password(string Usr_ID, string Usr_Pass)
    {
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";
        Usuarios usuario = new Usuarios();
        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM USUARIOS WHERE correo = \"{Usr_ID}\" AND contrasena = \"{Usr_Pass}\"", conexion);
        cmd.Connection = conexion;

        
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                usuario.correo = reader["correo"].ToString();
                usuario.contrasena = reader["contrasena"].ToString();
                usuario.id_usuario = Convert.ToInt32(reader["id_usuario"]);
                usuario.nombre = reader["nombre"].ToString();
                usuario.apellido = reader["apellido"].ToString();
                usuario.monedas = Convert.ToInt32(reader["monedas"]);
                usuario.experiencia = Convert.ToInt32(reader["experiencia"]);
                usuario.puntos = Convert.ToInt32(reader["puntos"]);
                usuario.id_rol = Convert.ToInt32(reader["id_rol"]);
                usuario.id_tienda = Convert.ToInt32(reader["id_tienda"]);
            }
        }

        conexion.Close();
        return usuario;
    }    
}

