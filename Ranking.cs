namespace apiOXXO;

public class Ranking{
    public int id_ranking {get;set;}
    public int puntaje {get;set;}
    public string? fecha_puntaje {get;set;}
    public int id_usuario {get;set;}
    public int id_minijuego {get;set;}
        
}

//   `id_ranking` int NOT NULL AUTO_INCREMENT,
//   `puntaje` int NOT NULL,
//   `fecha_puntaje` date NOT NULL,
//   `id_usuario` int NOT NULL,
//   `id_minijuego` int NOT NULL,