CREATE OR ALTER PROCEDURE SelectareConsultatii
(
@IdDoctor INT,
@PerioadaStart DATE,
@PerioadaStop DATE = NULL
)
AS
BEGIN
	IF @PerioadaStop IS NOT NULL
	BEGIN
		SELECT  [prog].[DataProgramare] AS DataProgramare,
			[pac].[Nume] AS NumePacient,
			[doc].[Nume] AS NumeDoctor,
			[prog].[Comentarii] AS Comentarii
		FROM [Cabinet].[dbo].[Programari] prog
		LEFT JOIN [Cabinet].[dbo].[Pacient] pac ON [prog].[IdPacient] = [pac].[ID]
		LEFT JOIN [Cabinet].[dbo].[Doctor] doc ON [prog].[IdDoctor] = [doc].[ID] AND [doc].[ID] = @IdDoctor
		WHERE [prog].[DataProgramare] >= @PerioadaStart AND [prog].[DataProgramare] <= @PerioadaStop
	END
	ELSE
	BEGIN
		SELECT  [prog].[DataProgramare],
			[pac].[Nume],
			[doc].[Nume],
			[prog].[Comentarii] AS Comentarii
		FROM [Cabinet].[dbo].[Programari] prog
		LEFT JOIN [Cabinet].[dbo].[Pacient] pac ON [prog].[IdPacient] = [pac].[ID]
		LEFT JOIN [Cabinet].[dbo].[Doctor] doc ON [prog].[IdDoctor] = [doc].[ID] AND [doc].[ID] = @IdDoctor
		WHERE [prog].[DataProgramare] = @PerioadaStart
	END

END