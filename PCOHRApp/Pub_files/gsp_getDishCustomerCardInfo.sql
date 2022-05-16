-- =============================================
-- Author:		Newaz Sharif
-- Create date: 16 May 2022
-- =============================================
CREATE PROCEDURE [dbo].[gsp_getDishCustomerCardInfo]
	
AS
BEGIN
	SELECT 
			dci.customerId,
			cast(ltrim(rtrim(cs.customerSerialName)) as varchar) + cast(dc.customerId as varchar(20)) as customerSerial, 
			dci.customerName,
			dci.customerPhone,
			dci.customerAddress,
			dci.customerLocality,
			dci.ownerName,
			dci.ownerPhone
    FROM tblDishCustomerCardInfo dci
	INNER JOIN tblDishCustomers dc on dci.customerId = dc.id
	INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId

END
