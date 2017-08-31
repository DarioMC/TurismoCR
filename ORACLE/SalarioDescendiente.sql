--Procedimiento almacenado que devuelvo los empleados de un departamento ordenados por salario de forma descendente.

CREATE OR REPLACE
PROCEDURE Salario_DescendienteDepartamento( idDepartamento NUMBER)
IS

BEGIN
  SELECT NOMBRE, APELIIDO, SALARIO FROM EMPLEADO WHERE ID_DEPARTAMENTO = idDepartamento
  ORDER BY SALARIO DESC;

END Salario_DescendienteDepartamento;