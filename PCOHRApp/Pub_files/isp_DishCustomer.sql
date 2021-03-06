USE [PCOHRAppDB]
GO
/****** Object:  StoredProcedure [dbo].[isp_DishCustomer]    Script Date: 6/10/2022 11:43:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Newaz Sharif
-- Create date: 19 Apr 2020
-- =============================================
ALTER PROCEDURE [dbo].[isp_DishCustomer]
	@insertFlag int,
	@customerId varchar(20),
	@customerSerialId int,
	@customerName nvarchar(100),
	@customerPhone nvarchar(15),
	@customerAddress nvarchar(200) = null,
	@hostId int,
	@zoneId int,
	@assignedUserId int,
	@connFee decimal(8, 2) = 0,
	@monthBill decimal(8, 2) = 0,
	@othersAmount decimal(8, 2) = 0,
	@description nvarchar(300) = null,
	@connMonth varchar(20),
	@connYear int,
	@isActive bit,
	@createdBy int,
	@EntryDate datetime =null,
	@nid varchar(20)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	declare @customerSerial varchar(20) = '';
	DECLARE @cid int;
	DECLARE @requestTypeId int;
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
	DECLARE @connYearName decimal;
	DECLARE @existingCustNid varchar(50);

	BEGIN TRY
	BEGIN TRANSACTION tranc;
	SELECT @customerSerial = customerSerialName from tblCustomerSerial where customerSerialId = @customerSerialId;
	SELECT @requestTypeId = requestTypeId from tblCustomerRequestType where requestName = 'Connection Fee';
	if ( LEN(REPLACE(@nid, ' ', '' )) <> 10 and LEN(REPLACE(@nid, ' ', '' )) <> 13)
	BEGIN
		RAISERROR('NID should be 10 or 13 digit',16,1)
	END
	if @insertFlag = 1
	begin
		
		SELECT @existingCustNid = cast(ltrim(rtrim(customerSerialName)) as varchar)+cast(dc.customerId as varchar(20)) 
		FROM tblDishCustomers dc
		INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
		where nid = @nid
		
		IF(@existingCustNid is not null)
		BEGIN
			RAISERROR('This nid is already exist with customer : %s'  ,16,1,@existingCustNid)
		END

		INSERT INTO tblDishCustomers
           (
		   customerId,
		   customerSerialId,
			customerName,
			customerPhone,
			customerAddress,
			hostId,
			zoneId,
			assignedUserId,
			connFee,
			monthBill,
			othersAmount,
			[description],
			connMonth,
			connYear,
			isActive,
			createdBy,
			createdDate
			,EntryDate
			,nid
		   )
     VALUES
           (
		   @customerId,
		   @customerSerialId,
		    @customerName,
			@customerPhone,
			@customerAddress,
			@hostId,
			@zoneId,
			@assignedUserId,
			@connFee,
			@monthBill,
			@othersAmount,
			@description,
			@connMonth,
			@connYear,
			@isActive,
			@createdBy,
		    getdate(),
			@EntryDate,
			@nid
			);
		SELECT @cid =  SCOPE_IDENTITY()
		IF @connFee > 0
		BEGIN
			INSERT INTO [dbo].[tblDishTransactionMaster]
				([cid]
				,[transactionType]
				,[transactionAmount]
				,[isConFee]
				,[transactionMonth]
				,[transactionYear]
				,[createdBy]
				,[createdDate]
				)
			VALUES
				(@cid
				,'Dr.'
				,@connFee
				,1
				,@connMonth
				,@connYear
				,@createdBy
				,GETDATE())

			
		END
		IF @othersAmount > 0
			BEGIN
				INSERT INTO [dbo].[tblDishTransactionMaster]
					([cid]
					,[transactionType]
					,[transactionAmount]
					,[isOtherFee]
					,[transactionMonth]
					,[transactionYear]
					,[createdBy]
					,[createdDate]
					)
				VALUES
					(@cid
					,'Dr.'
					,@othersAmount
					,1
					,@connMonth
					,@connYear
					,@createdBy
					,GETDATE())

				
			END
		INSERT INTO [dbo].[tblDishCustomerBalance]
				([cid]
				,[totalDebit]
				,[totalCredit]
				,[totalBalance]
				,[createdBy]
				,[createdDate]
				)
				VALUES
				(@cid
				,@connFee+@othersAmount
				,0
				,0-(@connFee + @othersAmount)
				,@createdBy
				,GETDATE())

		SELECT @connYearName =  y.yearName FROM tblDishCustomers c
			INNER JOIN tblYear y on c.connYear = y.yearId
			WHERE id = @cid;

			DECLARE @betweenMonthName varchar(20);
			DECLARE @betweenYearName int;
			DECLARE billGenCursor CURSOR 
			FOR Select betweenMonthName,betweenYearName from GetMonthListBetweenDates('01-'+@connMonth+'-' +CAST (@connYearName as varchar),DATEADD(month, -1, getdate()))

			OPEN billGenCursor
			FETCH NEXT FROM billGenCursor INTO
				@betweenMonthName
			   ,@betweenYearName
			WHILE @@FETCH_STATUS = 0
			BEGIN
				print @betweenMonthName
				print @betweenYearName
				EXEC isp_DishCustomerBillGenerate @cid,0,@betweenMonthName,@betweenYearName,'Generated While Customer Setup',@createdBy
				FETCH NEXT FROM billGenCursor INTO
				@betweenMonthName
			   ,@betweenYearName 
			END
			CLOSE billGenCursor;
			DEALLOCATE billGenCursor;
	end

	else
	begin
		SELECT @existingCustNid = cast(ltrim(rtrim(customerSerialName)) as varchar)+cast(dc.customerId as varchar(20)) 
		FROM tblDishCustomers dc
		INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
		where 
		nid = @nid
		and (dc.customerId != @customerId or cs.customerSerialId != @customerSerialId)

		IF(@existingCustNid is not null)
		BEGIN
			RAISERROR('This nid is already exist with customer : %s'  ,16,1,@existingCustNid)
		END

		UPDATE tblDishCustomers 
		set
			customerName = @customerName,
			customerPhone = @customerPhone,
			customerAddress = @customerAddress,
			hostId = @hostId,
			zoneId = @zoneId,
			assignedUserId = @assignedUserId,
			--connFee = @connFee,
			--monthBill = @monthBill,
			--othersAmount = @othersAmount,
			[description] = @description,
			--connMonth = @connMonth,
			--connYear = @connYear,
			isActive = @isActive,
			editedBy = @createdBy,
			editedDate = getdate(),
			EntryDate=@EntryDate,
			nid = @nid
		where customerId = @customerId and customerSerialId = @customerSerialId

		
	end
	SELECT @customerSerial + cast(@customerId as varchar(20)) as customerSerial
	COMMIT TRANSACTION tranc;
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ERROR_MESSAGE()
		SET @ErrorSeverity = ERROR_SEVERITY()
		RAISERROR(@ErrorMessage, @ErrorSeverity, 1)
		ROLLBACK TRANSACTION tranc;
	END CATCH
	
END
