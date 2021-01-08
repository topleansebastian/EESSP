CREATE OR ALTER VIEW ViewProgramari AS SELECT 
[prog].[ID],
[prog].[DataProgramare],
[pac].[Nume],
[pac].[Prenume],
[pac].[CNP],
[prog].[Comentarii]
FROM [Cabinet].[dbo].[Programari] prog
LEFT JOIN [Cabinet].[dbo].[Pacient] pac ON [prog].[IdPacient] = [pac].[ID]