USE [master];
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'Certificade_GS')
BEGIN
    ALTER DATABASE Certificade_GS SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Certificade_GS;
END;

CREATE DATABASE Certificade_GS;
GO
USE Certificade_GS;
GO

-- CREACIÓN DE TABLAS ---------------------------------------------------

CREATE TABLE EmployeeRole (
    IdEmployeeRole INT PRIMARY KEY IDENTITY(1,1),
    Role VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE EmployeeContractType (
    IdContractType INT PRIMARY KEY IDENTITY(1,1),
    Type VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE EmployeeProfesorCategory (
    IdProfesorCategory INT PRIMARY KEY IDENTITY(1,1),
    Category VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE Career (
    IdCareer INT PRIMARY KEY IDENTITY (1,1),
    Career VARCHAR(50) NOT NULL
);

CREATE TABLE Employee (
    IdEmployee INT PRIMARY KEY IDENTITY(1,1),
    Tuition VARCHAR(40) NOT NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Gender CHAR NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    IdRole INT NOT NULL,
    IdContractType INT NOT NULL,
    IdProfesorCategory INT NOT NULL,
    IdCareer INT,

    CONSTRAINT fk_Role_Employee FOREIGN KEY (IdRole) REFERENCES EmployeeRole(IdEmployeeRole),
    CONSTRAINT fk_ContractType_Employee FOREIGN KEY (IdContractType) REFERENCES EmployeeContractType(IdContractType),
    CONSTRAINT fk_ProfesorCategory_Employee FOREIGN KEY (IdProfesorCategory) REFERENCES EmployeeProfesorCategory(IdProfesorCategory),
    CONSTRAINT fk_Career_Employee FOREIGN KEY (IdCareer) REFERENCES Career(IdCareer)
);

CREATE TABLE CertificadeType (
    IdCertificadeType INT PRIMARY KEY IDENTITY(1,1),
    Type VARCHAR(80) NOT NULL UNIQUE,
    Doc VARBINARY(MAX) NOT NULL
);

CREATE TABLE Certificade (
    IdCertifcade INT PRIMARY KEY IDENTITY(1,1),
    DateApplied DATE NOT NULL,
    Doc VARBINARY(MAX),
    IdCertifiedType INT NOT NULL,
    IdProfesor INT NOT NULL,

    CONSTRAINT fk_CertificadeType_Certificade FOREIGN KEY (IdCertifiedType) REFERENCES CertificadeType (IdCertificadeType),
    CONSTRAINT fk_Profesor_Certificade FOREIGN KEY (IdProfesor) REFERENCES Employee (IdEmployee)
);

CREATE TABLE Director (
    IdDirector INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Signature VARBINARY(MAX) NOT NULL
);
-- INSERCIÓN DE DATOS CONSTANTES ---------------------------------------

INSERT INTO EmployeeRole (Role) 
            VALUES ('Administrador'), ('Administrativo'), ('Profesor');

INSERT INTO EmployeeContractType (Type) 
            VALUES ('Sin asignar'),     -- ID = 1
                   ('Tiempo completo'), -- ID = 2
                   ('Medio tiempo'),    -- ID = 3
                   ('Por asignatura'),  -- ID = 4
                   ('Temporal'),        -- ID = 5
                   ('Honorarios');      -- ID = 6

INSERT INTO EmployeeProfesorCategory (Category)
            VALUES ('Sin asignar'),     -- ID = 1
                   ('Titular'),         -- ID = 2
                   ('Asociado'),        -- ID = 3
                   ('Asistente'),       -- ID = 4
                   ('Adjunto'),         -- ID = 5
                   ('Sustituto');       -- ID = 6

INSERT INTO Career (Career)
            VALUES ('Ingeniería de software (2014)'),           -- ID = 1
                   ('Ingeniería de software (2023)'),           -- ID = 1
                   ('Tecnologías computacionales (2014)'),      -- ID = 2
                   ('Tecnologías computacionales (2023)');      -- ID = 4

-- INSERCIÓN DE EMPLEADOS ---------------------------------------------

INSERT INTO Employee (Tuition, FirstName, MiddleName, LastName, Email, Password, Gender, IdRole, IdContractType, IdProfesorCategory, IdCareer) 
            VALUES ('zA04012840',   'Ernesto',    'González',   'Pérez',    'administrador@example.com',    '1234', 'H', 1, 1, 1, NULL), 
                   ('zD19017267',   'Sheila',     'Muñóz',      'Arraiga',  'administrativo@example.com',   '1234', 'M', 2, 1, 1, NULL),
                   ('zP23097890',   'Erika',      'Meneses',    'Riko',     'profesor@example.com',         '1234', 'M', 3, 2, 2, 1),
                   ('zP23097891',   'Karen',      'Cortes',     'Verdín',     'profesor2@example.com',      '1234', 'M', 3, 2, 2, 3);

-- INSERCIÓN DE DIRECTORES --------------------------------------------
INSERT INTO Director (FirstName, MiddleName, LastName, Signature) 
            VALUES ('Luis Gerardo', 'Montané', 'Jimenez',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\director_signature.png', SINGLE_BLOB) AS Doc));

-- INSERCIÓN DE TIPOS DE CERTIFICADOS ---------------------------------

INSERT INTO CertificadeType (Type, Doc) 
            VALUES ('Participación en actualización de EE', 
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\Participación en la actualización del Programa EE Proyecto Integrador.docx', SINGLE_BLOB) AS Doc)),
                   ('Proceso de acreditación',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\Proceso de acreditación.docx', SINGLE_BLOB) AS Doc)),
                   ('De dirección',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\Dirección.docx', SINGLE_BLOB) AS Doc)),
                   ('De experiencia educativa',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\EE.docx', SINGLE_BLOB) AS Doc)),
                   ('Jurado de exámen de oposición',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\Participación como Jurado de Examen de Oposición.docx', SINGLE_BLOB) AS Doc)),
                   ('Orbis Tertius',
                   (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\fofor\Desktop\Escuela\Administración de proyectos de software\Constancias\Orbis Tertius.docx', SINGLE_BLOB) AS Doc));

-- INSERCIÓN DE CERTIFICADOS ---------------------------------
INSERT INTO Certificade (DateApplied, IdCertifiedType, IdProfesor) 
            VALUES('2024-11-25', 1, 3), ('2024-11-25', 1, 4);



-- PRUEBAS DE SELECCIÓN DAOEmployee --------------------------
USE Certificade_GS;
SELECT * FROM Employee;

SELECT e.IdEmployee, e.Tuition, e.FirstName, e.MiddleName, e.LastName, e.Gender, e.Email, e.Password, 
    er.IdEmployeeRole, er.Role, ect.IdContractType, ect.Type, epc.IdProfesorCategory, epc.Category FROM Employee e
    LEFT JOIN EmployeeRole er ON e.IdRole = er.IdEmployeeRole
    LEFT JOIN EmployeeContractType ect ON e.IdContractType = ect.IdContractType
    LEFT JOIN EmployeeProfesorCategory epc ON e.IdProfesorCategory = epc.IdProfesorCategory
    WHERE Email = 'profesor@example.com' AND Password = '1234';

SELECT e.IdEmployee, e.Tuition, e.FirstName, e.MiddleName, e.LastName, e.Gender, e.Email, e.Password, 
er.IdEmployeeRole, er.Role, ect.IdContractType, ect.Type, epc.IdProfesorCategory, epc.Category FROM Employee e 
LEFT JOIN EmployeeRole er ON e.IdRole = er.IdEmployeeRole
LEFT JOIN EmployeeContractType ect ON e.IdContractType = ect.IdContractType
LEFT JOIN EmployeeProfesorCategory epc ON e.IdProfesorCategory = epc.IdProfesorCategory
WHERE Email = 'profesor@example.com' AND Password = '1234';
SELECT IdEmployee, Tuition, FirstName, MiddleName, LastName, Email, Password, 
    IdRole, IdContractType, IdProfesorCategory FROM Employee 
    WHERE IdRole = 3;

-- PRUEBAS DE SELECCIÓN DAOCertificate------------------------
USE Certificade_GS;
SELECT * FROM Certificade;

SELECT crt.IdCertifcade, crt.DateApplied AS DateEmited, crtty.IdCertificadeType, crtty.Type,
    empl.IdEmployee, CONCAT(empl.FirstName, ' ', empl.MiddleName) AS ProfesorName
    FROM Certificade crt 
    LEFT JOIN CertificadeType crtty ON crt.IdCertifiedType = crtty.IdCertificadeType 
    LEFT JOIN Employee empl ON crt.IdProfesor = empl.IdEmployee;

SELECT crt.IdCertifcade, crt.DateApplied AS DateEmited, crtty.IdCertificadeType, crtty.Type, 
    empl.IdEmployee, CONCAT(empl.FirstName, ' ', empl.MiddleName) AS ProfesorName, crtty.Doc
    FROM Certificade crt 
    LEFT JOIN CertificadeType crtty ON crt.IdCertifiedType = crtty.IdCertificadeType 
    LEFT JOIN Employee empl ON crt.IdProfesor = empl.IdEmployee 
    WHERE crt.IdCertifcade = 1;

SELECT * FROM CertificadeType;