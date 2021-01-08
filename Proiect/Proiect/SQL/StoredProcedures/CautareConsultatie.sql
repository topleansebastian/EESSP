CREATE OR ALTER PROCEDURE [dbo].[CreareConsultatie]
(@Nume NVARCHAR(MAX))
AS
BEGIN
	SELECT * FROM [dbo].[ViewConsultatii] WHERE [Nume] LIKE @Nume+'%'
END