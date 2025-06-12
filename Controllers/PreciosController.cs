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

        string ConnectionString = "Server=mysql-370e2f78-tec-f8a4.b.aivencloud.com;Port=27566;Database=reto_oxxo;Uid=avnadmin;Password=AVNS_2M0v78xMV-H3ZdoHrdY;SslMode=Required;";

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