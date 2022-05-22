
-- =============================================
-- Author:		Newaz Sharif
-- Create date: 16 May 2022
-- =============================================
CREATE PROCEDURE [dbo].[gsp_getInternetCustomerCardInfoForDropdown]
	
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
    FROM tblInternetCustomers dc
	INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
	LEFT JOIN tblInternetCustomerCardInfo dci on dc.id = dci.customerId
	WHERE dci.customerId IS NULL

END


