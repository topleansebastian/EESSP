CREATE OR ALTER VIEW [ViewPacienti] AS
SELECT [Nume],
[Prenume],
[CNP],
[DataNasterii],
CASE WHEN [Sex]=1 THEN 'M' ELSE 'F' END AS [Sex],
[NrTelefon],
[StatutSocial],
[Adresa],
[GrupSangvin],
CASE WHEN [RH] = 1 THEN 'Poz' ELSE 'Neg' END AS [RH],
[Masa],
[Inaltime],
[timestamp]
FROM [Cabinet].[dbo].[Pacient]