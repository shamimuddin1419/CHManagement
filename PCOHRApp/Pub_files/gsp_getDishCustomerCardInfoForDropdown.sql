
-- =============================================
-- Author:		Newaz Sharif
-- Create date: 16 May 2022
-- =============================================
CREATE PROCEDURE [dbo].[gsp_getDishCustomerCardInfoForDropdown]
	
AS
BEGIN

SELECT 
			id,
			dc.customerId,
			dc.customerSerialId,
			cast(ltrim(rtrim(customerSerialName)) as varchar) + cast(dc.customerId as varchar(20)) as customerSerial, 
			dc.customerName,
			dc.customerPhone,
			dc.customerAddress,
			dc.isActive
    FROM tblDishCustomers dc
	INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
	LEFT JOIN tblDishCustomerCardInfo dci on dc.id = dci.customerId
	WHERE dci.customerId IS NULL

END


