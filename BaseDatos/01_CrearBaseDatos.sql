IF DB_ID('RestauranteDB') IS NULL
    CREATE DATABASE RestauranteDB;
GO

USE RestauranteDB;
GO

IF OBJECT_ID('Pagos', 'U')          IS NOT NULL DROP TABLE Pagos;
IF OBJECT_ID('DetallePedido', 'U')  IS NOT NULL DROP TABLE DetallePedido;
IF OBJECT_ID('Pedidos', 'U')        IS NOT NULL DROP TABLE Pedidos;
IF OBJECT_ID('Productos', 'U')      IS NOT NULL DROP TABLE Productos;
IF OBJECT_ID('Mesas', 'U')          IS NOT NULL DROP TABLE Mesas;
IF OBJECT_ID('Categorias', 'U')     IS NOT NULL DROP TABLE Categorias;
GO

CREATE TABLE Categorias (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Productos (
    IdProducto  INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL,
    Precio      DECIMAL(10,2) NOT NULL CHECK (Precio > 0),
    IdCategoria INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias
        FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);

CREATE TABLE Mesas (
    IdMesa  INT IDENTITY(1,1) PRIMARY KEY,
    Numero  INT NOT NULL UNIQUE CHECK (Numero > 0),
    Estado  NVARCHAR(10) NOT NULL DEFAULT 'Libre'
            CHECK (Estado IN ('Libre', 'Ocupada'))
);

CREATE TABLE Pedidos (
    IdPedido INT IDENTITY(1,1) PRIMARY KEY,
    IdMesa   INT NOT NULL,
    Fecha    DATETIME NOT NULL DEFAULT GETDATE(),
    Estado   NVARCHAR(10) NOT NULL DEFAULT 'Abierto'
             CHECK (Estado IN ('Abierto', 'Pagado')),
    Total    DECIMAL(10,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_Pedidos_Mesas
        FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);

CREATE TABLE DetallePedido (
    IdDetalle      INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido       INT NOT NULL,
    IdProducto     INT NOT NULL,
    Cantidad       INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10,2) NOT NULL CHECK (PrecioUnitario > 0),
    Subtotal       AS (Cantidad * PrecioUnitario) PERSISTED,
    CONSTRAINT FK_Detalle_Pedidos
        FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido),
    CONSTRAINT FK_Detalle_Productos
        FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

CREATE TABLE Pagos (
    IdPago     INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido   INT NOT NULL UNIQUE,
    MetodoPago NVARCHAR(20) NOT NULL
               CHECK (MetodoPago IN ('Efectivo', 'Tarjeta')),
    Monto      DECIMAL(10,2) NOT NULL CHECK (Monto > 0),
    Fecha      DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Pagos_Pedidos
        FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido)
);
GO

INSERT INTO Categorias (Nombre) VALUES
    ('Comidas'), ('Bebidas'), ('Postres'), ('Otros');

INSERT INTO Productos (Nombre, Precio, IdCategoria) VALUES
    ('Pupusas revueltas',   1.50, 1),
    ('Pollo con papas',     5.75, 1),
    ('Hamburguesa',         4.50, 1),
    ('Sopa de gallina',     6.00, 1),
    ('Gaseosa',             1.25, 2),
    ('Jugo natural',        1.75, 2),
    ('Cafe',                1.00, 2),
    ('Pastel de chocolate', 2.50, 3),
    ('Helado',              2.00, 3),
    ('Porcion de arroz',    1.00, 4);

INSERT INTO Mesas (Numero) VALUES (1), (2), (3), (4), (5), (6);
GO

SELECT * FROM Categorias;
SELECT * FROM Mesas;

SELECT p.IdProducto, p.Nombre AS Producto, p.Precio, c.Nombre AS Categoria
FROM Productos p
INNER JOIN Categorias c ON c.IdCategoria = p.IdCategoria
ORDER BY c.Nombre, p.Nombre;
GO