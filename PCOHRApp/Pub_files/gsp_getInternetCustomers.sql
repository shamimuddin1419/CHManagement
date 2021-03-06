USE [PCOHRAppDB]
GO
/****** Object:  StoredProcedure [dbo].[gsp_getInternetCustomers]    Script Date: 6/10/2022 4:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Newaz Sharif
-- Create date: 25 Apr 2020
-- =============================================
ALTER PROCEDURE [dbo].[gsp_getInternetCustomers]
	
AS
BEGIN
	SELECT 
			id,
			customerId,
			dc.customerSerialId,
			cast(ltrim(rtrim(customerSerialName)) as varchar) + cast(dc.customerId as varchar(20)) as customerSerial, 
			customerName,
			customerPhone,
			customerAddress,
			requiredNet,
			ipAddress,
			dc.hostId,
			h.hostName,
			dc.zoneId,
			z.zoneName,
			assignedUserId,
			isnull(u.userName,'') as assignedUserName,
			connFee,
			monthBill,
			othersAmount,
			[description],
			connMonth,
			connYear,
			y.yearName as connYearName,
			dc.isActive,
			case when dc.isActive = 1 then 'Yes' else 'No' end isActiveString,
			isnull(dc.nid,'') as nid
    FROM tblInternetCustomers dc
	INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
	left join tblHosts h on dc.hostId = h.hostId
	left join tblZones z on dc.zoneId = z.zoneId
	left join tblUsers u on dc.assignedUserId = u.userId
	left join tblYear y on dc.connYear = y.yearId
	


END


