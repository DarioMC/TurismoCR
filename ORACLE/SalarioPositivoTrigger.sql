-- 8D Trigger que se activa al insertar o modificar el salario de un empleado.

CREATE OR REPLACE TRIGGER SalarioPositivo
  BEFORE INSERT OR UPDATE OF SALARIO
  ON EMPLEADO
  FOR EACH ROW
BEGIN
  IF :new.SALARIO < 0 THEN
    RAISE_APPLICATION_ERROR(-20100, 'Por favor inserte un valor positivo');
  END IF;
END SalarioPositivo;