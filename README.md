El Sistema Gestor de Ventas e Inventario (Mini-POS) es una aplicación de consola desarrollada en C# que permite administrar productos, controlar el inventario y registrar ventas.

El sistema fue creado como un proyecto académico para aplicar conceptos fundamentales de programación, como:

- Variables y tipos de datos.
- Listas ("List<T>").
- Estructuras condicionales.
- Ciclos.
- Métodos y parámetros.
- Validación de datos.
- Manejo de valores decimales.
- Operaciones matemáticas.
- Control de inventario.
- Registro y estadísticas de ventas.

---

Funcionalidades principales

El programa cuenta con un menú principal con las siguientes opciones:

1. Registrar nuevo producto

Permite agregar un producto al inventario ingresando:

- Nombre del producto.
- Precio unitario.
- Stock inicial.

El sistema valida que:

- El nombre no esté vacío.
- No exista otro producto con el mismo nombre.
- El precio sea mayor a cero.
- El stock sea un número válido.

2. Consultar inventario completo

Muestra todos los productos registrados junto con:

- Nombre.
- Precio.
- Stock disponible.

Cuando un producto tiene menos de 5 unidades, el sistema muestra una alerta de bajo stock.

3. Registrar una venta

Permite seleccionar un producto y registrar una venta indicando:

- Producto.
- Cantidad a comprar.
- Si aplica o no un descuento de cliente frecuente.

El sistema verifica que exista suficiente stock antes de realizar la venta.

Al finalizar, se genera un ticket mostrando:

- Subtotal.
- Descuento del 10%, si aplica.
- IVA del 19%.
- Total a pagar.
- Stock actualizado.

4. Ver reporte de caja y estadísticas

Muestra información general de las ventas realizadas:

- Total de ventas realizadas.
- Total acumulado en caja.
- Promedio de dinero por venta.
- Producto más vendido y cantidad de unidades vendidas.

5. Salir

Permite finalizar la ejecución del programa.

---

Cálculo de las ventas

El sistema utiliza las siguientes reglas:

Subtotal:

"Precio × Cantidad"

Descuento:

Se aplica un descuento del 10% cuando el cliente es frecuente.

IVA:

Se calcula un 19% de IVA sobre el valor después del descuento.

Total:

"Subtotal - Descuento + IVA"

---

 Tecnologías utilizadas

- Lenguaje: C#
- Framework: .NET
- Tipo de aplicación: Aplicación de consola
- Control de versiones: Git
- Repositorio: GitHub

---

Requisitos

Para ejecutar el proyecto se necesita:

1. Tener instalado .NET SDK.
2. Tener Git instalado si se desea clonar el repositorio.
3. Contar con una terminal o un editor de código como Visual Studio Code.

---
 Instrucciones para ejecutar el proyecto

1. Clonar el repositorio

Desde una terminal, ejecutar:

git clone URL_DEL_REPOSITORIO

Reemplaza "URL_DEL_REPOSITORIO" por la dirección de tu repositorio de GitHub.

2. Entrar a la carpeta del proyecto

cd NOMBRE_DEL_PROYECTO

3. Ejecutar el programa

Ejecuta:

dotnet run

El programa mostrará el menú principal y permitirá seleccionar las diferentes opciones del sistema.

---

Estructura básica del proyecto

Sistema-MiniPOS/
│
├── Program.cs
├── README.md
└── Sistema-MiniPOS.csproj

"Program.cs"

Contiene todo el código principal de la aplicación, incluyendo:

- Menú principal.
- Registro de productos.
- Consulta de inventario.
- Registro de ventas.
- Cálculo de facturas.
- Reporte de caja.
- Validaciones de entrada.

"README.md"

Contiene la documentación e instrucciones necesarias para conocer y ejecutar el proyecto.

".csproj"

Contiene la configuración del proyecto de C# y .NET.

---

Autora

Rosaura León Rodríguez 

Proyecto académico desarrollado en C#.
