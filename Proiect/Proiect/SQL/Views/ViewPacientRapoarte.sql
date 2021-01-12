CREATE OR ALTER VIEW [ViewPacientRapoarte] AS
SELECT 
[p].[ID],
[p].[Nume],
[p].[Prenume],
[p].[CNP],
[p].[DataNasterii],
CASE WHEN [p].[Sex]=1 THEN 'M' ELSE 'F' END AS [Sex],
[p].[NrTelefon],
[p].[StatutSocial],
[p].[Adresa],
[p].[GrupSangvin],
CASE WHEN [p].[RH] = 1 THEN 'Poz' ELSE 'Neg' END AS [RH],
[p].[Masa],
[p].[Inaltime],
[p].[timestamp]
FROM [Cabinet].[dbo].[Pacient]