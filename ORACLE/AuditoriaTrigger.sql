--Tabla Auditoria necesaria para llevar un orden en los cambios al salario.

CREATE TABLE AUDITORIA(FECHA_CAMBIO DATE NOT NULL,
  USUARIO VARCHAR2(25) NOT NULL, 
  SALARIO_OLD INT NOT NULL, 
  SALARIO_NEW INT NOT NULL);
  

--Trigger Auditoria, inserta como tipo log, todos los cambios que se hagan al salario.

CREATE OR REPLACE TRIGGER AuditoriaTrigger BEFORE UPDATE
  ON EMPLEADO FOR EACH ROW
BEGIN
  IF UPDATING(SALARIO) THEN
    INSERT INTO AUDITORIA
    VALUES(SYSDATE, USER, :old.SALARIO, :new.SALARIO);
  END IF;
END AuditoriaTrigger;
