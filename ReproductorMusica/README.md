# Reproductor de Música — Cola + Árbol Binario (Eto.Forms)

## Requisitos previos en Linux

```bash
# .NET SDK 8 (si no lo tienes)
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Motor gráfico GTK3 que usa Eto.Forms en Linux
sudo apt install -y libgtk-3-0

# Graphviz (para generar los .png de la cola y el árbol)
sudo apt install -y graphviz
dot -V   # debe imprimir la versión, confirma que quedó en el PATH
```

## Restaurar y ejecutar

```bash
cd ReproductorMusica
dotnet restore
dotnet run
```

La primera vez que corres `dotnet restore` va a descargar `Eto.Forms` y
`Eto.Platform.Gtk` desde NuGet — necesitas conexión a internet para eso,
pero después queda cacheado.

## Cómo usar la app

1. Clic en **"Cargar canciones.json"** (lee el archivo, llena la cola y el árbol).
2. **"Reproducir siguiente"** hace `Desencolar()` y muestra qué canción sonó.
3. El cuadro de **búsqueda** usa `arbol.Buscar(titulo)` (búsqueda binaria por título).
4. Las pestañas de abajo muestran los `.png` generados por Graphviz de la cola
   y del árbol, y se regeneran cada vez que cambia el estado (`ActualizarTodo()`).

## Qué cambió respecto a tu código con `System.Windows.Forms`

| Antes (WinForms)            | Ahora (Eto.Forms)              |
|------------------------------|---------------------------------|
| `System.Windows.Forms.ListBox` | `Eto.Forms.ListBox`          |
| `PictureBox`                  | `ImageView`                    |
| `System.Drawing.Image`        | `Eto.Drawing.Bitmap`           |
| `MessageBox.Show(...)` (WinForms) | `MessageBox.Show(...)` (Eto, mismo nombre, distinto namespace) |
| `MessageBoxIcon.Error`        | `MessageBoxType.Error`         |

Toda tu lógica de `NodoCola`, `NodoArbol`, `ColaReproduccion.Encolar/Desencolar`
y `ArbolBinario.Insertar/Buscar` quedó **exactamente igual** — eso no depende
para nada del framework gráfico, así que no perdiste nada de lo que ya tenías.

## Si el auxiliar exige literalmente "Windows Forms"

Este proyecto usa Eto.Forms porque WinForms no corre de forma nativa en Linux.
Si el auxiliar confirma que solo acepta WinForms real, la misma estructura de
carpetas (`Modelos/`) se reutiliza casi sin cambios en un proyecto de
Windows Forms — lo único que cambiaría es `MainForm.cs` y las dos líneas de
`ListBox`/`PictureBox`/`MessageBox` que aquí están en su versión Eto.
