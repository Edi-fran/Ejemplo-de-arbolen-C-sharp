using System;

public class Nodo_Arbol
{
    private int valor;
    private Nodo_Arbol izquierda;
    private Nodo_Arbol derecha;

    public Nodo_Arbol(int valor)
    {
        this.valor = valor;
        izquierda = null;
        derecha = null;
    }

    public int ObtenerValor()
    {
        return valor;
    }

    public void AsignarValor(int nuevoValor)
    {
        valor = nuevoValor;
    }

    public Nodo_Arbol ObtenerIzquierda()
    {
        return izquierda;
    }

    public void AsignarIzquierda(Nodo_Arbol nodo)
    {
        izquierda = nodo;
    }

    public Nodo_Arbol ObtenerDerecha()
    {
        return derecha;
    }

    public void AsignarDerecha(Nodo_Arbol nodo)
    {
        derecha = nodo;
    }
}

public class Arbol_Binario
{
    public Nodo_Arbol Raiz { get; private set; }

    public void Insertar(int valor)
    {
        Raiz = InsertarRecursivo(Raiz, valor);
    }

    private Nodo_Arbol InsertarRecursivo(Nodo_Arbol nodo, int valor)
    {
        if (nodo == null)
            return new Nodo_Arbol(valor);

        if (valor < nodo.ObtenerValor())
            nodo.AsignarIzquierda(InsertarRecursivo(nodo.ObtenerIzquierda(), valor));
        else
            nodo.AsignarDerecha(InsertarRecursivo(nodo.ObtenerDerecha(), valor));

        return nodo;
    }

    public Nodo_Arbol Buscar(int valor)
    {
        return BuscarRecursivo(Raiz, valor);
    }

    private Nodo_Arbol BuscarRecursivo(Nodo_Arbol nodo, int valor)
    {
        if (nodo == null || nodo.ObtenerValor() == valor)
            return nodo;

        return valor < nodo.ObtenerValor()
            ? BuscarRecursivo(nodo.ObtenerIzquierda(), valor)
            : BuscarRecursivo(nodo.ObtenerDerecha(), valor);
    }

    public void Eliminar(int valor)
    {
        Raiz = EliminarRecursivo(Raiz, valor);
    }

    private Nodo_Arbol EliminarRecursivo(Nodo_Arbol nodo, int valor)
    {
        if (nodo == null) return null;

        if (valor < nodo.ObtenerValor())
            nodo.AsignarIzquierda(EliminarRecursivo(nodo.ObtenerIzquierda(), valor));
        else if (valor > nodo.ObtenerValor())
            nodo.AsignarDerecha(EliminarRecursivo(nodo.ObtenerDerecha(), valor));
        else
        {
            if (nodo.ObtenerIzquierda() == null) return nodo.ObtenerDerecha();
            if (nodo.ObtenerDerecha() == null) return nodo.ObtenerIzquierda();

            nodo.AsignarValor(EncontrarMinimo(nodo.ObtenerDerecha()));
            nodo.AsignarDerecha(EliminarRecursivo(nodo.ObtenerDerecha(), nodo.ObtenerValor()));
        }
        return nodo;
    }

    private int EncontrarMinimo(Nodo_Arbol nodo)
    {
        while (nodo.ObtenerIzquierda() != null)
            nodo = nodo.ObtenerIzquierda();
        return nodo.ObtenerValor();
    }

    public void MostrarRecorrido(string tipo)
    {
        Console.Write(tipo + ": ");
        switch (tipo.ToLower())
        {
            case "a":
                Console.WriteLine("(InOrden)");
                RecorridoInOrden(Raiz);
                break;
            case "b":
                Console.WriteLine("(PreOrden)");
                RecorridoPreOrden(Raiz);
                break;
            case "c":
                Console.WriteLine("(PostOrden)");
                RecorridoPostOrden(Raiz);
                break;
            default:
                Console.WriteLine("Tipo de recorrido no reconocido. Opciones: a) InOrden, b) PreOrden, c) PostOrden");
                return;
        }
        Console.WriteLine();
    }

    private void RecorridoInOrden(Nodo_Arbol nodo)
    {
        if (nodo != null)
        {
            RecorridoInOrden(nodo.ObtenerIzquierda());
            Console.Write(nodo.ObtenerValor() + " ");
            RecorridoInOrden(nodo.ObtenerDerecha());
        }
    }

    private void RecorridoPreOrden(Nodo_Arbol nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.ObtenerValor() + " ");
            RecorridoPreOrden(nodo.ObtenerIzquierda());
            RecorridoPreOrden(nodo.ObtenerDerecha());
        }
    }

    private void RecorridoPostOrden(Nodo_Arbol nodo)
    {
        if (nodo != null)
        {
            RecorridoPostOrden(nodo.ObtenerIzquierda());
            RecorridoPostOrden(nodo.ObtenerDerecha());
            Console.Write(nodo.ObtenerValor() + " ");
        }
    }

    public void MostrarResumen()
    {
        Console.WriteLine("\n--- Información del Árbol ---");
        Console.WriteLine("Cantidad de nodos: " + ContarNodos(Raiz));
        Console.WriteLine("Cantidad de hojas: " + ContarHojas(Raiz));
        Console.WriteLine("Altura: " + CalcularAltura(Raiz));
    }

    private int ContarNodos(Nodo_Arbol nodo)
    {
        if (nodo == null) return 0;
        return 1 + ContarNodos(nodo.ObtenerIzquierda()) + ContarNodos(nodo.ObtenerDerecha());
    }

    private int ContarHojas(Nodo_Arbol nodo)
    {
        if (nodo == null) return 0;
        if (nodo.ObtenerIzquierda() == null && nodo.ObtenerDerecha() == null) return 1;
        return ContarHojas(nodo.ObtenerIzquierda()) + ContarHojas(nodo.ObtenerDerecha());
    }

    private int CalcularAltura(Nodo_Arbol nodo)
    {
        if (nodo == null) return -1;
        return 1 + Math.Max(CalcularAltura(nodo.ObtenerIzquierda()), CalcularAltura(nodo.ObtenerDerecha()));
    }
}

public class Aplicacion_Arbol
{
    static void Main()
    {
        Arbol_Binario arbol = new Arbol_Binario();
        int opcion;

        do
        {
            Console.WriteLine("\n********* UNIVERSIDAD ESTATAL AMAZÓNICA (UEA) *********");
            Console.WriteLine("/////// INGENIERÍA EN TECNOLOGÍAS DE LA INFORMACIÓN ///////");
            Console.WriteLine("=== SISTEMA DE OPERACIÓN CON ÁRBOL BINARIO ===");
            Console.WriteLine("1. Insertar, Buscar o Eliminar un dato");
            Console.WriteLine("2. Mostrar recorrido del árbol");
            Console.WriteLine("3. Ver estadísticas del árbol");
            Console.WriteLine("0. Salir del sistema");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("\n-- Operaciones básicas --");
                    Console.Write("(i) Insertar | (b) Buscar | (e) Eliminar: ");
                    string sub = Console.ReadLine().ToLower();
                    Console.Write("Ingrese valor numérico: ");
                    int valor = int.Parse(Console.ReadLine());

                    if (sub == "i")
                    {
                        arbol.Insertar(valor);
                        Console.WriteLine("✓ Valor insertado.");
                    }
                    else if (sub == "b")
                    {
                        Console.WriteLine(arbol.Buscar(valor) != null ? "✓ Valor encontrado." : "✗ Valor no encontrado.");
                    }
                    else if (sub == "e")
                    {
                        arbol.Eliminar(valor);
                        Console.WriteLine("✓ Eliminación realizada si existía.");
                    }
                    else
                    {
                        Console.WriteLine("✗ Opción inválida.");
                    }
                    break;

                case 2:
                    Console.WriteLine("\n-- Tipos de recorrido disponibles --");
                    Console.WriteLine("a) InOrden");
                    Console.WriteLine("b) PreOrden");
                    Console.WriteLine("c) PostOrden");
                    Console.Write("Seleccione una opción (a, b, c): ");
                    string tipo = Console.ReadLine();
                    arbol.MostrarRecorrido(tipo);
                    break;

                case 3:
                    arbol.MostrarResumen();
                    break;

                case 0:
                    Console.WriteLine("Cerrando el sistema. ¡Hasta pronto!");
                    break;

                default:
                    Console.WriteLine("✗ Opción no válida. Intente de nuevo.");
                    break;
            }

        } while (opcion != 0);
    }
}