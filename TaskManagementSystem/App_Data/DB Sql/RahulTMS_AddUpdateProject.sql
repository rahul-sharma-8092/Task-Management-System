﻿-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <18-03-2024>
-- Description:	<Create Update Delete on Project Table>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS_AddUpdateProject]
	@userId BIGINT = 0,
	@id INT = 0,
	@name nvarchar(255) = '',
	@description nvarchar(max) = '',
	@startDate Date = NULL,
	@endDate Date = NULL,
	@status NVARCHAR(20) = '',
	@isClosed BIT = 0,
	@query INT = 0
AS
BEGIN
	IF(@query = 1)
	BEGIN
		Declare @lastId BIGINT;
		
		INSERT INTO RahulTMG_Project(ProjectName, ProjectDescription, CreatedBy, StartDate, EndDate, [Status], ModifiedBy, ModifiedDate)
		Values(@name, @description, @userId, @startDate, @endDate, @status, @userId, GETDATE())

		Set @lastId = SCOPE_IDENTITY();

		Insert INTO RahulTMG_Employee_Project(EmpId, ProjectId)
		Values(@userId, @lastId)
	END

	IF(@query = 2)
	BEGIN
		Update RahulTMG_Project SET ProjectName = @name, ProjectDescription = @description, StartDate = @startDate,
		EndDate = @endDate, ModifiedBy = @userId, ModifiedDate = GETDATE(), [Status] = @status, IsClosed = @isClosed
		WHERE ProjectId = @id
	END

	--Delete The Project 
	IF(@query = 3)
	BEGIN
		UPDATE RahulTMG_Project SET IsDeleted = 1, ModifiedDate = GETDATE(), ModifiedBy = @userId Where ProjectId = @id

		UPDATE RahulTMG_Employee_Project SET IsEmployeeAccess = 0 Where ProjectId = @id
	END
END
