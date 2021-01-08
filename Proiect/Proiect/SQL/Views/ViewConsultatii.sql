CREATE OR ALTER VIEW ViewConsultatii
AS SELECT 
[c].[Data],
[p].[CNP],
[p].[Nume],
[c].[Diagnostic],
[c].[Simptome],
[c].[Prescriptii]

FROM [Cabinet].[dbo].[Consultatii] AS c
INNER JOIN [Cabinet].[dbo].[Pacient] AS p ON [c].[IdPacient] = [p].[ID]