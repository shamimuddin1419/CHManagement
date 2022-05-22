-- =============================================
-- Author:		Newaz Sharif
-- Create date: 16 May 2022
-- =============================================
CREATE PROCEDURE [dbo].[gsp_getDishCustomerCardInfoById]
	@id int
AS
BEGIN
	SELECT 
			customerId,
			customerName,
			customerPhone,
			customerAddress,
			customerLocality,
			ownerName,
			ownerPhone
    FROM tblDishCustomerCardInfo dci
	
	WHERE customerId = @id 
	


END

