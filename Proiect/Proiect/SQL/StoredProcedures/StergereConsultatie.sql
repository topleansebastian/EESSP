CREATE OR ALTER PROCEDURE [dbo].[StergereProgramare]
(@CNP CHAR(13),
@Data DATETIME)
AS
BEGIN
	DECLARE @IdPacient INT;
	SELECT @IdPacient = ID FROM [Cabinet].[dbo].[Pacient] WHERE CNP = @CNP
	DELETE FROM [Cabinet].[dbo].[Programari] WHERE [IdPacient] = @IdPacient AND DataProgramare = @Data
END