-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <20-03-2024>
-- Description:	<To store Task Attachment>
-- EXEC RahulTMS__SaveTaskAttachment '/Docs/DOC2138084396.png|/Docs/DOC2138084397.png|/Docs/DOC2138084398.png|/Docs/DOC2138084399.png|/Docs/DOC2138084400.png|/Docs/DOC2138084401.png|/Docs/DOC2138084402.png|/Docs/DOC2138084403.png|/Docs/DOC2138084404.png|/Docs/DOC2138084405.png|', 999, 5555
-- =============================================
ALTER PROCEDURE RahulTMS__SaveTaskAttachment
	@path NVARCHAR(MAX) = '',
	@taskId BIGINT = 0,
	@empId BIGINT = 0
AS
BEGIN
	DECLARE @return BIT  = 0;

	INSERT INTO RahulTMG_Attachment(EmpId, TaskId, Attachment)
	Select @empId, @taskId, [VALUE] AS [Attachment] 
	FROM STRING_SPLIT(@path, '|') Where [VALUE] <> '';

	SET @return  = 1;
	SELECT @return AS Response
END
GO



