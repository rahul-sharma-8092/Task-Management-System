-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <22-03-2024>
-- Description:	<Manage Blocking for Wrong Login Attempts>
-- EXEC RahulTMS_AddUpdateLoginAttempt 1001, 2
-- =============================================
CREATE PROCEDURE RahulTMS_AddUpdateLoginAttempt
	@userId BIGINT = 0,
	@query BIGINT = 0
AS
BEGIN
	DECLARE @wrongAttempt BIGINT = 0;
	DECLARE @isBlocked BIT = 0;

	IF(@query = 1)
	BEGIN
		IF((SELECT IsParmanentBlocked FROM RahulTMG_LoginHistory WHERE UserId = @userId) = 0)
		BEGIN
			IF EXISTS(Select 1 from RahulTMG_LoginHistory Where UserId = @userId)
			BEGIN
				SET @wrongAttempt = (Select WrongLoginAttempt from RahulTMG_LoginHistory Where UserId = @userId) + 1;

				IF(@wrongAttempt < 5)
				BEGIN
					Update RahulTMG_LoginHistory SET WrongLoginAttempt = @wrongAttempt, 
					LastLoginTime = GETDATE() WHERE UserId = @userId

					Select @isBlocked AS IsBlocked
				END
				ELSE
				BEGIN
					declare @prevBlockedTime DateTime = (Select PrevBlockedTime from RahulTMG_LoginHistory WHere UserId = @userId)
					IF(DATEDIFF(MINUTE, @prevBlockedTime, GETDATE()) < 30)
					BEGIN
						Update RahulTMG_LoginHistory SET WrongLoginAttempt = 5, IsBlocked = 1,
						IsParmanentBlocked = 1, LastLoginTime = GETDATE() WHERE UserId = @userId
					END
					ELSE
					BEGIN
						Update RahulTMG_LoginHistory SET WrongLoginAttempt = 5, IsBlocked = 1,
						LastLoginTime = GETDATE() Where UserId = @userId
					END

					SET @isBlocked = 1;
					Select @isBlocked AS IsBlocked
				END
			END

			ELSE
			BEGIN
				INSERT INTO RahulTMG_LoginHistory(UserId, WrongLoginAttempt) VALUES(@userId, 1)
				Select @isBlocked AS IsBlocked
			END
		END

		ELSE
		BEGIN
			Select 2 AS IsBlocked --Parmanently blocked
		END
	END

	--Check IsBlocked
	ELSE IF(@query = 2)
	BEGIN
	IF EXISTS(Select 1 from RahulTMG_LoginHistory Where UserId = @userId)
		BEGIN
			IF((SELECT IsParmanentBlocked FROM RahulTMG_LoginHistory WHERE UserId = @userId) = 0)
			BEGIN
				IF((SELECT IsBlocked FROM RahulTMG_LoginHistory WHERE UserId = @userId) = 1)
				BEGIN 
					declare @lastLogin DateTime = (Select LastLoginTime from RahulTMG_LoginHistory Where UserId = @userId)
			

					IF(DATEDIFF(MINUTE, @lastLogin, GETDATE()) < 10)
					BEGIN
						SET @isBlocked = 1
						Select @isBlocked AS IsBlocked
					END

					ELSE
					BEGIN
						Update RahulTMG_LoginHistory SET IsBlocked = 0, WrongLoginAttempt = 0, 
						LastLoginTime = GETDATE(), PrevBlockedTime = GETDATE() Where UserId = @userId

						SET @isBlocked = 0
						Select @isBlocked AS IsBlocked
					END
				END

				ELSE
				BEGIN
					Update RahulTMG_LoginHistory SET WrongLoginAttempt = 0, IsBlocked = 0,
					LastLoginTime = GETDATE() Where UserId = @userId
					
					Select @isBlocked AS IsBlocked
				END
			END

			ELSE
			BEGIN
				Select 2 AS IsBlocked --Parmanently blocked
			END
		END
	ELSE
	BEGIN
		INSERT INTO RahulTMG_LoginHistory(UserId, WrongLoginAttempt) VALUES(@userId, 0)
		Select @isBlocked AS IsBlocked
	END
	END
END
GO