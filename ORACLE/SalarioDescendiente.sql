--7D Procedimiento almacenado que devuelvo los empleados de un departamento ordenados por salario de forma descendente.

CREATE OR REPLACE
PROCEDURE Salario_DescendienteDepartamento( idDepartamento NUMBER)
IS
  empNombre EMPLEADO.NOMBRE%TYPE;
  empApellido EMPLEADO.APELLIDO%TYPE;
  empSalario EMPLEADO.SALARIO%TYPE;
BEGIN
  SELECT NOMBRE, APELLIDO, SALARIO INTO empNombre, empApellido, empSalario FROM EMPLEADO WHERE ID_DEPARTAMENTO = idDepartamento
  ORDER BY SALARIO DESC;

END Salario_DescendienteDepartamento;