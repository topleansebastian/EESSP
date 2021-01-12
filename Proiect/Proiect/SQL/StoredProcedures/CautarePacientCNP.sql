CREATE OR ALTER PROCEDURE [dbo].[CautarePacient]
(@CNP CHAR(13))
AS
BEGIN
	SELECT Nume,Prenume,CNP,DataNasterii,Sex,NrTelefon,StatutSocial,Adresa,GrupSangvin,RH,Masa,Inaltime FROM [Cabinet].[dbo].[ViewPacientRapoarte] WHERE CNP=@CNP
END