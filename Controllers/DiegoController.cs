using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace apiOXXO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiegoSaldanaController : ControllerBase
    {
        public string ConnectionString = "Server=127.0.0.1;Port=3306;Database=reto_oxxo;Uid=root;";


        [HttpGet("getHorarios")] // ENDPOINT GET: Los horarios que el lider de tienda tiene que manejar
        public IEnumerable<Turnos> getHorarios(DateTime semana, int id_usuario)
        {
            List<Turnos> myListaTurnos = new List<Turnos>();
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getHorarios";
            cmd.Parameters.AddWithValue("@hourDate", semana); // Parametro que le pusimos en el procedure
            cmd.Parameters.AddWithValue("@userId", id_usuario);
            cmd.Connection = conexion;

            Turnos miTurno = new Turnos();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    miTurno = new Turnos();
                    miTurno.empleado = reader["empleado"].ToString();
                    miTurno.horario = reader["horario"].ToString();
                    miTurno.lunes = Convert.ToBoolean(reader["lunes"]);
                    miTurno.martes = Convert.ToBoolean(reader["martes"]);
                    miTurno.miercoles = Convert.ToBoolean(reader["miercoles"]);
                    miTurno.jueves = Convert.ToBoolean(reader["jueves"]);
                    miTurno.viernes = Convert.ToBoolean(reader["viernes"]);
                    miTurno.sabado = Convert.ToBoolean(reader["sabado"]);
                    miTurno.domingo = Convert.ToBoolean(reader["domingo"]);
                    myListaTurnos.Add(miTurno);
                }
            }
            conexion.Close();


            return myListaTurnos;
        }

        [HttpPut("{id_usuario}/{empleado}/{semana}")] // ENDPOINT PUT: Actualiza cambios que el lider de tienda haga
        public IActionResult updateHorario(int id_usuario, string empleado, DateTime semana, [FromBody] Turnos upToDate)
        {
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;

            cmd.CommandText = "updateHorarios";
            cmd.Parameters.AddWithValue("@hourDate", semana);
            cmd.Parameters.AddWithValue("@userId", id_usuario);
            cmd.Parameters.AddWithValue("@empleadoAct", empleado);
            cmd.Parameters.AddWithValue("@newLun", upToDate.lunes);
            cmd.Parameters.AddWithValue("@newMar", upToDate.martes);
            cmd.Parameters.AddWithValue("@newMie", upToDate.miercoles);
            cmd.Parameters.AddWithValue("@newJue", upToDate.jueves);
            cmd.Parameters.AddWithValue("@newVie", upToDate.viernes);
            cmd.Parameters.AddWithValue("@newSab", upToDate.sabado);
            cmd.Parameters.AddWithValue("@newDom", upToDate.domingo);

            cmd.ExecuteNonQuery();

            conexion.Close();

            return Ok();
        }

        [HttpGet("GetSemanasDisponibles")]
        public List<DateTime> GetSemanasDisponibles(int id_usuario)
        {
            List<DateTime> semanas = new List<DateTime>();
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("getSemanasPorUsuario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId", id_usuario);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                semanas.Add(reader.GetDateTime("semana"));
            }
            return semanas;
        }

    }
}

 