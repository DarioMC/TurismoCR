--Procedimiento almacenado que cambia al manager de un departamento por el empleado con más antiguo del mismo.

CREATE OR REPLACE
PROCEDURE Actualiza_ManagerDepartamento(idDepartamento NUMBER)
IS

BEGIN

  UPDATE DEPARTAMENTO
  SET ID_GERENTE = (SELECT ID_EMPLEADO FROM EMPLEADO WHERE ID_DEPARTAMENTO = idDepartamento AND FECHA_CONTRATACION > ALL(
    SELECT FECHA_CONTRATACION FROM EMPLEADO WHERE ID_DEPARTAMENTO = idDepartamento))
  WHERE ID_DEPARTAMENTO = idDepartamento;

END Actualiza_ManagerDepartamento;


