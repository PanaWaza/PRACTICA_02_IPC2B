using System.IO;
using System.Text.Json;

namespace Modelos
{
    // Se apoya en System.Text.Json solo para *parsear* el texto (leer los
    // campos), pero nunca guarda las canciones en un List<T> ni en un arreglo:
    // cada canción leída se inserta directamente en la Cola y en el Árbol.
    public static class CargadorJson
    {
        public static void CargarCanciones(string rutaArchivo, ColaReproduccion cola, ArbolBinario arbol)
        {
            string contenido = File.ReadAllText(rutaArchivo);

            using JsonDocument documento = JsonDocument.Parse(contenido);
            foreach (JsonElement elemento in documento.RootElement.EnumerateArray())
            {
                Cancion cancion = new Cancion
                {
                    Titulo = elemento.GetProperty("titulo").GetString(),
                    Artista = elemento.GetProperty("artista").GetString(),
                    Genero = elemento.GetProperty("genero").GetString(),
                    Duracion = elemento.GetProperty("duracion").GetDouble()
                };

                cola.Encolar(cancion);
                arbol.Insertar(cancion);
            }
        }
    }
}
