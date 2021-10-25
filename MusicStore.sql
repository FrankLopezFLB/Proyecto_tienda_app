USE Master
GO

IF EXISTS (SELECT
    *
  FROM sys.databases
  WHERE name = 'MusicStore')
BEGIN
  DROP DATABASE MusicStore
END
GO

CREATE DATABASE MusicStore
GO

USE MusicStore
GO

CREATE TABLE cliente (
  codigo int IDENTITY (1, 1) NOT NULL,
  nombre varchar(25) NOT NULL,
  apellido varchar(25) NOT NULL,
  telefono char(9) NULL,
  direccion varchar(200) NULL,
  email varchar(100) NULL UNIQUE,
  clave varchar(150) NOT NULL,
  CONSTRAINT pk_registroClienteID PRIMARY KEY CLUSTERED (codigo)
)
GO

CREATE TABLE puestos (
  id int NOT NULL PRIMARY KEY,
  nombre varchar(20) NOT NULL
)
GO

INSERT INTO puestos
  VALUES (1, 'Adminstrador')
INSERT INTO puestos
  VALUES (2, 'Asistente de ventas')
INSERT INTO puestos
  VALUES (3, 'Jefe de ventas')

CREATE TABLE trabajador (
  codigo int IDENTITY (1, 1) NOT NULL,
  nombre varchar(25) NOT NULL,
  apellido varchar(25) NOT NULL,
  telefono char(9) NULL,
  direccion varchar(200) NULL,
  email varchar(100) NULL UNIQUE,
  clave varchar(150) NOT NULL,  
  dni char(8) NOT NULL UNIQUE,
  id int REFERENCES puestos,
  CONSTRAINT pk_registroTrabajadorID PRIMARY KEY CLUSTERED (codigo)
)
GO

CREATE TABLE distrito (
  id int NOT NULL PRIMARY KEY,
  nombre varchar(50) NOT NULL
)
GO

INSERT INTO DISTRITO
  VALUES (1, 'Cercado de Lima')
INSERT INTO DISTRITO
  VALUES (2, 'Ate')
INSERT INTO DISTRITO
  VALUES (3, 'Barranco')
INSERT INTO DISTRITO
  VALUES (4, 'Breña')
INSERT INTO DISTRITO
  VALUES (5, 'Comas')
INSERT INTO DISTRITO
  VALUES (6, 'Chorrillos')
INSERT INTO DISTRITO
  VALUES (7, 'El Agustino')
INSERT INTO DISTRITO
  VALUES (8, 'Jesus Maria')
INSERT INTO DISTRITO
  VALUES (9, 'La Molina')
INSERT INTO DISTRITO
  VALUES (10, 'La Victoria')
INSERT INTO DISTRITO
  VALUES (11, 'Lince')
INSERT INTO DISTRITO
  VALUES (12, 'Magdalena del Mar')
INSERT INTO DISTRITO
  VALUES (13, 'Miraflores')
INSERT INTO DISTRITO
  VALUES (14, 'Pueblo Libre')
INSERT INTO DISTRITO
  VALUES (15, 'Puente Piedra')
INSERT INTO DISTRITO
  VALUES (16, 'Rimac')
INSERT INTO DISTRITO
  VALUES (17, 'San Isidro')
INSERT INTO DISTRITO
  VALUES (18, 'Independencia')
INSERT INTO DISTRITO
  VALUES (19, 'San Juan de Miraflores')
INSERT INTO DISTRITO
  VALUES (20, 'San Luis')
INSERT INTO DISTRITO
  VALUES (21, 'San Martin de Porres')
INSERT INTO DISTRITO
  VALUES (22, 'San Miguel')
INSERT INTO DISTRITO
  VALUES (23, 'Santiago de Surco')
INSERT INTO DISTRITO
  VALUES (24, 'Surquillo')
INSERT INTO DISTRITO
  VALUES (25, 'Villa Maria del Triunfo')
INSERT INTO DISTRITO
  VALUES (26, 'San Juan de Lurigancho')
INSERT INTO DISTRITO
  VALUES (27, 'Santa Rosa')
INSERT INTO DISTRITO
  VALUES (28, 'Los Olivos')
INSERT INTO DISTRITO
  VALUES (29, 'Los Olivos')
INSERT INTO DISTRITO
  VALUES (30, 'Villa El Savador')
INSERT INTO DISTRITO
  VALUES (31, 'Santa Anita')

CREATE TABLE direccionEnvio (
  codigoEnvio int IDENTITY (1,1) NOT NULL,
  direccionI varchar(200) NOT NULL,
  direccionII varchar(200) NULL,
  referencia varchar(200) NOT NULL,
  id int REFERENCES distrito,
  codigoPostal char(5) NOT NULL,
  CONSTRAINT pk_direcionEnvioID PRIMARY KEY CLUSTERED (codigoEnvio)
)
GO

CREATE TABLE categorias
(
codigoCat int primary key not null,
nombre varchar(50) not null
)
GO

INSERT INTO categorias VALUES(1,'Guitarras')
INSERT INTO categorias VALUES(2,'Bajos')
INSERT INTO categorias VALUES(3,'Baterías')
INSERT INTO categorias VALUES(4,'Accesorios')
INSERT INTO categorias VALUES(5,'Sintetizadores')
INSERT INTO categorias VALUES(6,'Servicios')

CREATE TABLE productos
(
codigoProd int IDENTITY(1000,1) not null,
nombre varchar(100) not null,
descripcion varchar(100) not null,
id int references categorias,
stock smallint not null,
precio decimal(6,2) not null,
estado tinyint default 1,
rutaImg varchar(150) not null,
constraint pk_productoID primary key clustered (codigoProd)
)
GO

/*DETALLE DE BOLETA*/
CREATE TABLE detalleBoleta
(
codigoDetalleBol int identity(1,1) not null,
codigoProd int references productos,
monto decimal(10,2) not null,
cantidad int not null,
constraint pk_DetalleBoletaID primary key clustered (codigoDetalleBol)
)
go

/*CABECERA DE BOLETA*/
CREATE TABLE boleta
(
codigoBol int identity(10000,1) not null,
codigoDetalleBol int REFERENCES detalleBoleta,
precioTotal decimal(10,2) not null,
fecha date not null,
constraint pk_boletaID primary key clustered (codigoBol)
)
go

/*PROCEDIMIENTOS ALMACENADOS*/

CREATE OR ALTER PROC sp_insertProduct
@nombre varchar(100),
@descripcion varchar(100),
@idCat int,
@stock int,
@precio decimal(6,2),
@imagen varchar(150)
AS
INSERT INTO productos (nombre,descripcion,id,stock,precio,rutaImg) VALUES(@nombre,@descripcion,@idCat,@stock,@precio,@imagen)
GO

CREATE OR ALTER PROC sp_updateProduct
@nombre varchar(100),
@descripcion varchar(100),
@idCat int,
@stock int,
@precio decimal(6,2),
@codigo int,
@imagen varchar(150)
AS
UPDATE productos SET nombre=@nombre, @descripcion=@descripcion, id=@idCat, stock=@stock, precio=@precio, rutaImg=@imagen where codigoProd=@codigo
GO

CREATE OR ALTER PROC sp_deleteProduct
@codigo int
AS
UPDATE productos SET estado = 0 WHERE codigoProd=@codigo
GO

CREATE OR ALTER PROC sp_listProduct
AS
SELECT * FROM productos WHERE estado = 1
GO

CREATE OR ALTER PROC sp_listCategoria
AS
SELECT * FROM categorias
go

exec sp_listCategoria
go