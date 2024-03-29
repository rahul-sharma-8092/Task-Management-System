-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- EXEC RahulTMS_GetProjectList
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS_GetProjectList]
	
AS
BEGIN
	Select ProjectId, ProjectName, ProjectDescription, StartDate, EndDate, [Status], IsClosed, 
	UO.FullName AS [CreatedBy], UM.FullName AS [ModifiedBy]
	FROM RahulTMG_Project AS P 
	LEFT JOIN RahulTMG_Users UO ON P.CreatedBy = UO.UserId
	LEFT JOIN RahulTMG_Users UM ON P.ModifiedBy = UM.UserId WHERE P.IsDeleted = 0
END
