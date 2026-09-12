using System.IO;
using System.Text.Json;

namespace Modelos
{

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
