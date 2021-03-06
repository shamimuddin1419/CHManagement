USE [PCOHRAppDB]
GO
/****** Object:  StoredProcedure [dbo].[gsp_getInternetCustomerById]    Script Date: 6/10/2022 4:16:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Newaz Sharif
-- Create date: 25 Apr 2020
-- =============================================
ALTER PROCEDURE [dbo].[gsp_getInternetCustomerById]
		@id int
AS
BEGIN
	SELECT 
			id,
			customerId,
			dc.customerSerialId,
			cast(ltrim(rtrim(customerSerialName)) as varchar)+cast(dc.customerId as varchar(20)) as customerSerial,
			customerName,
			customerPhone,
			customerAddress,
			requiredNet,
			ipAddress,
			dc.hostId,
			h.hostName,
			h.hostPhone,
			dc.zoneId,
			z.zoneName,
			assignedUserId,
			isnull(u.userName,'') as assignedUserName,
			assignedUserId,
			connFee,
			monthBill,
			othersAmount,
			[description],
			connMonth,
			connYear,
			y.yearName as connYearName,
			dc.isActive
			,convert(varchar, dc.EntryDate, 103) as EntryDateString,
			dc.EntryDate,
			dc.OnuMC,
			isnull(dc.nid,'') as nid,
		    case when dc.isDisconnected = 1 and dc.disconnectionEffectiveDate <= GETDATE() then  'Disconnected' else 'N/A' end as isDisconnectedString,
			case when dc.isDisconnected = 1 and dc.disconnectionEffectiveDate <= GETDATE() then convert(varchar, dc.disconnectionEffectiveDate, 103) else '' end as disconnectedDateString
    FROM tblInternetCustomers dc
	inner join tblCustomerSerial tc on tc.customerSerialId = dc.customerSerialId
	left join tblHosts h on dc.hostId = h.hostId
	left join tblZones z on dc.zoneId = z.zoneId
	left join tblUsers u on dc.assignedUserId = u.userId
	left join tblYear y on dc.connYear = y.yearId
	WHERE id = @id 
	


END

