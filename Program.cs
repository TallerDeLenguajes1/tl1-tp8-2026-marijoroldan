using EspacioTarea;
using System.Linq;

const int cantT = 5; // declaro una constante

// CREO LAS LISTAS PERO AMBAS ESTAN VACIAS INICIALMENTE

List<T> tareasPendientes = new List<T>();
List<T> tareasRealizadas = new List<T>();

// ==============================================================================
// ========================== MENÚ DE TAREAS ====================================
// ==============================================================================

int opcion = 99;

CargarTareasAleatorias(tareasPendientes);

do
{
    Console.WriteLine("\n===== SISTEMA DE GESTIÓN DE TAREAS =====");
    Console.WriteLine("1. Mover tarea de pendiente a realizada");
    Console.WriteLine("2. Buscar tarea por palabra clave");
    Console.WriteLine("3. Buscar tarea por ID");
    Console.WriteLine("4. Listar todas las tareas");
    Console.WriteLine("0. Salir");
    Console.Write("Elija una opción: ");
    if (int.TryParse(Console.ReadLine(), out opcion))
    {
        switch (opcion)
        {
            case 1:
                MoverTarea(tareasPendientes, tareasRealizadas);
                break;

            case 2:
                BuscarPorPalabra(tareasPendientes);
                break;

            case 3:
                BuscarPorId(tareasPendientes);
                break;

            case 4:
                MostrarListas(tareasPendientes, tareasRealizadas);
                break;

            case 0:
                Console.WriteLine("Saliendo del programa...");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    else
    {
        // Si el TryParse falló, forzamos a que 'opcion' no sea 0 
        // para que el bucle while no se cierre por accidente.
        opcion = 99;
        Console.WriteLine("Error: Por favor, ingrese una opción válida.");
    }

} while (opcion != 0);




// ========================================================================================
//                                 SECCIÓN DE FUNCIONES
// ========================================================================================



// ********************************************************************
//                       FUNCION CARGAR TAREAS

static void CargarTareasAleatorias(List<T> pendientes)
{
    // creo un arreglo con tipos de tareas a realizar para poder agregarlas de manera random

    string[] tiposDeTareas = {"Hacer pedido de productos",
    "Controlar stock en depósito de procutos",
    "Llamar al proveedor",
    "Limpiar el sector de carga",
    "Revisar facturas pendientes"};

    //creo un objeto random
    Random rand = new Random();

    //hago lista de tareas pendientes
    for (int i = 0; i < cantT; i++)
    {
        T nuevaTarea = new T();
        nuevaTarea.TareaID = i + 1;
        int indiceAleatorio = rand.Next(0, tiposDeTareas.Length);
        nuevaTarea.Descripcion = tiposDeTareas[indiceAleatorio];
        nuevaTarea.Duracion = rand.Next(10, 101); //la duración va a ser aleatoria
        pendientes.Add(nuevaTarea);   // aca guardo voy guardando los datos en la lista de tareas pendientes
    }

}


// ********************************************************************
//                       FUNCION OBTENER TAREAS POR ID

static T ObtenerTareaPorId(List<T> pendientes)
{
    Console.Write("Ingrese el ID de la tarea: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        // Buscamos y retornamos directamente el objeto (o null si no existe)
        return pendientes.FirstOrDefault(t => t.TareaID == id);
    }
    else
    {
        return null;
    }
}

// ********************************************************************
//                       FUNCION OBTENER TAREAS POR PALABRA

static List<T> ObtenerTareaPorPalabra(List<T> pendientes)
{
    string palabraBuscada;
    Console.Write("Ingrese la palabra que desea buscar: ");
    palabraBuscada = Console.ReadLine();
    // Te devuelve una sublista (List<Tarea>) con todas las coincidencias
    List<T> coincidencias = pendientes.Where(t => t.Descripcion.Contains(palabraBuscada)).ToList();
    return coincidencias;
}


// ********************************************************************
//                       FUNCION MOVER TAREAS

static void MoverTarea(List<T> pendientes, List<T> realizadas)
{
    int opcion2 = 99;
    do
    {
        Console.WriteLine("\n1. Mover tarea por palabra clave");
        Console.WriteLine("2. Mover tarea por ID");
        Console.WriteLine("Elija una opción: ");

        if (int.TryParse(Console.ReadLine(), out opcion2))
        {
            if (opcion2 == 1)
            {
List<T> tareasFiltradas = ObtenerTareaPorPalabra(pendientes);

    if (tareasFiltradas.Count > 0)
    {
        Console.WriteLine($"\nSe encontraron {tareasFiltradas.Count} tareas que coinciden:");
        foreach (T TF in tareasFiltradas)
        {
            Console.WriteLine($"\nID de la tarea: {TF.TareaID}");
            Console.WriteLine($"Descripcion: {TF.Descripcion}");
            Console.WriteLine($"Duración: {TF.Duracion} min");
        }
        Console.WriteLine($"\nCuál desea eliminar de pendientes?");

        T tareaAMover = ObtenerTareaPorId(tareasFiltradas);

        if (tareaAMover != null)
        {
            realizadas.Add(tareaAMover);
            pendientes.Remove(tareaAMover); // Lo borrás de la original
            Console.WriteLine("¡Tarea movida con éxito!");
            opcion2 = 0; // Rompe el menú interno para regresar al principal
        }
    }
    else
    {
    Console.WriteLine("No se encontraron tareas con esa palabra :()");
    }

            }
            else if (opcion2 == 2)
            {
                T tareaEncontrada = ObtenerTareaPorId(pendientes);

                if (tareaEncontrada != null)
                {
                    // 1. Lo agregamos al contenedor de realizadas
                    realizadas.Add(tareaEncontrada);

                    // 2. Lo sacamos del contenedor de pendientes (¡Así de simple!)
                    pendientes.Remove(tareaEncontrada);

                    Console.WriteLine("¡Tarea movida a realizadas con éxito!");
                    opcion2 = 0; // Forzamos la salida al menú principal tras el éxito
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna tarea con ese ID.");
                }
            }
        }
        else
        {
            opcion2 = 99;
            Console.WriteLine("Error: Por favor, ingrese una opción válida.");
        }
    } while (opcion2 != 0);

}


// ********************************************************************
//                       FUNCION BUSCAR TAREAS POR ID Y MOSTRAR

static void BuscarPorId(List<T> pendientes)
{

    T tareaEncontrada = ObtenerTareaPorId(pendientes);
    if (tareaEncontrada != null)
    {
        Console.WriteLine($"\n****************** Tarea Encontrada *****************");
        Console.WriteLine($"ID: {tareaEncontrada.TareaID}");
        Console.WriteLine($"Descripción: {tareaEncontrada.Descripcion}");
        Console.WriteLine($"Duración: {tareaEncontrada.Duracion} min");
    }
    else
    {
        Console.WriteLine("No se encontró ninguna tarea con ese ID.");
    }

}


// ********************************************************************
//                       FUNCION BUSCAR TAREAS POR PALABRA

static void BuscarPorPalabra(List<T> pendientes)
{
    List<T> tareasFiltradas = ObtenerTareaPorPalabra(pendientes);

    if (tareasFiltradas.Count > 0) // aca controlo que la lista no este vacia porque puede devolverme que si esta y romper el programa, no me conviene hacer como en ID
    {
        Console.WriteLine($"\nSe encontraron {tareasFiltradas.Count} tareas que coinciden:");
        foreach (T TF in tareasFiltradas)
        {
            Console.WriteLine($"ID de la tarea: {TF.TareaID}");
            Console.WriteLine($"Descripcion: {TF.Descripcion}");
            Console.WriteLine($"Duración: {TF.Duracion} min");
        }
    }
    else
    {
    Console.WriteLine("No se encontraron tareas con esa palabra :()");
    }
}


// ********************************************************************
//                       FUNCION MOSTRAR TAREAS

static void MostrarListas(List<T> pendientes, List<T> realizadas)
{
    Console.WriteLine("**************************** TAREAS PENDIENTES *******************************");
    foreach (T TP in pendientes)
    {
        Console.WriteLine($"ID de la tarea: {TP.TareaID}");
        Console.WriteLine($"Descripcion: {TP.Descripcion}");
        Console.WriteLine($"Duración: {TP.Duracion} min");
    }
    Console.WriteLine("**************************** TAREAS REALIZADAS *******************************");
    foreach (T TR in realizadas)
    {
        Console.WriteLine($"ID de la tarea: {TR.TareaID}");
        Console.WriteLine($"Descripcion: {TR.Descripcion}");
        Console.WriteLine($"Duración: {TR.Duracion} min");
    }
}