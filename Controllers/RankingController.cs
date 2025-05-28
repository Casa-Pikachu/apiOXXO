using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiOXXO.Controllers;

[ApiController]
[Route("[controller]")]

public class RankingController : ControllerBase {
    [HttpGet("GetFirst")]
    public Ranking GetFirstMini1()
    {
        Ranking first = new Ranking();

        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();

        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM ranking WHERE id_minijuego = 1 ORDER BY puntaje DESC LIMIT 1", conexion);

        using(var reader = cmd.ExecuteReader()){
            if (reader.Read())
            {
                first.id_ranking = Convert.ToInt32(reader["id_ranking"]);
                first.puntaje = Convert.ToInt32(reader["puntaje"]);
                first.fecha_puntaje = reader["fecha_puntaje"].ToString();
                first.id_usuario = Convert.ToInt32(reader["id_usuario"]);
                first.id_minijuego = Convert.ToInt32(reader["id_minijuego"]);   
            }
        }

        return first;
    }
}