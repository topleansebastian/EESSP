CREATE OR ALTER PROCEDURE [dbo].[AdaugareProgramare]
(
	@Nume NVARCHAR(MAX),
	@Prenume NVARCHAR(MAX),
	@Data DATETIME,
	@Mentiuni NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @IdPacient INT;
	SELECT @IdPacient = ID FROM [Cabinet].[dbo].[Pacient] WHERE Nume = @Nume AND Prenume = @Prenume
	INSERT INTO [Cabinet].[dbo].[Programari](IdPacient, IdDoctor, DataProgramare, Comentarii) VALUES (@IdPacient, 1, @Data, @Mentiuni)
END