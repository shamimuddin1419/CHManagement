-- =============================================
-- Author:		Newaz Sharif
-- Create date: 05 may 2022
-- =============================================
CREATE PROCEDURE [dbo].[isp_InternetCustomerCardInfo]
	@insertFlag int,
	@customerId varchar(20),
	@customerLocality nvarchar(100) = null, 
	@customerName nvarchar(100),
	@customerPhone nvarchar(15),
	@ownerName nvarchar(100) = null,
	@ownerPhone nvarchar(15)  = NULL,
	@customerAddress nvarchar(200)  = NULL,
	@createdBy int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here

	DECLARE @requestTypeId int;
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
	DECLARE @connYearName decimal;

	BEGIN TRY
	if @insertFlag = 1
	begin
		INSERT INTO tblInternetCustomerCardInfo
           (
		    [customerId],
			[customerLocality] ,
			[customerName] ,
			[customerPhone] ,
			[customerAddress] ,
			[ownerName] ,
			[ownerPhone] ,
			[createdBy] ,
			[createdDate] 
		   )
     VALUES
           (
		    @customerId,
			@customerLocality,
			@customerName,
			@customerPhone,
			@customerAddress,
			@ownerName,
			@ownerPhone,
			@createdBy,
			getdate()
			);
	end
	else
	begin
		UPDATE tblInternetCustomerCardInfo 
		set
			customerName = @customerName,
			customerLocality = @customerLocality,
			customerPhone = @customerPhone,
			customerAddress = @customerAddress,
			ownerName = @ownerName,
			ownerPhone = @ownerPhone,
			editedBy = @createdBy,
			editedDate = getdate()
		where customerId = @customerId
		
	end
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE()
		SET @ErrorSeverity = ERROR_SEVERITY()
		RAISERROR(@ErrorMessage, @ErrorSeverity, 1)
		ROLLBACK TRANSACTION tranc;
	END CATCH
	
END