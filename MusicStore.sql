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

CREATE TABLE puestos (
  id int NOT NULL PRIMARY KEY,
  nombre varchar(20) NOT NULL
)
GO

INSERT INTO puestos
  VALUES (1, 'Trabajador')
INSERT INTO puestos
  VALUES (2, 'Cliente')

CREATE TABLE usuarios (
  codigo int IDENTITY (1, 1) NOT NULL,
  nombre varchar(25) NOT NULL,
  apellido varchar(25) NOT NULL,
  telefono char(9) NULL,
  direccion varchar(200) NULL,
  email varchar(100) NULL UNIQUE,
  clave varchar(150) NOT NULL,  
  dni char(8) NOT NULL UNIQUE,
  estado INT NOT NULL DEFAULT 1,
  id int not null references puestos default 2,
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
  codigo int not null references usuarios,	
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
Go

CREATE TABLE productos
(
codigoProd int not null,
nombre varchar(100) not null,
descripcion varchar(100) not null,
codigoCat int,
stock int not null,
precio decimal(6,2) not null,
estado int default 1,
rutaImg varchar(255),
constraint pk_productoID primary key clustered (codigoProd)
)
GO

alter table productos add constraint fk_cat foreign key (codigoCat) references categorias(codigoCat)
go

/*CABECERA DE BOLETA*/
CREATE TABLE boleta
(
codigoBol int identity(10000,1) not null,
precioTotal decimal(10,2) not null,
fecha date not null,
codigo int not null references usuarios,	
constraint pk_boletaID primary key clustered (codigoBol)
)
go

/*DETALLE DE BOLETA*/
CREATE TABLE detalleBoleta
(
codigoBol int references boleta,
codigoProd int references productos,
monto decimal(10,2) not null,
cantidad int not null,
constraint pk_BoleProd primary key clustered (codigoBol,codigoProd)
)
go

/*PROCEDIMIENTOS ALMACENADOS*/

CREATE OR ALTER PROC sp_insertProduct
@codigo int,
@nombre varchar(100),
@descripcion varchar(100),
@idCat int,
@stock int,
@precio decimal(6,2),
@estado tinyint = 1,
@imagen varchar(255)
AS
INSERT INTO productos VALUES(@codigo,@nombre,@descripcion,@idCat,@stock,@precio,@estado,@imagen)
GO

CREATE OR ALTER PROC sp_updateProduct
@nombre varchar(100),
@descripcion varchar(100),
@idCat int,
@stock int,
@precio decimal(6,2),
@estado int,
@codigo int,
@imagen varchar(255)
AS
UPDATE productos SET nombre=@nombre, descripcion=@descripcion, codigoCat=@idCat, stock=@stock, precio=@precio, estado=@estado,rutaImg=@imagen where codigoProd=@codigo
GO

CREATE OR ALTER PROC sp_deleteProduct
@codigo int
AS
UPDATE productos SET estado = 0 WHERE codigoProd=@codigo
GO

CREATE OR ALTER PROC sp_listProduct
AS
SELECT codigoProd,p.nombre, descripcion,c.nombre,p.codigoCat,stock,precio,estado,rutaImg 
FROM productos p join categorias  c on p.codigoCat=c.codigoCat  
WHERE estado = 1
GO

CREATE OR ALTER PROC sp_listCategoria
AS
SELECT * FROM categorias
go

exec sp_listCategoria
go

-- Sirve para mostrar informacion de las compras contenidas en una boleta
CREATE OR ALTER PROCEDURE sp_detalle_bol_producto
  @codigoBol INT
AS
  SELECT b.codigoBol, d.codigoProd, p.descripcion, p.rutaImg,
         d.cantidad,d.monto
  FROM boleta b
  INNER JOIN detalleBoleta d
    ON b.codigoBol = d.codigoBol
  INNER JOIN productos p
    ON d.codigoProd = p.codigoProd
  WHERE d.codigoBol = @codigoBol
GO

--Procedures de creación, modificación, ingreso y eliminación de un usuario
create or alter proc sp_createUser
@nombre varchar(25),
@apellido varchar(25),
@telefono char(9),
@direccion varchar(100),
@email varchar(100),
@clave varchar(150),
@dni char(8)
as
	begin
		if not exists(select * from usuarios where dni=@dni)
		insert into usuarios (nombre,apellido,telefono,direccion,email,clave,dni,estado,id)
		values(@nombre,@apellido,@telefono,@direccion,@email,@clave,@dni,DEFAULT,DEFAULT)
		else
		print 'Usuario con DNI ingresado ya existe'
	end
go

create or alter proc sp_login
@email varchar(100),
@clave varchar(150)
as
	begin
		if exists(select * from usuarios where email = @email and clave = @clave)
			select * from usuarios
		else
			print 'no existe'
	end
go

exec sp_login '',''
go

CREATE OR ALTER PROC sp_generar_boleta
 @precioTotal decimal(10,2),
 @codigoCliente int,
 @n int output
AS
BEGIN
 INSERT boleta(precioTotal, fecha, codigo)
   VALUES (@precioTotal, GETDATE(), @codigoCliente)

 SELECT @n = SCOPE_IDENTITY()

 RETURN @n
END
GO

CREATE OR ALTER PROC sp_agregar_detalle_boleta
 @codigoBol INT,
 @codigoProd INT,
 @importe DECIMAL(10,2),
 @cantidad INT
AS
BEGIN
	INSERT INTO detalleBoleta VALUES 
	  (@codigoBol, @codigoProd, @importe, @cantidad)

	UPDATE productos
		SET stock -= @cantidad
	WHERE codigoProd = @codigoProd
END
GO

EXEC sp_createUser 'Admin', 'Admin', '3334444', 'Soy un Admin 123', 'admin@gmail.com', '123456', '12345678'
EXEC sp_createUser 'Jose', 'Robles', '4445555', 'Buenos Aires 322', 'jose2021@gmail.com', '123456', '87654321'
GO

UPDATE usuarios SET id = 2 WHERE codigo = 2
GO

select * from usuarios