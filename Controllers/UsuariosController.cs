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

    [HttpGet("GetTopExperiencia")]
    public List<Usuarios> GetTopExperiencia()
    {
        List<Usuarios> usuarios = new List<Usuarios>();
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("SELECT * FROM USUARIOS ORDER BY experiencia DESC LIMIT 3", conexion);
        cmd.Connection = conexion;

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Usuarios usuario = new Usuarios
                {
                    id_usuario = Convert.ToInt32(reader["id_usuario"]),
                    nombre = reader["nombre"].ToString(),
                    apellido = reader["apellido"].ToString(),
                    correo = reader["correo"].ToString(),
                    monedas = Convert.ToInt32(reader["monedas"]),
                    experiencia = Convert.ToInt32(reader["experiencia"]),
                    puntos = Convert.ToInt32(reader["puntos"]),
                    id_rol = Convert.ToInt32(reader["id_rol"]),
                    id_tienda = Convert.ToInt32(reader["id_tienda"])
                };
                usuarios.Add(usuario);
            }
        }

        conexion.Close();
        return usuarios;
    }

    [HttpPut("UpdateExperiencia/{id_usuario}/{experiencia}")]
    public void UpdateExperiencia(int id_usuario, int experiencia)
    {
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";
        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("UPDATE USUARIOS SET experiencia = @experiencia WHERE id_usuario = @id_usuario", conexion);
        cmd.Parameters.AddWithValue("@experiencia", experiencia);
        cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

        cmd.Prepare();

        cmd.ExecuteNonQuery();
    }

    [HttpPut("UpdatePuntos/{id_usuario}/{puntos}")]
    public void UpdatePuntos(int id_usuario, int puntos)
    {
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";
        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("UPDATE USUARIOS SET puntos = @puntos WHERE id_usuario = @id_usuario", conexion);
        cmd.Parameters.AddWithValue("@puntos", puntos);
        cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

        cmd.Prepare();

        cmd.ExecuteNonQuery();
    }
}

