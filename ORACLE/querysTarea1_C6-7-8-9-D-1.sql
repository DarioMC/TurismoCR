------6. Muestre todos los nombres de empleados que llevan m�s de un a�o trabajando y su salario sea m�s de $10000.
SELECT Nombre from Empleado
where Salario > 10000 AND Fecha_Contratacion < '31-08-2016';

-----7. Despliegue todos los trabajos para los cuales se contrat� personal en el �ltimo a�o.

SELECT Puesto.Titulo_Puesto FROM PUESTO
INNER JOIN Empleado 
ON PUESTO.ID_PUESTO = Empleado.ID_PUESTO
WHERE EMPLEADO.FECHA_CONTRATACION > '31-08-2016';


-----8. Despliegue toda la localizaci�n geogr�fica, pa�s, ciudad y departamento, para aquellosdepartamentos que tienen m�s de 10 empleados.

SELECT Pais.Nombre_Pais, Localizacion.Ciudad, Departamento.Nombre_Departamento
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
-----9. Despliegue todas las personas que tienen al menos 10 personas a cargo.
      SELECT DISTINCT Empleado.ID_Gerente FROM Empleado
      ---GROUP BY Empleado.ID_Empleado
      WHERE Empleado.ID_Gerente IN (
      SELECT Empleado.ID_Gerente FROM Empleado
      GROUP BY Empleado.ID_Gerente
      HAVING COUNT(*) > 1   ----10
  );

----1. Incremente el salario de los empleados en un 10%, si han trabajado en la empresa m�s de 10 a�os,
----un 5%, m�s de 5 a�os, 2%, en cualquier otro caso.

----UPDATE Empleado SET Salario = ROUND( Salario * 1.20, 2 );

