-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RahulTMS_CheckEmailExists]
	@email NVARCHAR(100) = ''
AS
BEGIN
	Select Email from RahulTMG_Users Where Email = @email
END
