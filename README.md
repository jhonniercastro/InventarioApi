# MiniSistema de Gestión de Inventario - Backend API

Este repositorio contiene la API RESTful para el sistema de gestión de inventario de la empresa CCL. Está desarrollado para proporcionar endpoints seguros que permitan consultar el stock y registrar entradas o salidas de productos.

## Tecnologías Utilizadas

*   Framework: C# (.NET Core 9)
*   Base de Datos: PostgreSQL (Configuración local)
*   ORM: Entity Framework Core (Consultas básicas, sin procedimientos almacenados)
*   Seguridad: Autenticación con JWT (Bearer Token)

## Requisitos Previos

*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [PostgreSQL](https://www.postgresql.org/download/) ejecutándose localmente.

## Configuración de la Base de Datos

El sistema utiliza una única tabla llamada `productos` manejada mediante datos iniciales cargados manualmente, sin migraciones complejas. 

1. Crea una base de datos en tu servidor PostgreSQL local (ej. `inventario_bd`).
2. Actualiza la cadena de conexión (`DefaultConnection`) en el archivo `appsettings.json` con tus credenciales de PostgreSQL.
3. Ejecuta el siguiente script SQL en tu administrador de base de datos para crear la tabla e insertar los datos iniciales:
4. 
```sql
CREATE TABLE productos (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cantidad INT NOT NULL
);

INSERT INTO Productos (Nombre, Cantidad) VALUES
('Laptop', 10),
('Mouse', 25),
('Teclado', 20),
('Monitor', 8),
('Impresora', 5),
('Memoria USB', 40);
