-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<List of Users [Admin, Employee, Client]>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS_GetUsersList]
	
AS
BEGIN
	Select UserId, FullName, Email, [Password], Mobile, DateOfJoining, U.RoleId, R.[Role], Image, IsDeleted
	FROM RahulTMG_Users AS U
	LEFT JOIN RahulTMG_Roles AS R ON U.RoleId = R.RoleId 
END
