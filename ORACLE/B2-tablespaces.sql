CREATE TABLESPACE grupoAdrianDarioOscarDaniel_tbl
    DATAFILE 'tbs_grupo.dbf'
        SIZE 8K
        AUTOEXTEND OFF;
    
CREATE TABLESPACE grupoAdrianDarioOscarDaniel_idx
    DATAFILE 'idx_grupo.dbf'
    SIZE 8K
    AUTOEXTEND OFF;
    
CREATE USER egarro 
    IDENTIFIED BY abcd123 
    DEFAULT TABLESPACE grupoAdrianDarioOscarDaniel_tbl;
    
GRANT dba, connect, resource TO egarro;
GRANT CREATE ANY VIEW TO egarro WITH ADMIN OPTION;