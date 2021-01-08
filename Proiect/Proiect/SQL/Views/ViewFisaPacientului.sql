﻿CREATE OR ALTER VIEW ViewFisaPacientului AS SELECT
[p].[Nume],
[p].[Prenume],
[p].[DataNasterii],
[p].[Adresa],
[p].[Email],
[p].[GrupSangvin],
CASE WHEN [p].[RH]=1 THEN 'Pozitiv' ELSE 'NEGATIV' END AS [RH],
[p].[Masa],
[p].[Inaltime],
[p].[Alergii],
[p].[Ocupatie],
[f].[DiagnosticDeTrimitere] AS [DiagnosticDeTrimitere],
[f].[CineTrimite] AS [CineTrimite],
[f].[DiagnosticPrincipal] AS [DiagnosticPrincipal],
[f].[BoliConcomitente] AS [BoliConcomitente],
[f].[SemneSiSimptome] AS [SemneSiSimptome],
[f].[AntecedenteHeredoColaterale] AS [AntecedenteHeredoColaterale],
[f].[AntecedentePersonale] AS [AntecedentePersonale],
[f].[ConditiiMunca] AS [ConditiiMunca],
[f].[IPPDebut] AS [IPPDebut],
[f].[IPPTratamenteAnterioare] AS [IPPTratamenteAnterioare],
[f].[IPPEvolutie] AS [IPPEvolutie],
[f].[SPConditiiDeAparitie] AS [SPConditiiDeAparitie],
[f].[SPLocalizare] AS [SPLocalizare],
[f].[SPIntensitateaDurerii] AS [SPIntensitateaDurerii],
[f].[SPCaracterulDurerii] AS [SPCaracterulDurerii],
[f].[SPRedoareMatinala] AS [SPRedoareMatinala],
[f].[SPEvolutieInTimp] AS [SPEvolutieInTimp],
[f].[SPDurata] AS [SPDurata],
[f].[SPFactoriFavorizanti] AS [SPFactoriFavorizanti],
[f].[SPFactoriAgravanti] AS [SPFactoriAgravanti],
[f].[SPCalmareLa] AS [SPCalmareLa],
[f].[SPAlteObservatii] AS [SPAlteObservatii],
[f].[TNEpilepsie] AS [TNEpilepsie],
[f].[TNFrecventaCrize] AS [TNFrecventaCrize],
[f].[TNPareze] AS [TNPareze],
[f].[TNParalizii] AS [TNParalizii],
[f].[TNParaliziiObservatii] AS [TNParaliziiObservatii],
[f].[TNNevralgii] AS [TNNevralgii],
[f].[TNAltele] AS [TNAltele],
[f].[SMGTroficitate] AS [SMGTroficitate],
[f].[SMGTonicitate] AS [SMGTonicitate],
[f].[SMGKinezie] AS [SMGKinezie],
[f].[SMGAlteObservatii] AS [SMGAlteObservatii],
[f].[TMGuperhidroza] AS [TMGuperhidroza],
[f].[TMVeruci] AS [TMVeruci],
[f].[SistemGanglionarSuperficial] AS [SistemGanglionarSuperficial],
[f].[ACVTensiune] AS [ACVTensiune],
[f].[ACVBoliCoronare] AS [ACVBoliCoronare],
[f].[ACVTulburariDeRitmCardiac] AS [ACVTulburariDeRitmCardiac],
[f].[ACVCardioMiopatii] AS [ACVCardioMiopatii],
[f].[ACVDefecteValvulare] AS [ACVDefecteValvulare],
[f].[ACVArterita] AS [ACVArterita],
[f].[ACVTulburariDeCoagulareASangelui] AS [ACVTulburariDeCoagulareASangelui],
[f].[ACVAlteBoliSangvine] AS [ACVAlteBoliSangvine]

FROM [Cabinet].[dbo].[Pacient] AS p
LEFT JOIN [Cabinet].[dbo].[FiselePacientilor] AS f ON [f].[IdPacient]=[p].[ID]