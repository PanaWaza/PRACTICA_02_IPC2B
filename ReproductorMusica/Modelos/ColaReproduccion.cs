using System;
using Eto.Forms; // en vez de System.Windows.Forms

namespace Modelos
{
    public class ColaReproduccion
    {
        private NodoCola frente;
        private NodoCola final;
        private int contador;

        public ColaReproduccion()
        {
            frente = null;
            final = null;
            contador = 0;
        }

        public void Encolar(Cancion cancion)
        {
            NodoCola nuevo = new NodoCola(cancion);
            if (EstaVacia())
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
            contador++;
        }

        public Cancion Desencolar()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La cola está vacía");
            }

            Cancion cancion = frente.Cancion;
            frente = frente.Siguiente;
            contador--;

            if (frente == null)
            {
                final = null;
            }

            return cancion;
        }

        public Cancion VerFrente()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("La cola está vacía");
            }

            return frente.Cancion;
        }

        public bool EstaVacia() => frente == null;

        public int Contar() => contador;

        public double CalcularTiempoTotal()
        {
            double total = 0;
            NodoCola actual = frente;
            while (actual != null)
            {
                total += actual.Cancion.Duracion;
                actual = actual.Siguiente;
            }
            return total;
        }

        // Recorrido directo sin usar List<T> ni arreglos
        public void ImprimirTodas()
        {
            if (EstaVacia())
            {
                Console.WriteLine("La cola de reproducción está vacía.");
                return;
            }

            NodoCola actual = frente;
            int posicion = 1;

            while (actual != null)
            {
                Console.WriteLine($"{posicion}. {actual.Cancion}");
                actual = actual.Siguiente;
                posicion++;
            }
        }

        // Recorre la cola y carga cada elemento en el ListBox de Eto.Forms.
        // Eto.Forms.ListBox.Items.Add acepta directamente un string (crea un
        // ListItem internamente), así que usamos el ToString() de Cancion,
        // igual que hacías con el ListBox de WinForms.
        public void MostrarEnListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            NodoCola actual = frente;

            while (actual != null)
            {
                listBox.Items.Add(actual.Cancion.ToString());
                actual = actual.Siguiente;
            }
        }

        public string GenerarDotCola()
        {
            string dot = "digraph Cola {\n  rankdir=LR;\n  node [shape=record];\n";
            NodoCola actual = frente;
            int i = 0;

            while (actual != null)
            {
                dot += $"  nodo{i} [label=\"{actual.Cancion.Titulo}\"];\n";
                if (actual.Siguiente != null)
                    dot += $"  nodo{i} -> nodo{i + 1};\n";
                actual = actual.Siguiente;
                i++;
            }
            return dot + "}\n";
        }
    }
}
