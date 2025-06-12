using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiOXXO.Controllers;


[Route("[controller]")]

public class NivelesController : ControllerBase
{
    [HttpGet("GetNivelimg/{nivel}")]
    public Niveles GetNivelesimg(int nivel)
    {
        Niveles resnivel = new Niveles();
        string ConnectionString = "Server=mysql-370e2f78-tec-f8a4.b.aivencloud.com;Port=27566;Database=reto_oxxo;Uid=avnadmin;Password=AVNS_2M0v78xMV-H3ZdoHrdY;SslMode=Required;";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("SELECT liga_nivel, nivel FROM niveles WHERE nivel = @nivel", conexion);
        cmd.Parameters.AddWithValue("@nivel", nivel);
        cmd.Prepare();

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {

                resnivel.liga_nivel = reader["liga_nivel"].ToString();
                resnivel.nivel = Convert.ToInt32(reader["nivel"]);
            }
        }

        return resnivel;
    }   
}