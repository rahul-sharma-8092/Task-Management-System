-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS_AddUpdateUser]
	@userId bigint = 0,
	@fullName nvarchar(200) = '',
	@email NVARCHAR(200) = '',
	@password nvarchar(max) = '',
	@mobile nvarchar(15) = '',
	@doj Date = NULL,
	@roleId bigint = 0,
	@image NVARCHAR(MAX) = '',
	@query INT = 0
AS
BEGIN
	Declare @lastId BIGINT;
	IF(@query = 1)
	BEGIN
		INSERT INTO RahulTMG_Users(FullName, Email, [Password], Mobile, DateOfJoining, RoleId, [Image])
		VALUES(@fullName, @email, @password, @mobile, @doj, @roleId, @image)

		SET @lastId = SCOPE_IDENTITY();

		IF(@roleId = 1 OR @roleId = 2)
		BEGIN
			Insert INTO RahulTMG_Employee(EmpId, EmpEmail)
			VALUES(@lastId, @email)
		END
	END

	--Update User
	IF(@query = 2)
	BEGIN
		Update RahulTMG_Users SET FullName = @fullName, DateOfJoining = @doj, RoleId = @roleId, Mobile = @mobile, 
		[Image] = @image, UpdatedAt = GETDATE() WHERE UserId = @userId

		IF(@roleId = 3)
		BEGIN
			Update RahulTMG_Employee Set IsDeleted = 1 Where EmpId = @userId
		END

		-- Role Changed to Admin/Employee then Update entry on RahulTMG_Employee
		ELSE IF(@roleId = 1 OR @roleId = 2)
		BEGIN
			IF((Select Count(1) from RahulTMG_Employee Where EmpId = @userId) = 0)
			BEGIN
				Insert Into RahulTMG_Employee(EmpId, EmpEmail) VALUES (@userId, @email)
			END

			
			Update RahulTMG_Employee Set IsDeleted = (Select Isdeleted from RahulTMG_Users Where UserId = @userId) Where EmpId = @userId 
		END
	END

	--Delete User
	IF(@query = 3)
	BEGIN
		UPDATE RahulTMG_Users SET IsDeleted = 1 WHERE UserId = @userId

		Update RahulTMG_Employee SET IsDeleted = 1 WHERE EmpId = @userId
	END

END
