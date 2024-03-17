-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS__GetUsersRoleByEmail]
@email NVARCHAR(75) = ''
AS
BEGIN
	Select Role, Email from RahulTMG_Users AS U join RahulTMG_Roles AS  R ON U.RoleId = R.RoleId WHERE Email = @email
END
