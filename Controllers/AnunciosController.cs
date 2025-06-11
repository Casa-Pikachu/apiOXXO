using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiOXXO.Controllers;

[ApiController]
[Route("[controller]")]
public class AnunciosController : ControllerBase{
    [HttpPost("CreateAnuncio")]
    public void PostAnuncio([FromBody] Anuncios anuncio){
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();
        
        MySqlCommand cmd = new MySqlCommand("INSERT INTO anuncios (contenido, id_usuario, fecha_anuncio) VALUES (@Contenido, @IdUsuario, CURDATE())", conexion);
        cmd.Parameters.AddWithValue("@Contenido", anuncio.contenido);
        cmd.Parameters.AddWithValue("@IdUsuario", anuncio.id_usuario);
        cmd.Prepare();

        cmd.ExecuteNonQuery();
        conexion.Close();
    }

    [HttpGet("GetAnuncios/{id_usuario}")]
    public List<Anuncios> GetAnuncios(int id_usuario){
        List<Anuncios> listaAnuncios = new List<Anuncios>();
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";
        
        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();

        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM anuncios WHERE id_usuario = @IdUsuario", conexion);
        cmd.Parameters.AddWithValue("@IdUsuario", id_usuario);
        cmd.Prepare();

        using (var reader = cmd.ExecuteReader()){
            while (reader.Read()){
                Anuncios anuncio = new Anuncios();
                anuncio.id_anuncio = Convert.ToInt32(reader["id_anuncio"]);
                anuncio.contenido = reader["contenido"].ToString();
                anuncio.fecha_anuncio = reader["fecha_anuncio"].ToString();
                anuncio.id_usuario = Convert.ToInt32(reader["id_usuario"]);

                listaAnuncios.Add(anuncio);
            }
        }

        return listaAnuncios;
    }


    [HttpDelete("DeleteAnuncio/{id_anuncio}")]
    public void DeleteAnuncio(int id_anuncio){
        string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root";

        MySqlConnection conexion = new MySqlConnection(ConnectionString);

        conexion.Open();
        
        MySqlCommand cmd = new MySqlCommand($"DELETE FROM anuncios WHERE id_anuncio = {id_anuncio}", conexion);

        cmd.ExecuteNonQuery();
        conexion.Close();
    }
}
