CREATE OR ALTER PROCEDURE [dbo].[AdaugareConsultatie]
(
@CNP CHAR(13),
@Simptome NVARCHAR(MAX),
@Diagnostic NVARCHAR(MAX),
@Prescriptii NVARCHAR(MAX),
@ConcediuMedical BIT,
@Data DATETIME
)
AS
BEGIN
	DECLARE @IdPacient INT
	SELECT @IdPacient=[ID] FROM [Cabinet].[dbo].[Pacient] WHERE [CNP] = @CNP
	INSERT INTO [Cabinet].[dbo].[Consultatii](IdPacient,IdDoctor,Simptome,Diagnostic, Cod,Prescriptii,ConcediuMedical,[Data]) VALUES
	(@IdPacient,1,@Simptome,@Diagnostic,0,@Prescriptii,@ConcediuMedical,@Data)
END