namespace apiOXXO;

public class Turnos
{
    public int id_turnos { get; set; }
    public DateTime semana { get; set; }
    public string? empleado { get; set; }
    public string? horario { get; set; }
    public bool lunes { get; set; }
    public bool martes { get; set; }
    public bool miercoles { get; set; }
    public bool jueves { get; set; }
    public bool viernes { get; set; }
    public bool sabado { get; set; }
    public bool domingo { get; set; }
    public int id_usuario { get; set; }
}