/*
 * Inserts para la base de datos ORACLE.
 * Dario Monestel Corrella.
 */

-- REGION --
INSERT INTO Region (ID_Region, Nombre)
    VALUES(1, 'BuenosAires');
    
INSERT INTO Region (ID_Region, Nombre)
    VALUES(2, 'SanJose');
    
INSERT INTO Region (ID_Region, Nombre)
    VALUES(3, 'Cali');
    
INSERT INTO Region (ID_Region, Nombre)
    VALUES(4, 'Tokyo');
    
INSERT INTO Region (ID_Region, Nombre)
    VALUES(5, 'Berlin');

-- PAIS --     
INSERT INTO Pais (ID_Pais, Nombre_Pais,ID_Region)
    VALUES(1,'Argentina', 1);
                  
INSERT INTO Pais (ID_Pais, Nombre_Pais,ID_Region)
    VALUES(2,'CostaRica', 2);
     
INSERT INTO Pais (ID_Pais, Nombre_Pais,ID_Region)
    VALUES(3,'Colombia', 3);
    
INSERT INTO Pais (ID_Pais, Nombre_Pais,ID_Region)
    VALUES(4,'japon', 4);
    
INSERT INTO Pais (ID_Pais, Nombre_Pais,ID_Region)
    VALUES(5,'Alemania', 5);
              
-- LOCALIZACION --
INSERT INTO Localizacion (ID_Localizacion, Direccion,
    Codigo_Postal,Ciudad,Provincia,ID_Pais)
    VALUES(1, 'Rotonda Princial',1675, 'Trinidad','Buenos Aires', 1);
    
INSERT INTO Localizacion (ID_Localizacion, Direccion,
    Codigo_Postal,Ciudad,Provincia,ID_Pais)
    VALUES(2, 'Iglesia', 2467, 'Desamparados', 'San Jose', 2);
    
INSERT INTO Localizacion (ID_Localizacion, Direccion, 
    Codigo_Postal,Ciudad,Provincia,ID_Pais)
    VALUES(3, 'Chapinero', 5473, 'Trinidad', 'Cali', 3);
    
INSERT INTO Localizacion (ID_Localizacion, Direccion,
    Codigo_Postal,Ciudad,Provincia,ID_Pais)
    VALUES(4, 'Edificio Central', 9074, 'Kanto','Tokyo', 4);
    
INSERT INTO Localizacion (ID_Localizacion, Direccion,
    Codigo_Postal,Ciudad,Provincia,ID_Pais)
    VALUES(5,'Palacio de Charlottenburg', 6784, 'Lietzenburgo', 'BerlinOct', 5);
        
-- DEPARTAMENTO --
INSERT INTO Departamento(ID_Departamento,Nombre_Departamento, ID_Gerente, ID_Localizacion)
    VALUES(1,'Dise?o',1,1);
    
INSERT INTO Departamento(ID_Departamento,Nombre_Departamento, ID_Gerente, ID_Localizacion)
    VALUES(2,'Cientifico de datos',2,2);
    
INSERT INTO Departamento(ID_Departamento,Nombre_Departamento, ID_Gerente, ID_Localizacion)
    VALUES(3,'Desarrollo',3,3);
    
INSERT INTO Departamento(ID_Departamento,Nombre_Departamento, ID_Gerente, ID_Localizacion)
    VALUES(4,'Pruebas',4,4);
    
INSERT INTO Departamento(ID_Departamento,Nombre_Departamento, ID_Gerente, ID_Localizacion)
    VALUES(5,'Logistica',5,5);
          
-- PUESTO --
INSERT INTO Puesto (ID_Puesto ,Titulo_Puesto,Salario_Min, Salario_Max)
    VALUES(1,'Dise?ador', 1000, 1500);
            
INSERT INTO Puesto (ID_Puesto ,Titulo_Puesto,Salario_Min, Salario_Max)
    VALUES(2,'Analista', 6000, 9000);

INSERT INTO Puesto (ID_Puesto ,Titulo_Puesto,Salario_Min, Salario_Max)
    VALUES(3,'Desarrollador', 2000, 4500);
            
INSERT INTO Puesto (ID_Puesto ,Titulo_Puesto,Salario_Min, Salario_Max)
    VALUES(4,'Ensayador', 1000, 1200);
        
INSERT INTO Puesto (ID_Puesto ,Titulo_Puesto,Salario_Min, Salario_Max)
    VALUES(5,'Logista',3500,5000);
            
-- EMPLEADOS --
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(1,'Santiago', 'Rivera', 'Stiago78@gmail.com', 59748493,'11-11-2000',
        1, 1000, 0, 1, 1);
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(2,'Willian','Longe','WLonje89@gmail.com',64837561,'01-04-2017',
        2, 8000, 700, 2, 2);
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(3,'Andres','Rodriguez','Arod46@gmail.com',90473721,'23-02-2016',
        3, 4000, 200, 3, 3);
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(4, 'Xion', 'Park', 'XPark92@gmail.com', 28836417, '02-01-2017',
        4, 1000, 3000, 4, 4);
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(5, 'Henry', 'Hitler', 'hhiler91@gmail.com', 42558582, '30-10-2005',
        2, 4800, 2000, 5, 5);
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(6, 'Gengis', 'Kanh', 'genK78@gmail.com', 65338578, '02-02-2014',
        2, 11000, 1000, 2, 2);
                             
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(7, 'John', 'Smith', 'jsmith29@gmail.com', 76554383, '11-05-2013',
        3, 3800, 1200, 3, 3); 
        
INSERT INTO Empleado (ID_Empleado,Nombre,Apellido,Email,Telefono,Fecha_Contratacion,
    ID_Puesto,Salario,Por_Comision ,ID_Gerente, ID_Departamento)
    VALUES(8, 'Alison', 'Smith', 'ally69@gmail.com', 89344383, '11-04-2012',
        3, 3800, 1000, 3, 3); 
                     
-- HISTORIAL_PUESTO
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio, ID_Puesto, ID_Departamento)
    VALUES(1, '11-11-2000', 1, 1);
 
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio, ID_Puesto, ID_Departamento)
    VALUES(2, '01-04-2017', 2, 2);
              
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio, ID_Puesto, ID_Departamento)
    VALUES(2, '01-05-2018', 3, 3);
              
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(3, '23-02-2016', 3, 3);

INSERT INTO  Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(4, '02-01-2017', 4, 4);
              
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(5, '30-10-2005', 5, 5);
              
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(6, '02-02-2014', 2, 2); 
              
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(7, '11-05-2013', 3, 3);
    
INSERT INTO Historial_Puesto (ID_Empleado, Fecha_Inicio,ID_Puesto,ID_Departamento)
    VALUES(8, '11-04-2012', 3, 3);