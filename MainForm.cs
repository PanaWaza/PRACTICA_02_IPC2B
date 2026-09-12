using System;
using System.IO;
using Eto.Forms;
using Eto.Drawing;
using Modelos;

namespace ReproductorMusica
{
    public class MainForm : Form
    {
        private readonly ColaReproduccion cola = new ColaReproduccion();
        private readonly ArbolBinario arbol = new ArbolBinario();

        // Controles (equivalentes a los que arrastrarías en el diseñador de WinForms)
        private readonly ListBox listBoxCola = new ListBox { Height = 180 };
        private readonly ListBox listBoxArbol = new ListBox { Height = 180 };
        private readonly Label lblTiempoTotal = new Label { Text = "Tiempo total: 0 min" };
        private readonly Label lblResultadoBusqueda = new Label { Text = "" };
        private readonly TextBox txtBusqueda = new TextBox { PlaceholderText = "Título de la canción..." };
        private readonly ImageView imageViewCola = new ImageView { Size = new Size(480, 260) };
        private readonly ImageView imageViewArbol = new ImageView { Size = new Size(480, 260) };

        private const string RutaJson = "canciones.json";

        public MainForm()
        {
            Title = "Reproductor de Música - Cola + Árbol Binario";
            ClientSize = new Size(760, 640);
            Padding = 12;

            var btnCargar = new Button { Text = "Cargar canciones.json" };
            btnCargar.Click += (s, e) => CargarCanciones();

            var btnReproducir = new Button { Text = "Reproducir siguiente" };
            btnReproducir.Click += (s, e) => ReproducirSiguiente();

            var btnBuscar = new Button { Text = "Buscar" };
            btnBuscar.Click += (s, e) => BuscarCancion();

            var pestañas = new TabControl();
            pestañas.Pages.Add(new TabPage { Text = "Grafo: Cola", Content = imageViewCola });
            pestañas.Pages.Add(new TabPage { Text = "Grafo: Árbol", Content = imageViewArbol });

            var layout = new DynamicLayout { DefaultSpacing = new Size(8, 8), Padding = 0 };

            layout.AddRow(btnCargar, btnReproducir, lblTiempoTotal);

            layout.AddRow(
                new GroupBox { Text = "Cola de reproducción (orden FIFO)", Content = listBoxCola },
                new GroupBox { Text = "Árbol binario (in-order, alfabético)", Content = listBoxArbol }
            );

            layout.AddRow(new Label { Text = "Buscar por título:" }, txtBusqueda, btnBuscar, lblResultadoBusqueda);

            layout.AddRow(new GroupBox { Text = "Visualización con Graphviz", Content = pestañas });

            layout.Add(null); // empuja todo hacia arriba

            Content = layout;
        }

        private void CargarCanciones()
        {
            if (!File.Exists(RutaJson))
            {
                MessageBox.Show(this,
                    $"No se encontró '{RutaJson}' en:\n{Directory.GetCurrentDirectory()}",
                    "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxType.Warning);
                return;
            }

            CargadorJson.CargarCanciones(RutaJson, cola, arbol);
            ActualizarTodo();
        }

        private void ReproducirSiguiente()
        {
            if (cola.EstaVacia())
            {
                MessageBox.Show(this, "La cola de reproducción está vacía.", "Cola vacía",
                    MessageBoxButtons.OK, MessageBoxType.Information);
                return;
            }

            Cancion actual = cola.Desencolar();
            MessageBox.Show(this, $"Reproduciendo ahora:\n{actual}", "Reproduciendo",
                MessageBoxButtons.OK, MessageBoxType.Information);

            ActualizarTodo();
        }

        private void BuscarCancion()
        {
            string titulo = txtBusqueda.Text?.Trim();
            if (string.IsNullOrEmpty(titulo))
            {
                lblResultadoBusqueda.Text = "Escribe un título.";
                return;
            }

            Cancion encontrada = arbol.Buscar(titulo);
            lblResultadoBusqueda.Text = encontrada != null
                ? $"Encontrada: {encontrada}"
                : "No se encontró esa canción.";
        }

        // Refresca listas, tiempo total y los dos grafos de Graphviz "en tiempo real"
        private void ActualizarTodo()
        {
            cola.MostrarEnListBox(listBoxCola);
            arbol.MostrarEnListBox(listBoxArbol);

            lblTiempoTotal.Text = $"Tiempo total: {cola.CalcularTiempoTotal()} min";

            GraphvizHelper.GenerarYMostrarImagen(cola.GenerarDotCola(), "grafo_cola", imageViewCola);
            GraphvizHelper.GenerarYMostrarImagen(arbol.GenerarDotArbol(), "grafo_arbol", imageViewArbol);
        }
    }
}
