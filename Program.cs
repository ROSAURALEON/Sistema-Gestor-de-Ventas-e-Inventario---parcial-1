using System;
using System.Collections.Generic;

class Program
{
    static List<string> productos = new List<string>();
    static List<decimal> precios = new List<decimal>();
    static List<int> stocks = new List<int>();
    static List<int> unidadesVendidas = new List<int>();

    static int totalVentasRealizadas = 0;
    static decimal totalDineroCaja = 0m;

    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();
            ImprimirEncabezado("SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");
            Console.WriteLine("1. Registrar nuevo producto en inventario");
            Console.WriteLine("2. Consultar inventario completo");
            Console.WriteLine("3. Registrar una venta");
            Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
            Console.WriteLine("5. Salir");
            Console.WriteLine("====================================================");

            opcion = LeerEntero("Seleccione una opción (1-5): ", 1, 5);

            switch (opcion)
            {
                case 1:
                    RegistrarProducto();
                    break;
                case 2:
                    ConsultarInventario();
                    break;
                case 3:
                    RegistrarVenta();
                    break;
                case 4:
                    MostrarReporteCaja();
                    break;
                case 5:
                    Console.WriteLine("\n¡Gracias por utilizar el sistema Mini-POS! Hasta pronto.");
                    break;
            }

            if (opcion != 5)
            {
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 5);
    }

    static int LeerEntero(string mensaje, int min, int max)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int valor))
            {
                if (valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine($"[ERROR] Opción fuera de rango. Ingrese un valor entre {min} y {max}.");
            }
            else
            {
                Console.WriteLine("[ERROR] Entrada no válida. Debe ingresar un número entero.");
            }
        }
    }

    static decimal LeerDecimal(string mensaje, decimal min)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (decimal.TryParse(entrada, out decimal valor))
            {
                if (valor >= min)
                {
                    return valor;
                }
                Console.WriteLine($"[ERROR] El valor debe ser mayor o igual a {min:C2}.");
            }
            else
            {
                Console.WriteLine("[ERROR] Entrada no válida. Debe ingresar un número decimal válido.");
            }
        }
    }

    static decimal CalcularFactura(decimal precio, int cantidad, bool tieneDescuento, out decimal montoIva, out decimal montoDescuento)
    {
        decimal subtotal = precio * cantidad;

        montoDescuento = tieneDescuento ? subtotal * 0.10m : 0m;

        decimal baseImponible = subtotal - montoDescuento;
        montoIva = baseImponible * 0.19m;

        decimal totalPagar = subtotal - montoDescuento + montoIva;
        return totalPagar;
    }

    static void ImprimirEncabezado(string titulo)
    {
        Console.WriteLine("====================================================");
        Console.WriteLine($"                 {titulo.ToUpper()}");
        Console.WriteLine("====================================================");
    }

    static void RegistrarProducto()
    {
        Console.Clear();
        ImprimirEncabezado("REGISTRAR NUEVO PRODUCTO");

        string nombre = "";
        while (true)
        {
            Console.Write("Ingrese el nombre del producto: ");
            nombre = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("[ERROR] El nombre del producto no puede estar vacío.");
                continue;
            }

            bool existe = false;
            foreach (string p in productos)
            {
                if (p.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
            {
                Console.WriteLine("[ERROR] Ya existe un producto registrado con ese nombre.");
            }
            else
            {
                break;
            }
        }

        decimal precio = LeerDecimal("Ingrese el precio unitario ($): ", 0.01m);
        int stock = LeerEntero("Ingrese el stock inicial (cantidad >= 0): ", 0, int.MaxValue);

        productos.Add(nombre);
        precios.Add(precio);
        stocks.Add(stock);
        unidadesVendidas.Add(0);

        Console.WriteLine($"\n[OK] Producto '{nombre}' registrado exitosamente.");
    }

    static void ConsultarInventario()
    {
        Console.Clear();
        ImprimirEncabezado("INVENTARIO COMPLETO");

        if (productos.Count == 0)
        {
            Console.WriteLine("No hay productos registrados en el inventario.");
            return;
        }

        for (int i = 0; i < productos.Count; i++)
        {
            string alertaStock = stocks[i] < 5 ? " [ALERTA: BAJO STOCK]" : "";
            Console.WriteLine($"{i + 1}. {productos[i],-25} | Precio: {precios[i],12:C2} | Stock: {stocks[i],3}{alertaStock}");
        }
    }

    static void RegistrarVenta()
    {
        Console.Clear();
        ImprimirEncabezado("REGISTRAR VENTA");

        if (productos.Count == 0)
        {
            Console.WriteLine("[ERROR] No hay productos en el inventario para vender.");
            return;
        }

        ConsultarInventario();
        Console.WriteLine();

        int seleccion = LeerEntero($"Seleccione el número del producto a vender (1-{productos.Count}): ", 1, productos.Count);
        int indice = seleccion - 1;

        if (stocks[indice] == 0)
        {
            Console.WriteLine($"[ERROR] El producto '{productos[indice]}' no tiene unidades en stock disponibles.");
            return;
        }

        int cantidad = 0;
        while (true)
        {
            cantidad = LeerEntero("Ingrese la cantidad a comprar: ", 1, int.MaxValue);

            if (cantidad > stocks[indice])
            {
                Console.WriteLine($"[ERROR] Stock insuficiente. Solo quedan {stocks[indice]} unidades en inventario.");
            }
            else
            {
                break;
            }
        }

        bool tieneDescuento = false;
        while (true)
        {
            Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
            string resp = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (resp == "S")
            {
                tieneDescuento = true;
                break;
            }
            else if (resp == "N")
            {
                tieneDescuento = false;
                break;
            }

            Console.WriteLine("[ERROR] Debe responder 'S' para Sí o 'N' para No.");
        }

        decimal subtotal = precios[indice] * cantidad;
        decimal totalPagar = CalcularFactura(precios[indice], cantidad, tieneDescuento, out decimal montoIva, out decimal montoDescuento);

        stocks[indice] -= cantidad;
        unidadesVendidas[indice] += cantidad;
        totalVentasRealizadas++;
        totalDineroCaja += totalPagar;

        Console.Clear();
        ImprimirEncabezado("TICKET DE VENTA");
        Console.WriteLine($" Producto:             {productos[indice]} (x{cantidad})");
        Console.WriteLine($" Subtotal:             {subtotal,12:C2}");
        Console.WriteLine($" Descuento (10%):     -{montoDescuento,12:C2}");
        Console.WriteLine($" IVA (19%):            +{montoIva,12:C2}");
        Console.WriteLine(" ---------------------------------------------------");
        Console.WriteLine($" TOTAL A PAGAR:        {totalPagar,12:C2}");
        Console.WriteLine("====================================================");
        Console.WriteLine($"[OK] Venta efectuada con éxito. Stock actualizado: {stocks[indice]} unidades.");
    }

    static void MostrarReporteCaja()
    {
        Console.Clear();
        ImprimirEncabezado("REPORTE DE CAJA Y ESTADÍSTICAS DIARIAS");

        Console.WriteLine($"Total de ventas realizadas:  {totalVentasRealizadas}");
        Console.WriteLine($"Total acumulado en caja:     {totalDineroCaja:C2}");

        decimal promedio = totalVentasRealizadas > 0 ? totalDineroCaja / totalVentasRealizadas : 0m;
        Console.WriteLine($"Promedio por venta:          {promedio:C2}");

        int maxVendidas = 0;
        string productoMasVendido = "Ninguno";

        for (int i = 0; i < unidadesVendidas.Count; i++)
        {
            if (unidadesVendidas[i] > maxVendidas)
            {
                maxVendidas = unidadesVendidas[i];
                productoMasVendido = productos[i];
            }
        }

        if (maxVendidas > 0)
        {
            Console.WriteLine($"Producto más vendido:        {productoMasVendido} ({maxVendidas} unidades)");
        }
        else
        {
            Console.WriteLine("Producto más vendido:        Aún no se registran ventas.");
        }
    }
}