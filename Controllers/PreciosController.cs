using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiOXXO.Controllers;

[Route("[controller]")]

public class PreciosController : ControllerBase
{
    [HttpGet("GetPrecio/{nombre_articulo}")]
    public Precios GetPrecio(string nombre_articulo)
    {
        Precios precio = new Precios();

        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("SELECT * FROM precios WHERE nombre_articulo = @nombre_articulo", conexion);
        cmd.Parameters.AddWithValue("@nombre_articulo", nombre_articulo);

        cmd.Prepare();

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                precio.id_precio = Convert.ToInt32(reader["id_precio"]);
                precio.nombre_articulo = reader["nombre_articulo"].ToString();
                precio.precio_articulo = Convert.ToInt32(reader["precio_articulo"]);
            }
        }

        return precio;
    }   
}