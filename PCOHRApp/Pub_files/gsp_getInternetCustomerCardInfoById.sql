-- =============================================
-- Author:		Newaz Sharif
-- Create date: 16 May 2022
-- =============================================
CREATE PROCEDURE [dbo].[gsp_getInternetCustomerCardInfoById]
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
    FROM tblInternetCustomerCardInfo dci
	
	WHERE customerId = @id 
	


END

