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
