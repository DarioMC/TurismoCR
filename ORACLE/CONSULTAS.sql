/*
 * Bases de datos II 
 * Tarea 1 ORACLE
 * Integrantes: 
     - Adrian Garro, 
     - Dario Monestel, 
     - Oscar Chavarria, 
     - Daniel Solis.
 * II Semestre, 2017
 *
 */

-- 1B
/*
    Respuesta:
    - No se puede garantizar que se cumpla la primera forma normal, puesto que 
      en el modelo de la base de datos no se puede apreciar que los atributos 
      tengan solo valores at?micos.

    - Al parecer no se cumple la segunda forma normal, puesto que el 
      id_gerente en la tabla empleado no tiene ninguna relaci?n con la llave primaria.

    - En la tabla localizacion hay redudancia de datos, el codigo postal aporta la
      misma informaci?n que los atributos provincia y ciudad.
*/

-- 2B 
CREATE TABLESPACE grupoAdDaOsDan_tbl
    DATAFILE 'tbs_grupo.dbf'
    SIZE 8M
    AUTOEXTEND OFF;
    
CREATE TABLESPACE grupoAdDaOsDan_idx
    DATAFILE 'idx_grupo.dbf'
    SIZE 8M
    AUTOEXTEND OFF;

ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;

-- 3B     
CREATE USER egarro 
    IDENTIFIED BY abcd123 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
    
-- 4B  
CREATE USER ochavarria
    IDENTIFIED BY abcd124 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;

CREATE USER dmONestel 
    IDENTIFIED BY abcd125 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
    
CREATE USER dsolis 
    IDENTIFIED BY abcd126 
    DEFAULT TABLESPACE grupoAdDaOsDan_tbl;
            
-- 5B 
CREATE ROLE ROL_EJECUTA;
--GRANT EXECUTE TRIGGER to ROL_EJECUTAR;

CREATE ROLE ROL_NOVATO;
GRANT SELECT any table to ROL_NOVATO;
GRANT UPDATE any table to ROL_NOVATO;
GRANT INSERT any table to ROL_NOVATO;

CREATE ROLE ROL_SUPERIOR;
GRANT SELECT any table to ROL_SUPERIOR with admin option;
GRANT UPDATE any table to ROL_SUPERIOR with admin option;
GRANT DELETE any table to ROL_SUPERIOR with admin option;
GRANT INSERT any table to ROL_SUPERIOR with admin option;

GRANT ROL_EJECUTA to egarro; 
GRANT ROL_NOVATO to ochavarria;
GRANT ROL_SUPERIOR to dmONestel; 
GRANT ROL_NOVATO to dsolis;

--1C 
SELECT MAX(Fecha_CONTRATACION) FROM Empleado; 

SELECT * FROM Departamento
    INNER JOIN Empleado ON Departamento.ID_Gerente = Empleado.ID_Empleado;

SELECT EXTRACT (YEAR FROM fecha_contratacion) as ano, count(Empleado.ID_Empleado)
    FROM EMPLEADO 
    GROUP BY EXTRACT(YEAR FROM fecha_contratacion)
    HAVING COUNT(Empleado.ID_Empleado) > 1 ;

--2C
SELECT departamento.nombre_departamento
    FROM Departamento 
    INNER JOIN Empleado ON Departamento.ID_Gerente = Empleado.ID_Empleado
    WHERE Por_Comision > 0; 

--3C 
SELECT Nombre, COUNT(Historial_Puesto.ID_Empleado) 
    FROM historial_puesto
    INNER JOIN Empleado ON Historial_Puesto.ID_Empleado = Empleado.ID_Empleado
    GROUP BY Historial_Puesto.ID_Empleado,empleado.nombre
    HAVING EXTRACT(YEAR FROM MAX(Sysdate)) = EXTRACT(YEAR FROM MAX(Fecha_Inicio)) 
    AND COUNT (Historial_Puesto.ID_Empleado) > 1; 


-- 4C 
SELECT Nombre,Apellido, sysdate-historial_puesto.fecha_inicio 
    AS dias, historial_puesto.fecha_inicio
    FROM Empleado 
    INNER JOIN Historial_Puesto 
    ON Empleado.ID_Empleado = Historial_Puesto.ID_Empleado;

-- 5C 
SELECT Nombre, Apellido, AVG(Salario) 
    FROM Empleado 
    WHERE Por_Comision > 0;

-- 6C
SELECT Nombre FROM Empleado
    WHERE Salario > 10000 AND Fecha_Contratacion < (SYSDATE - 365);

-- 7C
SELECT Puesto.Titulo_Puesto FROM PUESTO
    INNER JOIN Empleado 
    ON PUESTO.ID_PUESTO = Empleado.ID_PUESTO
    WHERE EMPLEADO.FECHA_CONTRATACION BETWEEN SYSDATE -1 AND SYSDATE;
    SELECT Puesto.Titulo_Puesto FROM PUESTO
    INNER JOIN Empleado 
    ON PUESTO.ID_PUESTO = Empleado.ID_PUESTO
    WHERE EMPLEADO.FECHA_CONTRATACION BETWEEN (SYSDATE - 366) AND SYSDATE;

-- 8C
SELECT DISTINCT Pais.Nombre_Pais, Localizacion.Ciudad, Departamento.Nombre_Departamento
    FROM Pais INNER JOIN LocalizaciON 
    ON Pais.ID_Pais = LocalizaciON.ID_Pais
    INNER JOIN Departamento
    ON Departamento.ID_Localizacion = Localizacion.ID_LocalizaciON
    INNER JOIN Empleado
    ON Empleado.ID_Departamento = Departamento.ID_Departamento
    WHERE Empleado.ID_Departamento IN (
        SELECT Empleado.ID_Departamento FROM Empleado
        GROUP BY Empleado.ID_Departamento
        HAVING COUNT(*) > 1
    );
    
-- 9C
SELECT DISTINCT Empleado.ID_Gerente FROM Empleado
    WHERE Empleado.ID_Gerente IN (
        SELECT Empleado.ID_Gerente FROM Empleado
        GROUP BY Empleado.ID_Gerente
        HAVING COUNT(*) > 1
    );

-- 1D
CREATE OR REPLACE PROCEDURE Aumento_Salario
IS 
BEGIN 
    FOR Empleado IN (SELECT ID_Empleado ,Fecha_Contratacion  FROM Empleado) LOOP
        IF Empleado.Fecha_CONtrataciON <= (SYSDATE - 3653)
            THEN  UPDATE Empleado SET Salario = ( (Salario * 0.10) + Salario ) 
            WHERE ID_Empleado = Empleado.ID_Empleado;
        EXIT;
        ELSIF Empleado.Fecha_Contratacion BETWEEN (SYSDATE - 3653) AND (SYSDATE - 1826)  
            THEN UPDATE Empleado SET Salario = ( (Salario * 0.05) + Salario) 
            WHERE ID_Empleado = Empleado.ID_Empleado;
        EXIT;
        ELSIF Empleado.Fecha_CONtrataciON > (SYSDATE - 1826)
            THEN UPDATE Empleado SET Salario = ( (Salario * 0.02) + Salario )
            WHERE ID_Empleado = Empleado.ID_Empleado;
        EXIT;
        ELSE 
            EXIT;
        END IF; 
    END LOOP;
END Aumento_Salario;

SELECT Salario, FECHA_CONTRATACION FROM empleado;
-- EXEC Aumento_Salario;

-- 2D
DECLARE
    yr NUMBER;
    yr_aux NUMBER;
    cntrcts NUMBER;
    cntrcts_aux NUMBER;
BEGIN
    yr := 0;
    cntrcts := 0;
    -- Get year with more contracts.
    FOR rw IN (
        -- Get table with columns: year and number of contracts
        SELECT EXTRACT(YEAR FROM TO_DATE(FECHA_CONTRATACION)) AS Year, COUNT(*) AS Contracts
        FROM EMPLEADO
        GROUP BY EXTRACT(YEAR FROM TO_DATE(FECHA_CONTRATACION))
    ) LOOP
        yr_aux := rw.Year;
        cntrcts_aux := rw.Contracts;
        IF (cntrcts_aux > cntrcts) THEN
            yr := yr_aux;
            cntrcts := cntrcts_aux;
        END IF;
    END LOOP;
    dbms_output.put_line('El año con más contrataciones es: ' || yr);
    -- Number of contracts by month in that year.
    FOR rw IN (
        SELECT EXTRACT(MONTH FROM TO_DATE(FECHA_CONTRATACION)) AS Month, COUNT(*) AS Contracts
        FROM EMPLEADO
        WHERE EXTRACT(YEAR FROM TO_DATE(FECHA_CONTRATACION)) = yr
        GROUP BY EXTRACT(MONTH FROM TO_DATE(FECHA_CONTRATACION))
    ) LOOP
        IF (rw.Month = 1) THEN
            dbms_output.put_line('Durante el mes de enero se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 2) THEN
            dbms_output.put_line('Durante el mes de febrero se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 3) THEN
            dbms_output.put_line('Durante el mes de marzo se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 4) THEN
            dbms_output.put_line('Durante el mes de abril se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 5) THEN
            dbms_output.put_line('Durante el mes de mayo se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 6) THEN
            dbms_output.put_line('Durante el mes de junio se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 7) THEN
            dbms_output.put_line('Durante el mes de julio se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 8) THEN
            dbms_output.put_line('Durante el mes de agosto se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 9) THEN
            dbms_output.put_line('Durante el mes de setiembre se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 10) THEN
            dbms_output.put_line('Durante el mes de octubre se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 11) THEN
            dbms_output.put_line('Durante el mes de noviembre se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
        IF (rw.Month = 12) THEN
            dbms_output.put_line('Durante el mes de diciembre se realizaron ' || rw.Contracts || ' contrataciones.');
        END IF;
    END LOOP;
END;

-- 3D
DECLARE
  employee_name VARCHAR2(32767);
  employee_last_name VARCHAR2(32767);
  job_title VARCHAR2(32767);
  date_hire VARCHAR2(32767);
BEGIN
  FOR rw IN (
    SELECT EMPLEADO.NOMBRE, EMPLEADO.APELLIDO, PUESTO.TITULO_PUESTO, FECHA_INICIO
    FROM HISTORIAL_PUESTO
    INNER JOIN EMPLEADO ON EMPLEADO.ID_EMPLEADO = HISTORIAL_PUESTO.ID_EMPLEADO
    INNER JOIN PUESTO ON PUESTO.ID_PUESTO = HISTORIAL_PUESTO.ID_PUESTO
    WHERE (HISTORIAL_PUESTO.ID_PUESTO, FECHA_INICIO)
    IN (SELECT ID_PUESTO, MIN(FECHA_INICIO) FROM HISTORIAL_PUESTO GROUP BY ID_PUESTO)
  ) LOOP
      employee_name := rw.NOMBRE;
      employee_last_name := rw.APELLIDO;
      job_title := rw.TITULO_PUESTO;
      date_hire := TO_CHAR(rw.FECHA_INICIO);
      dbms_output.put_line(
          'El primer empleado ' || job_title || ' contratado ' ||
          'es ' || employee_name || ' ' || employee_last_name ||
          ', se contrato en la fecha: ' || date_hire
      );
  END LOOP;
END;

-- 6D
CREATE OR REPLACE
PROCEDURE Actualiza_ManagerDepartamento(idDepartamento NUMBER)
IS
BEGIN
    UPDATE DEPARTAMENTO
    SET ID_GERENTE = (SELECT FIRST ID_EMPLEADO FROM EMPLEADO 
        WHERE ID_DEPARTAMENTO = idDepartamento AND FECHA_CONTRATACION > ALL(
            SELECT FECHA_CONTRATO FROM EMPLEADO WHERE ID_DEPARTAMENTO = idDepartamento)
        )
    WHERE ID_DEPARTAMENTO = idDepartamento;
END Actualiza_ManagerDepartamento;

-- 7D
CREATE OR REPLACE
PROCEDURE Salario_DescendienteDepartamento( idDepartamento NUMBER)
IS
BEGIN
    SELECT NOMBRE, APELLIDO, SALARIO FROM EMPLEADO
    WHERE ID_DEPARTAMENTO = idDepartamento
    ORDER BY SALARIO DESC;
END Salario_DescendienteDepartamento;

-- 8D
CREATE OR REPLACE TRIGGER SalarioPositivo
    BEFORE INSERT OR UPDATE OF SALARIO
    ON EMPLEADO
    FOR EACH ROW
BEGIN
    IF :new.SALARIO < 0 THEN
        RAISE_APPLICATION_ERROR(-20100, 'Por favor inserte un valor positivo');
    END IF;
END SalarioPositivo;

--9D
CREATE TABLE AUDITORIA(FECHA_CAMBIO DATE NOT NULL,
    USUARIO VARCHAR2(25) NOT NULL, 
    SALARIO_OLD INT NOT NULL, 
    SALARIO_NEW INT NOT NULL);

CREATE OR REPLACE TRIGGER AuditoriaTrigger BEFORE UPDATE
    ON EMPLEADO FOR EACH ROW
BEGIN
    IF UPDATING(SALARIO) THEN
        INSERT INTO AUDITORIA
        VALUES(SYSDATE, USER, :old.SALARIO, :new.SALARIO);
    ELSIF UPDATING THEN
        INSERT INTO AUDITORIA
        VALUES(SYSDATE, USER, :old.key, :new.key);
    END IF;
END AuditoriaTrigger;

