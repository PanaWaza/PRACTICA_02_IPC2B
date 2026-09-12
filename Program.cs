using Eto.Forms;

namespace ReproductorMusica
{
    public class Program
    {
        [System.STAThread]
        public static void Main(string[] args)
        {
            // new Eto.Platform.Detect ya elige GTK automáticamente en Linux.
            new Application(Eto.Platforms.Gtk).Run(new MainForm());
        }
    }
}
