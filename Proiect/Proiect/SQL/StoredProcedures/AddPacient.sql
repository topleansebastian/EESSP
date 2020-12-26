CREATE OR ALTER PROCEDURE [dbo].AdaugarePacient
@Nume NVARCHAR(MAX),
@Prenume NVARCHAR(MAX),
@DataNasterii DATETIME,
@Sex BIT,
@StatutSocial NVARCHAR(MAX),
@Adresa NVARCHAR(MAX),
@NrTelefon CHAR(10),
@Email NVARCHAR(MAX),
@Cetatenie NVARCHAR(MAX),
@GrupSangvin NVARCHAR(2),
@RH BIT = 0,
@CNP CHAR(13),
@Masa FLOAT,
@Inaltime FLOAT,
@Alergii NVARCHAR(MAX),
@Ocupatie NVARCHAR(MAX),
@AntecedenteHeredoColaterale NVARCHAR(MAX),
@AntecedentePersonale NVARCHAR(MAX),
@ConditiiMunca NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [Cabinet].[dbo].Pacient(Nume,Prenume,DataNasterii,CNP,Sex,StatutSocial,Adresa,NrTelefon,Email,Cetatenie,GrupSangvin,RH,Masa,Inaltime,Alergii,Ocupatie,AntecedenteHeredoColaterale,AntecedentePersonale,ConditiiMunca)
		VALUES(@Nume,@Prenume,@DataNasterii, @CNP,@Sex,@StatutSocial,@Adresa,@NrTelefon,@Email,@Cetatenie,@GrupSangvin,@RH,@Masa,@Inaltime,@Alergii,@Ocupatie,@AntecedenteHeredoColaterale,@AntecedentePersonale,@ConditiiMunca);
END
