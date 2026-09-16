using System;
using System.Diagnostics;
using System.IO;
using Eto.Drawing;  // en vez de System.Drawing
using Eto.Forms;    // en vez de System.Windows.Forms

namespace Modelos
{
    public static class GraphvizHelper
    {
        // imageView reemplaza al PictureBox de WinForms.
        public static void GenerarYMostrarImagen(string contenidoDot, string nombreArchivo, ImageView imageView)
        {
            try
            {
                string dotPath = $"{nombreArchivo}.dot";
                string imgPath = $"{nombreArchivo}.png";

                // 1. Guardar el código DOT en un archivo temporal
                File.WriteAllText(dotPath, contenidoDot);

                // 2. Configurar la ejecución del comando 'dot' de Graphviz.
                //    En Linux: sudo apt install graphviz (debe quedar en el PATH).
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot",
                    Arguments = $"-Tpng \"{dotPath}\" -o \"{imgPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }

                // 3. Cargar la imagen generada en el ImageView evitando bloqueos de archivo
                if (File.Exists(imgPath))
                {
                    // Liberar la imagen previa del control para evitar excepciones de archivo en uso
                    if (imageView.Image != null)
                    {
                        imageView.Image.Dispose();
                        imageView.Image = null;
                    }

                    using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                    {
                        imageView.Image = new Bitmap(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo generar el gráfico. Verifique que Graphviz esté instalado (sudo apt install graphviz).\n\nDetalles: {ex.Message}",
                    "Error de Graphviz",
                    MessageBoxButtons.OK,
                    MessageBoxType.Error);
            }
        }
    }
}
