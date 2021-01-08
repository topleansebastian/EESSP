IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'IdConsultatie'
          AND Object_ID = Object_ID(N'dbo.Programari'))
BEGIN
    ALTER TABLE [Cabinet].[dbo].Programari ADD IdConsultatie INT NULL;
END


IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'IdProgramare'
          AND Object_ID = Object_ID(N'dbo.Consultatii'))
BEGIN
    ALTER TABLE [Cabinet].[dbo].[Consultatii] ADD IdProgramare INT NULL;
END

