-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <2024-03-15>
-- Description:	<RahulTMS__GetUserDetailByEmail>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS__GetUserDetailByEmail]
@email NVARCHAR(75) = '',
@userId bigint = 0
AS
BEGIN
	Select  UserId, FullName, Email, Password, Mobile, DateOfJoining, U.RoleId, R.Role, Image
	from RahulTMG_Users AS U join RahulTMG_Roles AS R on U.RoleId = R.RoleId 
	Where Email = @email OR UserId = @userId
END
GO


