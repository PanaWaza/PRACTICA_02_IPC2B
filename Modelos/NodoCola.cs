namespace Modelos
{
    public class NodoCola
    {
        public Cancion Cancion { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(Cancion cancion)
        {
            Cancion = cancion;
            Siguiente = null;
        }
    }
}
