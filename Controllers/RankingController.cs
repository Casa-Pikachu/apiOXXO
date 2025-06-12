using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiOXXO.Controllers;

[ApiController]
[Route("[controller]")]

public class RankingController : ControllerBase
{
    [HttpGet("GetFirst")]
    public Ranking GetFirstMini2()
    {
        Ranking first = new Ranking();

        string ConnectionString = "Server=mysql-370e2f78-tec-f8a4.b.aivencloud.com;Port=27566;Database=reto_oxxo;Uid=avnadmin;Password=AVNS_2M0v78xMV-H3ZdoHrdY;SslMode=Required;";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();

        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM ranking WHERE id_minijuego = 2 ORDER BY puntaje DESC LIMIT 1", conexion);

        using (var reader = cmd.ExecuteReader())
        {
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

    [HttpPost("PostRanking")]
    public void PostRanking([FromBody] Ranking newRanking)
    {
        string ConnectionString = "Server=mysql-370e2f78-tec-f8a4.b.aivencloud.com;Port=27566;Database=reto_oxxo;Uid=avnadmin;Password=AVNS_2M0v78xMV-H3ZdoHrdY;SslMode=Required;";
        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand("INSERT INTO ranking (puntaje, fecha_puntaje, id_usuario, id_minijuego) VALUES (@puntaje, @fecha_puntaje, @id_usuario, @id_minijuego)", conexion);
        cmd.Parameters.AddWithValue("@puntaje", newRanking.puntaje);
        cmd.Parameters.AddWithValue("@fecha_puntaje", newRanking.fecha_puntaje);
        cmd.Parameters.AddWithValue("@id_usuario", newRanking.id_usuario);
        cmd.Parameters.AddWithValue("@id_minijuego", newRanking.id_minijuego);

        cmd.Prepare();

        cmd.ExecuteNonQuery();

        conexion.Close();
    }   
}