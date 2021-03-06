USE [PCOHRAppDB]
GO
/****** Object:  StoredProcedure [dbo].[gsp_getDishCustomers]    Script Date: 6/10/2022 12:03:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Newaz Sharif
-- Create date: 19 Apr 2020
-- =============================================
ALTER PROCEDURE [dbo].[gsp_getDishCustomers]
	
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
    FROM tblDishCustomers dc
	INNER JOIN tblCustomerSerial cs on dc.customerSerialId = cs.customerSerialId
	left join tblHosts h on dc.hostId = h.hostId
	left join tblZones z on dc.zoneId = z.zoneId
	left join tblUsers u on dc.assignedUserId = u.userId
	left join tblYear y on dc.connYear = y.yearId
	


END


