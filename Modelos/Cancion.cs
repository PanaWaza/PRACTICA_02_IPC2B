namespace Modelos
{
    public class Cancion
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string Genero { get; set; }
        public double Duracion { get; set; } // minutos

        public override string ToString()
        {
            return $"{Titulo} - {Artista} ({Genero}) : {Duracion}min";
        }
    }
}
