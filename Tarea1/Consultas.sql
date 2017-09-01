--1 C 
select max ( Fecha_CONTRATACION) from Empleado  ; 

select * from Departamento
inner join Empleado on Departamento.ID_Gerente = Empleado.ID_Empleado;

select EXTRACT(YEAR FROM fecha_contratacion)as ano, count(Empleado.ID_Empleado)
from EMPLEADO 
group by EXTRACT(YEAR FROM fecha_contratacion)
having count(Empleado.ID_Empleado)> 1 ;
 

--2 C
  select departamento.nombre_departamento
  from Departamento 
  inner join Empleado on Departamento.ID_Gerente = Empleado.ID_Empleado
  where Por_Comision > 0 ; 

--3 C 

select Nombre, count(Historial_Puesto.ID_Empleado) 
from historial_puesto
inner join Empleado on Historial_Puesto.ID_Empleado = Empleado.ID_Empleado
group by Historial_Puesto.ID_Empleado,empleado.nombre
having EXTRACT(YEAR FROM max(Sysdate)) = EXTRACT(YEAR FROM max(Fecha_Inicio)) and count (Historial_Puesto.ID_Empleado)>1  ; 


-- 4 C 
select Nombre,Apellido, sysdate-historial_puesto.fecha_inicio as dias,historial_puesto.fecha_inicio
from Empleado 
inner join Historial_Puesto on Empleado.ID_Empleado = Historial_Puesto.ID_Empleado;


-- 5 C 
select Nombre, Apellido, avg (Salario) 
from Empleado 
where Por_Comision >0 ;
-- A que se refiere con el promedio ?? 