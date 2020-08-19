USE [EasyBudget]
GO


INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'1ede313d-a531-4e79-ab34-06270b7bdb0c', N'RequestorEditBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'b03d9311-6938-4cf4-98c5-801e18d37d58', N'RequestorDeleteBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'0af011ae-6681-47e8-a408-8bfef004208a', N'RequestorViewBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'8863e973-d2d6-4c02-ad92-a4dc37706c6f', N'RequestorAddBudgetRequest')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'My 2 dept')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', N'My dept')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'2450b213-73b0-4136-93f8-5a2eed28eefc', N'My dept')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (N'6a875efe-05ef-4137-889a-137df8c67ab2', N'Tsybulkin Kyrylo', N'Chipolino', N'Lukovka01')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (N'3148ce2c-540e-4cc4-a372-42e0c29a478b', N'Deny Dipson', N'Dipson001', N'Den1985Dip')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'Vitalik Dopkin', N'Vetal', N'dopVetal1')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'Zheka Krasniy', N'RedOne', N'003kras004')

USE [EasyBudget]
GO

INSERT INTO [dbo].[BudgetRequest]
           ([Id]
           ,[Name]
           ,[DateRequested]
           ,[DateRequestedDeadline]
           ,[DateDirectorApprove]
           ,[DateStartExecution]
           ,[DateDeadlineExecution]
           ,[DateEndExecution]
           ,[EstimatedPrice]
           ,[RealPrice]
           ,[State]
           ,[Approver_Id]
           ,[Department_Id]
           ,[Executor_Id]
           ,[Requester_Id])
     VALUES
           (NEWID()
           ,'Table lampe'
           ,GETDATE()
           ,null
           ,null
           ,null
           ,null
           ,null
           ,200,6
           ,null
           ,1
           ,null
           ,'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c'
           ,null
           ,'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT INTO [dbo].[BudgetRequest]
           ([Id]
           ,[Name]
           ,[DateRequested]
           ,[DateRequestedDeadline]
           ,[DateDirectorApprove]
           ,[DateStartExecution]
           ,[DateDeadlineExecution]
           ,[DateEndExecution]
           ,[EstimatedPrice]
           ,[RealPrice]
           ,[State]
           ,[Approver_Id]
           ,[Department_Id]
           ,[Executor_Id]
           ,[Requester_Id])
     VALUES
           (NEWID()
           ,'New PC'
           ,'2020-06-21'
           ,'2020-10-01'
           ,'2020-08-18'
           ,null
           ,null
           ,null
           ,30000
           ,32445
           ,4
           ,'6a875efe-05ef-4137-889a-137df8c67ab2'
           ,'03de38f2-5445-4c9d-a039-2b47a1668a3f'
           ,'7bb4db6d-2072-4258-809b-c7a5bbe2d392'
           ,'2873d99d-a793-4b51-8ee9-484b6b30d5bb')



