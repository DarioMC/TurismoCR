-- 2- B 
CREATE TABLESPACE grupoAdDaOsDan_tbl
    DATAFILE 'tbs_grupo.dbf'
        SIZE 8M
        AUTOEXTEND OFF;
    
CREATE TABLESPACE grupoAdDaOsDan_idx
    DATAFILE 'idx_grupo.dbf'
    SIZE 8M
    AUTOEXTEND OFF;

-- 3 B     
CREATE USER egarro 
    IDENTIFIED BY abcd123 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
    
    
 -- 4 B  
 CREATE USER ochavarria
    IDENTIFIED BY abcd124 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;

CREATE USER dmonestel 
    IDENTIFIED BY abcd125 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
    
CREATE USER dsolis 
    IDENTIFIED BY abcd126 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
            
-- 5 B 
CREATE ROLE ROL_EJECUTA;
--Grant execute TRIGGER to ROL_EJECUTAR;

CREATE ROLE ROL_NOVATO;
grant select any table to ROL_NOVATO;
grant update any table to ROL_NOVATO;
grant insert any table to ROL_NOVATO;

CREATE ROLE ROL_SUPERIOR;
grant select any table to ROL_SUPERIOR with admin option;
grant update any table to ROL_SUPERIOR with admin option;
grant delete any table to ROL_SUPERIOR with admin option;
grant insert any table to ROL_SUPERIOR with admin option;

grant ROL_EJECUTA to egarro; 
grant ROL_NOVATO to ochavarria;
grant ROL_SUPERIOR to dmonestel; 
grant ROL_NOVATO to dsolis;


--GRANT dba, connect, resource TO egarro;
-- GRANT CREATE ANY VIEW TO egarro WITH ADMIN OPTION;
-- No se que hace ... 


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