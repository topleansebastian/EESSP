CREATE OR ALTER PROCEDURE [dbo].[SortarePacienti]
(
	@SortareDupaNume BIT,
	@SortareAscendenta BIT
)
AS
BEGIN
	IF @SortareDupaNume = 1
	BEGIN
		IF @SortareAscendenta = 1
		BEGIN
			SELECT * FROM [dbo].[ViewPacienti] ORDER BY Nume ASC
		END
		ELSE
		BEGIN
			SELECT * FROM [dbo].[ViewPacienti] ORDER BY Nume DESC
		END
	END
	ELSE
	BEGIN
		IF @SortareAscendenta = 1
		BEGIN
			SELECT * FROM [dbo].[ViewPacienti] ORDER BY [timestamp] ASC
		END
		ELSE
		BEGIN
			SELECT * FROM [dbo].[ViewPacienti] ORDER BY [timestamp] DESC
		END
	END
END