create table Region (ID_Region int not null,
                        Nombre varchar(100) not null, 
                        CONSTRAINT Region_pk PRIMARY KEY (ID_Region));

create table Pais (ID_Pais int not null,
                    Nombre_Pais varchar(100) not null,
                    ID_Region int not null, 
                    CONSTRAINT Pais_pk PRIMARY KEY (ID_Pais),
                      CONSTRAINT fk_Region FOREIGN KEY (ID_Region)
                              REFERENCES Region(ID_Region));
                              
create table Localizacion (ID_Localizacion int not null, 
                            Direccion varchar(100) not null,
                            Codigo_Postal int not null,
                            Ciudad varchar(100) not null,
                            Provincia varchar (100) not null,
                            ID_Pais int not null, 
                            CONSTRAINT Localizacion_pk PRIMARY KEY (ID_Localizacion),
                            CONSTRAINT fk_Pais FOREIGN KEY (ID_Localizacion)
                              REFERENCES Pais(ID_Pais));
                              
create table Departamento (ID_Departamento int not null,
                            Nombre_Departamento varchar(100) not null,
                            ID_Gerente int not null,
                            ID_Localizacion int not null, 
                            CONSTRAINT Departamento_pk PRIMARY KEY (ID_Departamento),
                            CONSTRAINT fk_Localizacion FOREIGN KEY (ID_Localizacion)
                              REFERENCES Localizacion(ID_Localizacion));
                              --CONSTRAINT fk_Empleado FOREIGN KEY (ID_Empleado)
                              --REFERENCES Empleado(ID_Empleado));
                              
create table Puesto (ID_Puesto int not null, 
                      Titulo_Puesto varchar(100) not null,
                      Salario_Min int not null,
                      Salario_Max int not null,
                      CONSTRAINT Puesto_pk PRIMARY KEY (ID_Puesto));


create table Empleado (ID_Empleado int not null,
                        Nombre varchar(100) not null,
                        Apellido varchar(100) not null,
                        Email varchar(100) not null,
                        Telefono int not null, 
                        Fecha_Contratacion date not null,
                        ID_Puesto int not null, 
                        Salario int not null, 
                        Por_Comision int not null, 
                        ID_Gerente int not null, 
                        ID_Departamento int not null, 
                        CONSTRAINT Empleado_pk PRIMARY KEY (ID_Empleado), 
                        
                        CONSTRAINT fk_Puesto2 FOREIGN KEY (ID_Puesto)
                              REFERENCES Puesto(ID_Puesto),
                              
                        CONSTRAINT fk_Departamento2 FOREIGN KEY (ID_Departamento)
                              REFERENCES Departamento(ID_Departamento),
                              
                      CONSTRAINT fk_Empleado FOREIGN KEY (ID_Empleado)
                              REFERENCES Empleado(ID_Empleado));
                              
create table Historial_Puesto ( ID_Empleado int not null, 
                                Fecha_Inicio date not null,
                                ID_Puesto int not null, 
                                ID_Departamento int not null, 
                                CONSTRAINT Historial_pk PRIMARY KEY (ID_Empleado,ID_Puesto),
                                
                                CONSTRAINT fk_Departamento FOREIGN KEY (ID_Departamento)
                              REFERENCES Departamento(ID_Departamento),
                              
                              CONSTRAINT fk_Puesto FOREIGN KEY (ID_Puesto)
                              REFERENCES Puesto(ID_Puesto),
                              
                              CONSTRAINT fk_Empleado2 FOREIGN KEY (ID_Empleado)
                              REFERENCES Empleado(ID_Empleado));
                                

drop table Region; 
drop table Pais;
drop table Localizacion; 
drop table Puesto; 
drop table Departamento; 
drop table Historial_Puesto; 
drop table Empleado;