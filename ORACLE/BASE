/*
 * Creacion de tablas.
 * Oscar Chavarria
*/

CREATE TABLE Region (ID_Region INT NOT NULL,
    Nombre varchar(100) NOT NULL, 
    CONSTRAINT Region_pk PRIMARY KEY (ID_Region));

CREATE TABLE Pais (ID_Pais INT NOT NULL,
    Nombre_Pais VARCHAR(100) NOT NULL,
    ID_Region INT NOT NULL, 
    CONSTRAINT Pais_pk PRIMARY KEY (ID_Pais),
    CONSTRAINT fk_Region FOREIGN KEY (ID_Region) REFERENCES Region(ID_Region));
                              
CREATE TABLE Localizacion (ID_Localizacion INT NOT NULL, 
    Direccion varchar(100) NOT NULL,
    Codigo_Postal INT NOT NULL,
    Ciudad varchar(100) NOT NULL,
    Provincia varchar (100) NOT NULL,
    ID_Pais INT NOT NULL, 
    CONSTRAINT Localizacion_pk PRIMARY KEY (ID_Localizacion),
    CONSTRAINT fk_Pais FOREIGN KEY (ID_Localizacion) REFERENCES Pais(ID_Pais));
                              
CREATE TABLE Departamento (ID_Departamento INT NOT NULL,
    Nombre_Departamento varchar(100) NOT NULL,
    ID_Gerente INT NOT NULL,
    ID_Localizacion INT NOT NULL, 
    CONSTRAINT Departamento_pk PRIMARY KEY (ID_Departamento),
    CONSTRAINT fk_Localizacion FOREIGN KEY (ID_Localizacion) REFERENCES Localizacion(ID_Localizacion));
                              
CREATE TABLE Puesto (ID_Puesto INT NOT NULL, 
    Titulo_Puesto varchar(100) NOT NULL,
    Salario_Min INT NOT NULL,
    Salario_Max INT NOT NULL,
    CONSTRAINT Puesto_pk PRIMARY KEY (ID_Puesto));

CREATE TABLE Empleado (ID_Empleado INT NOT NULL,
    Nombre varchar(100) NOT NULL,
    Apellido varchar(100) NOT NULL,
    Email varchar(100) NOT NULL,
    Telefono INT NOT NULL, 
    Fecha_Contratacion date NOT NULL,
    ID_Puesto INT NOT NULL, 
    Salario INT NOT NULL, 
    Por_Comision INT NOT NULL, 
    ID_Gerente INT NOT NULL, 
    ID_Departamento INT NOT NULL, 
    CONSTRAINT Empleado_pk PRIMARY KEY (ID_Empleado),
    CONSTRAINT fk_Puesto2 FOREIGN KEY (ID_Puesto) REFERENCES Puesto(ID_Puesto),
    CONSTRAINT fk_Departamento2 FOREIGN KEY (ID_Departamento) REFERENCES Departamento(ID_Departamento),
    CONSTRAINT fk_Empleado FOREIGN KEY (ID_Empleado) REFERENCES Empleado(ID_Empleado));
                              
CREATE TABLE Historial_Puesto (ID_Empleado INT NOT NULL, 
    Fecha_Inicio date NOT NULL,
    ID_Puesto INT NOT NULL, 
    ID_Departamento INT NOT NULL, 
    CONSTRAINT Historial_pk PRIMARY KEY (ID_Empleado,ID_Puesto,ID_Departamento),
    CONSTRAINT fk_Departamento FOREIGN KEY (ID_Departamento) REFERENCES Departamento(ID_Departamento),
    CONSTRAINT fk_Puesto FOREIGN KEY (ID_Puesto) REFERENCES Puesto(ID_Puesto),
    CONSTRAINT fk_Empleado2 FOREIGN KEY (ID_Empleado) REFERENCES Empleado(ID_Empleado));
                                
DROP TABLE Region; 
DROP TABLE Pais;
DROP TABLE Localizacion; 
DROP TABLE Puesto; 
DROP TABLE Departamento; 
DROP TABLE Historial_Puesto; 
DROP TABLE Empleado;
