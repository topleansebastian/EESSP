CREATE OR ALTER PROCEDURE [dbo].[ConectareDoctor](@Text NVARCHAR(MAX), @Parola NVARCHAR(MAX))
AS
BEGIN
	SELECT * FROM [Cabinet].[dbo].[Doctor] WHERE [ContUtilizator] = @Text AND [Parola] = @Parola OR [NumarParafa] = @Text AND [Parola] = @Parola
END