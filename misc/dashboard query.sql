
select  USR.UserId, USR.[UserName], SR.RequestStatusID, count (SR.RequestStatusID) NoOfRequests
From [htSolutionUk].[dbo].[ServiceRequest] SR 
JOIN [htSolutionUk].[dbo].[ServiceDetails] SD on SR.ServiceDetailsId= SD.ServiceDetailsId
JOIN [htSolutionUk].[dbo].[User] USR on USR.UserId=SD.EngineerUserId
Group by USR.UserId,USR.[UserName] ,SR.RequestStatusID
order by USR.[UserName], SR.RequestStatusID