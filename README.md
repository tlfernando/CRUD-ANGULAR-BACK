create database DDCrudAngular

go

use DDCrudAngular

create table Empleado(
IdEmpleado int primary key identity,
NombreCompleto varchar(50),
Correo varchar(50),
Sueldo decimal(10,2),
FechaContrato date
)

go

insert into Empleado (NombreCompleto, Correo, Sueldo,FechaContrato)
values ('Luis Torres','mail@mail.com', 5000, '2024-09-23')

select * from Empleado


create procedure sp_listaEmpleados
as
begin
	select 
	IdEmpleado,
	NombreCompleto,
	Correo,
	Sueldo,
	CONVERT(char(10),FechaContrato,103)[FechaContrato]
	from Empleado
end

go

create procedure sp_obtenerEmpleado(
@IdEmpleado int
)
as
begin
	select 
	IdEmpleado,
	NombreCompleto,
	Correo,
	Sueldo,
	CONVERT(char(10),FechaContrato,103)[FechaContrato]
	from Empleado where IdEmpleado = @IdEmpleado
end

go

create procedure sp_crearEmpleado(
@NombreCompleto varchar(50),
@Correo varchar(50),
@Sueldo decimal (10,2),
@FechaContrato varchar(10)
)
as 
begin
set dateformat dmy
	insert into Empleado (NombreCompleto, Correo, Sueldo,FechaContrato)
	values (
		@NombreCompleto,@Correo, @Sueldo, convert(date, @FechaContrato)
		)
end

go

create procedure sp_editarEmpleado(
@IdEmpleado int,
@NombreCompleto varchar(50),
@Correo varchar(50),
@Sueldo decimal (10,2),
@FechaContrato varchar(10)
)
as 
begin
set dateformat dmy
	update Empleado
	set
	NombreCompleto = @NombreCompleto,
	Correo = @Correo,
	Sueldo = @Sueldo,
	FechaContrato = convert(date, @FechaContrato)
	where IdEmpleado = @IdEmpleado
end

go

create procedure sp_eliminarEmpleado(
@IdEmpleado int)
as 
begin
	delete from Empleado
	where IdEmpleado = @IdEmpleado
end
