CREATE OR ALTER PROCEDURE [dbo].[CautarePacient]
(@Nume NVARCHAR(MAX))
AS
BEGIN
	SELECT [Nume],[Prenume],[DataNasterii],[Sex],[NrTelefon],[StatutSocial],[Adresa],[GrupSangvin],[RH],[Masa],[Inaltime] FROM [dbo].[ViewPacienti] WHERE [Nume] LIKE @Nume+'%'
END