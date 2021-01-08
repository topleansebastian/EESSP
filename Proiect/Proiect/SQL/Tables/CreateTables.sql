CREATE TABLE [Cabinet].[dbo].Pacient(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nume NVARCHAR(MAX) NOT NULL,
Prenume NVARCHAR(MAX) NOT NULL,
DataNasterii DATETIME NOT NULL, 
CNP CHAR(13) NOT NULL,
Sex BIT NOT NULL,
StatutSocial NVARCHAR(MAX) NOT NULL,
Adresa NVARCHAR(MAX) NOT NULL, 
NrTelefon CHAR(10) NULL, 
Email NVARCHAR(MAX) NULL, 
Cetatenie NVARCHAR(MAX) NOT NULL,
GrupSangvin NVARCHAR(2) NULL, 
RH BIT NOT NULL DEFAULT 1,
Masa FLOAT NOT NULL,
Inaltime FLOAT NOT NULL,
Alergii NVARCHAR(MAX) NULL,
Ocupatie NVARCHAR(MAX) NULL,
AntecedenteHeredoColaterale NVARCHAR(MAX) NULL,
AntecedentePersonale NVARCHAR(MAX) NULL,
ConditiiMunca NVARCHAR(MAX) NULL,
[timestamp] timestamp
);


CREATE TABLE [Cabinet].[dbo].Specializari(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Specializare NVARCHAR(MAX),
[timestamp] timestamp
);

CREATE TABLE [Cabinet].[dbo].Doctor(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nume NVARCHAR(MAX) NOT NULL,
Prenume NVARCHAR(MAX) NOT NULL,
Adresa NVARCHAR(MAX) NOT NULL,
NrTelefon CHAR(10),
IdSpecializare INT NOT NULL,
NumarParafa NVARCHAR(MAX) NOT NULL,
IdCreator INT NULL,
ContUtilizator NVARCHAR(MAX) NOT NULL,
Parola NVARCHAR(MAX) NOT NULL,
[timestamp] timestamp,

CONSTRAINT FK_Doctor_Specializari FOREIGN KEY (IdSpecializare) REFERENCES Specializari(ID) ON DELETE CASCADE
);


CREATE TABLE [Cabinet].[dbo].Consultatii(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdPacient INT NOT NULL,
IdDoctor INT NOT NULL,
Simptome NVARCHAR(MAX) NOT NULL,
Diagnostic NVARCHAR(MAX) NOT NULL,
Cod NVARCHAR(MAX) NOT NULL,
Prescriptii NVARCHAR(MAX) NOT NULL,
ConcediuMedical BIT NOT NULL DEFAULT 0,
[timestamp] timestamp,
CONSTRAINT FK_Consultatii_Pacient FOREIGN KEY(IdPacient) REFERENCES Pacient(ID) ON DELETE CASCADE,
CONSTRAINT FK_Consultatii_Doctor FOREIGN KEY(IdDoctor) REFERENCES Doctor(ID) ON DELETE CASCADE
);


CREATE TABLE [Cabinet].[dbo].[Programari](
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdPacient INT NOT NULL,
IdDoctor INT NOT NULL,
DataProgramare DATETIME NOT NULL,
Comentarii NVARCHAR(MAX) NULL,
[timestamp] timestamp,

CONSTRAINT FK_Programari_Pacient FOREIGN KEY(IdPacient) REFERENCES Pacient(ID) ON DELETE CASCADE,
CONSTRAINT FK_Programari_Doctori FOREIGN KEY(IdDoctor) REFERENCES Doctor(ID) ON DELETE CASCADE
);

CREATE TABLE [Cabinet].[dbo].[FiselePacientilor]
(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdPacient INT NOT NULL,
DiagnosticDeTrimitere NVARCHAR(MAX) NULL,
CineTrimite INT NULL,
DiagnosticPrincipal NVARCHAR(MAX) NULL,
BoliConcomitente NVARCHAR(MAX) NULL,
SemneSiSimptome NVARCHAR(MAX) NULL,
AntecedenteHeredoColaterale NVARCHAR(MAX) NULL,
AntecedentePersonale NVARCHAR(MAX) NULL,
ConditiiMunca NVARCHAR(MAX) NULL,
IPPDebut NVARCHAR(MAX) NULL, --Istoricul Patologiei Prezente
IPPTratamenteAnterioare NVARCHAR(MAX) NULL,
IPPEvolutie NVARCHAR(MAX) NULL,
SPConditiiDeAparitie NVARCHAR(MAX) NULL, -- Starea Prezenta
SPLocalizare NVARCHAR(MAX) NULL,
SPIntensitateaDurerii INT NULL,
SPCaracterulDurerii NVARCHAR(MAX),
SPRedoareMatinala BIT NULL,
SPEvolutieInTimp NVARCHAR(MAX) NULL,
SPDurata NVARCHAR(MAX) NULL,
SPFactoriFavorizanti NVARCHAR(MAX) NULL,
SPFactoriAgravanti NVARCHAR(MAX) NULL,
SPCalmareLa NVARCHAR(MAX) NULL,
SPAlteObservatii NVARCHAR(MAX) NULL,
TNEpilepsie BIT, -- Tulburari neurologice
TNFrecventaCrize NVARCHAR(MAX),
TNPareze NVARCHAR(MAX),
TNParalizii NVARCHAR(MAX),
TNParaliziiObservatii NVARCHAR(MAX),
TNNevralgii NVARCHAR(MAX),
TNAltele NVARCHAR(MAX),
SMGTroficitate NVARCHAR(MAX), -- Starea Musculara Generala
SMGTonicitate NVARCHAR(MAX),
SMGKinezie NVARCHAR(MAX),
SMGAlteObservatii NVARCHAR(MAX),
TMGuperhidroza NVARCHAR(MAX), -- Tegumente si Mucoase
TMVeruci NVARCHAR(MAX),
SistemGanglionarSuperficial NVARCHAR(MAX),
ACVTensiune NVARCHAR(MAX), -- Aparat Cardio-Vascular
ACVBoliCoronare NVARCHAR(MAX),
ACVTulburariDeRitmCardiac NVARCHAR(MAX),
ACVCardioMiopatii NVARCHAR(MAX),
ACVDefecteValvulare NVARCHAR(MAX),
ACVArterita NVARCHAR(MAX),
ACVTulburariDeCoagulareASangelui NVARCHAR(MAX),
ACVAlteBoliSangvine NVARCHAR(MAX),

CONSTRAINT FK_FiselePacientilor_Pacient FOREIGN KEY(IdPacient) REFERENCES Pacient(ID)

)