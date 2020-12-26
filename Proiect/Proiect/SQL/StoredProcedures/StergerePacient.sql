CREATE OR ALTER PROCEDURE [dbo].[StergerePacient]
(@CNP CHAR(13))
AS
BEGIN
	DELETE FROM [Cabinet].[dbo].[Pacient] WHERE [CNP]=@CNP
END