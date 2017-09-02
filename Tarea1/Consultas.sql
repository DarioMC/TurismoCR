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

------6C. Muestre todos los nombres de empleados que llevan más de un año trabajando y su salario sea más de $10000.
SELECT Nombre from Empleado
where Salario > 10000 AND Fecha_Contratacion < (SYSDATE - 365);

-----7C. Despliegue todos los trabajos para los cuales se contrató personal en el último año.

SELECT Puesto.Titulo_Puesto FROM PUESTO
INNER JOIN Empleado 
ON PUESTO.ID_PUESTO = Empleado.ID_PUESTO
---WHERE EMPLEADO.FECHA_CONTRATACION > SYSDATE - 1
WHERE EMPLEADO.FECHA_CONTRATACION BETWEEN SYSDATE -1 AND SYSDATE;
SELECT Puesto.Titulo_Puesto FROM PUESTO
INNER JOIN Empleado 
ON PUESTO.ID_PUESTO = Empleado.ID_PUESTO
WHERE EMPLEADO.FECHA_CONTRATACION BETWEEN (SYSDATE - 366) AND SYSDATE;

-----8C. Despliegue toda la localización geográfica, país, ciudad y departamento, para aquellosdepartamentos que tienen más de 10 empleados.

SELECT DISTINCT Pais.Nombre_Pais, Localizacion.Ciudad, Departamento.Nombre_Departamento
FROM Pais INNER JOIN Localizacion 
ON Pais.ID_Pais = Localizacion.ID_Pais
INNER JOIN Departamento
ON Departamento.ID_Localizacion = Localizacion.ID_Localizacion
INNER JOIN Empleado
ON Empleado.ID_Departamento = Departamento.ID_Departamento
WHERE Empleado.ID_Departamento IN (
      SELECT Empleado.ID_Departamento FROM Empleado
      GROUP BY Empleado.ID_Departamento
      HAVING COUNT(*) > 1   ----10
  );
-----9C. Despliegue todas las personas que tienen al menos 10 personas a cargo.
      SELECT DISTINCT Empleado.ID_Gerente FROM Empleado
      ---GROUP BY Empleado.ID_Empleado
      WHERE Empleado.ID_Gerente IN (
      SELECT Empleado.ID_Gerente FROM Empleado
      GROUP BY Empleado.ID_Gerente
      HAVING COUNT(*) > 1   ----10
  );

----1D. Incremente el salario de los empleados en un 10%, si han trabajado en la empresa más de 10 años,
----un 5%, más de 5 años, 2%, en cualquier otro caso.
/*
     UPDATE Empleado SET Salario = ROUND( Salario * 1.20, 0 )
     
     SELECT Empleado.ID_Empleado, (CASE  Empleado.Salario
      WHEN 4800 THEN 4850
      ELSE Empleado.Salario
      END) AS Salario_Actualizado  FROM Empleado;
  */    
 /*     
 SELECT Salario  FROM Empleado  
WHERE EMPLEADO.FECHA_CONTRATACION < (SYSDATE - 3653)
OR
 EMPLEADO.FECHA_CONTRATACION < (SYSDATE - 1826);
 */
    /*   
BEGIN     
IF EMPLEADO.FECHA_CONTRATACION < (SYSDATE - 3653) THEN
   SELECT Salario FROM Empleado;
IF EMPLEADO.FECHA_CONTRATACION < (SYSDATE - 1826) THEN
   SELECT Nombre Fecha_Contratacion FROM Empleado;
END IF;
SELECT * FROM Pais;     */
CREATE OR REPLACE PROCEDURE Aumento_Salario
IS 
BEGIN 
  FOR Empleado IN (SELECT ID_Empleado ,FECHA_CONTRATACION  FROM EMPLEADO) LOOP
    IF Empleado.FECHA_CONTRATACION <= (SYSDATE - 3653)
        THEN  UPDATE Empleado SET Salario = ROUND( (Salario * 0.10) + Salario, 0 )WHERE ID_EMPLEADO = Empleado.ID_EMPLEADO  ;
    elsif Empleado.FECHA_CONTRATACION <= (SYSDATE - 1826)  
      then UPDATE Empleado SET Salario = ROUND( (Salario * 0.05) + Salario, 0 )WHERE ID_EMPLEADO = Empleado.ID_EMPLEADO;
    else 
      UPDATE Empleado SET Salario = ROUND( (Salario * 0.02) + Salario, 0 )WHERE ID_EMPLEADO = Empleado.ID_EMPLEADO;
  
    END IF; 
  END LOOP;
END Aumento_Salario;

/*
SELECT (SYSDATE - 365) AS RESULTADO  FROM DUAL;

select Salario, FECHA_CONTRATACION from empleado;
EXEC Aumento_Salario;
*/