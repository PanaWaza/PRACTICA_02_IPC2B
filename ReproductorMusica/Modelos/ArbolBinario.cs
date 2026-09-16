using System;
using Eto.Forms; // en vez de System.Windows.Forms

namespace Modelos
{
    public class ArbolBinario
    {
        private NodoArbol raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        // 1. Insertar una canción en el árbol (ordenada alfabéticamente por Título)
        public void Insertar(Cancion cancion)
        {
            raiz = InsertarRec(raiz, cancion);
        }

        private NodoArbol InsertarRec(NodoArbol nodo, Cancion cancion)
        {
            if (nodo == null)
                return new NodoArbol(cancion);

            int comparacion = string.Compare(cancion.Titulo, nodo.Cancion.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comparacion < 0)
                nodo.Izquierdo = InsertarRec(nodo.Izquierdo, cancion);
            else if (comparacion > 0)
                nodo.Derecho = InsertarRec(nodo.Derecho, cancion);

            return nodo;
        }

        // 2. Buscar una canción por Título
        public Cancion Buscar(string titulo)
        {
            return BuscarRec(raiz, titulo);
        }

        private Cancion BuscarRec(NodoArbol nodo, string titulo)
        {
            if (nodo == null)
                return null;

            int comparacion = string.Compare(titulo, nodo.Cancion.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comparacion == 0)
                return nodo.Cancion;
            else if (comparacion < 0)
                return BuscarRec(nodo.Izquierdo, titulo);
            else
                return BuscarRec(nodo.Derecho, titulo);
        }

        // 3. Renderizar en Interfaz Gráfica (ListBox de Eto.Forms) sin usar List<T> ni arreglos
        public void MostrarEnListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            MostrarEnListBoxRec(raiz, listBox);
        }

        private void MostrarEnListBoxRec(NodoArbol nodo, ListBox listBox)
        {
            if (nodo != null)
            {
                MostrarEnListBoxRec(nodo.Izquierdo, listBox);
                listBox.Items.Add(nodo.Cancion.ToString());
                MostrarEnListBoxRec(nodo.Derecho, listBox);
            }
        }

        // 4. Imprimir por Consola (Útil para pruebas o depuración)
        public void RecorridoInOrden()
        {
            if (raiz == null)
            {
                Console.WriteLine("El árbol está vacío.");
                return;
            }
            RecorridoInOrdenRec(raiz);
        }

        private void RecorridoInOrdenRec(NodoArbol nodo)
        {
            if (nodo != null)
            {
                RecorridoInOrdenRec(nodo.Izquierdo);
                Console.WriteLine(nodo.Cancion);
                RecorridoInOrdenRec(nodo.Derecho);
            }
        }

        public string GenerarDotArbol()
        {
            string dot = "digraph Arbol {\n  node [shape=circle];\n";
            dot += GenerarDotArbolRec(raiz);
            return dot + "}\n";
        }

        private string GenerarDotArbolRec(NodoArbol nodo)
        {
            if (nodo == null) return "";
            string texto = "";

            if (nodo.Izquierdo != null)
            {
                texto += $"  \"{nodo.Cancion.Titulo}\" -> \"{nodo.Izquierdo.Cancion.Titulo}\";\n";
                texto += GenerarDotArbolRec(nodo.Izquierdo);
            }
            if (nodo.Derecho != null)
            {
                texto += $"  \"{nodo.Cancion.Titulo}\" -> \"{nodo.Derecho.Cancion.Titulo}\";\n";
                texto += GenerarDotArbolRec(nodo.Derecho);
            }
            return texto;
        }

        public NodoArbol ObtenerRaiz() => raiz;
    }
}
