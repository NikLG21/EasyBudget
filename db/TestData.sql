USE [EasyBudget]
GO


INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'1ede313d-a531-4e79-ab34-06270b7bdb0c', N'RequestorEditBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'b03d9311-6938-4cf4-98c5-801e18d37d58', N'RequestorDeleteBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'0af011ae-6681-47e8-a408-8bfef004208a', N'RequestorViewBudgetRequest')
INSERT [dbo].[Action] ([Id], [Name]) VALUES (N'8863e973-d2d6-4c02-ad92-a4dc37706c6f', N'RequestorAddBudgetRequest')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'IT департамент')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', N'Хозчасть')

INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'935d8abb-2e78-40ba-9b86-05d8a342d0fc', N'Користувач', NULL)
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'63193d69-d80d-4d43-bd43-1690e1731626', N'Фінансовий директор', NULL)
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'aab78899-6781-4a42-b7a0-18c18ca652d4', N'Директор', NULL)
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'cb294f90-3cb6-4169-9916-1e39d7f4bd3d', N'Ініціатор запиту', NULL)
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'111c670c-fc32-4101-8a90-421da79d708b', N'Виконавець IT', N'03de38f2-5445-4c9d-a039-2b47a1668a3f')
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'6594a73c-6bed-4a07-badd-6c32e730083e', N'Виконавець', N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c')
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'3dfeb0d5-cbb7-4855-b882-760b3a912dcd', N'Затверджувач', NULL)
INSERT [dbo].[Role] ([Id], [Name], [Department_Id]) VALUES (N'3db8ed7c-07d6-4240-876f-c6989baf5748', N'Адмін', NULL)

INSERT [dbo].[User] ([Id], [Name], [Login], [Password],[IsDisabled]) VALUES (N'6a875efe-05ef-4137-889a-137df8c67ab2', N'Кирил Цибулькин', N'Chipolino', N'Lukovka01',0)
INSERT [dbo].[User] ([Id], [Name], [Login], [Password],[IsDisabled]) VALUES (N'3148ce2c-540e-4cc4-a372-42e0c29a478b', N'Дени Дипсон', N'Dipson001', N'Den1985Dip',0)
INSERT [dbo].[User] ([Id], [Name], [Login], [Password],[IsDisabled]) VALUES (N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'Виталик Допкин', N'Vetal', N'dopVetal1',0)
INSERT [dbo].[User] ([Id], [Name], [Login], [Password],[IsDisabled]) VALUES (N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'Евгений Красный', N'RedOne', N'003kras004',0)
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'5965a5b5-f5ca-4995-b642-3206f33668da', N'Тумбочка для хранения личных вещей на время работы', CAST(N'2020-07-30T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', NULL, N'2873d99d-a793-4b51-8ee9-484b6b30d5bb')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'bbf0b6f5-8686-40ed-a945-3b17d8f5234f', N'Маски медицинские - 50 шт.', CAST(N'2020-08-08T00:00:00.000' AS DateTime), CAST(N'2020-09-15T00:00:00.000' AS DateTime), CAST(N'2020-08-19T00:00:00.000' AS DateTime), CAST(N'2020-08-20T00:00:00.000' AS DateTime), CAST(N'2020-09-01T00:00:00.000' AS DateTime), NULL, CAST(200.00 AS Decimal(18, 2)), CAST(190.00 AS Decimal(18, 2)), 9, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'457c8cb2-4feb-41ef-8134-45a98034f4b4', N'Toshiba Xario soft - УЗИ апарат', CAST(N'2020-07-21T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, CAST(400000.00 AS Decimal(18, 2)), CAST(406000.00 AS Decimal(18, 2)), 6, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'1f7559bb-73d7-496c-9300-5ced606f1fdd', N'Новые кресла для пациентов', CAST(N'2020-08-10T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(400.00 AS Decimal(18, 2)), CAST(379.00 AS Decimal(18, 2)), 7, N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'6a875efe-05ef-4137-889a-137df8c67ab2')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'18a858ac-3cb1-4bb5-b13c-737c2ec73fe1', N'Аппарат искусственной вентиляции легких S1200 экспертного класса', CAST(N'2020-06-02T00:00:00.000' AS DateTime), CAST(N'2020-07-31T00:00:00.000' AS DateTime), CAST(N'2020-06-18T00:00:00.000' AS DateTime), CAST(N'2020-06-23T00:00:00.000' AS DateTime), CAST(N'2020-07-15T00:00:00.000' AS DateTime), CAST(N'2020-07-02T00:00:00.000' AS DateTime), CAST(1000000.00 AS Decimal(18, 2)), CAST(800000.00 AS Decimal(18, 2)), 10, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'6c97593f-9b9a-4cd0-ad2a-75bbec4c9a7f', N'Анализатор глюкозы', CAST(N'2020-08-01T00:00:00.000' AS DateTime), CAST(N'2020-10-01T00:00:00.000' AS DateTime), CAST(N'2020-08-18T00:00:00.000' AS DateTime), NULL, NULL, NULL, CAST(80000.00 AS Decimal(18, 2)), CAST(78000.00 AS Decimal(18, 2)), 8, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'0159de4c-f25d-4b93-9cec-760b6e555856', N'Новый компьютер', CAST(N'2020-06-21T00:00:00.000' AS DateTime), CAST(N'2020-10-01T00:00:00.000' AS DateTime), CAST(N'2020-08-18T00:00:00.000' AS DateTime), NULL, NULL, NULL, CAST(30000.00 AS Decimal(18, 2)), CAST(32445.00 AS Decimal(18, 2)), 4, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'7bb4db6d-2072-4258-809b-c7a5bbe2d392', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'a9a2269f-e2c5-4970-940b-85f5df4a5a69', N'Наушники в колцентр', CAST(N'2020-08-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'3e2e7845-aa42-46e8-9d53-94afa625a68c', N'2 роутера вайфай для второй клиники', CAST(N'2020-05-07T00:00:00.000' AS DateTime), CAST(N'2020-11-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, CAST(1250.00 AS Decimal(18, 2)), 2, N'6a875efe-05ef-4137-889a-137df8c67ab2', N'03de38f2-5445-4c9d-a039-2b47a1668a3f', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb', N'2873d99d-a793-4b51-8ee9-484b6b30d5bb')
INSERT [dbo].[BudgetRequest] ([Id], [Name], [DateRequested], [DateRequestedDeadline], [DateDirectorApprove], [DateStartExecution], [DateDeadlineExecution], [DateEndExecution], [EstimatedPrice], [RealPrice], [State], [Approver_Id], [Department_Id], [Executor_Id], [Requester_Id]) 
VALUES (N'44ebb261-b7f9-484c-9759-a7855a246d6a', N'Настольная лампа', CAST(N'2020-08-20T13:02:20.267' AS DateTime), NULL, NULL, NULL, NULL, NULL, CAST(200.60 AS Decimal(18, 2)), NULL, 1, NULL, N'22946ba4-b06c-4d9e-a0d3-2e03b62afb5c', NULL, N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetDescription] ([Id], [Description], [Date], [BudgetRequest_Id], [User_Id]) VALUES (N'af5a0a59-3210-4ea5-8207-d7d9a841c72c', N'Это очень хороший новый ультразвуковой аппарат. Он имеет много разных преимуществ по сравнению со старыми копиями.', CAST(N'2020-07-21T00:00:00.000' AS DateTime), N'457c8cb2-4feb-41ef-8134-45a98034f4b4', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
INSERT [dbo].[BudgetDescription] ([Id], [Description], [Date], [BudgetRequest_Id], [User_Id]) VALUES (N'027745f2-e6a7-4244-aa5e-f3c0d99ca218', N'Не думаю, что этот аппарат подходит для нашей клиники. Мы можем найти вариант получше. Я одобряю этот запрос, но сомневаюсь в этой модели.', CAST(N'2020-08-03T00:00:00.000' AS DateTime), N'457c8cb2-4feb-41ef-8134-45a98034f4b4', N'6a875efe-05ef-4137-889a-137df8c67ab2')
INSERT [dbo].[BudgetDescription] ([Id], [Description], [Date], [BudgetRequest_Id], [User_Id]) VALUES (N'6dfc0cf6-d03f-4d69-a2e8-f5c229e7fbfb', N'У нас слишком мало защитных масок и по моим расчетам они закончатся в середине сентября. Их запасы должны быть пополнены', CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bbf0b6f5-8686-40ed-a945-3b17d8f5234f', N'3148ce2c-540e-4cc4-a372-42e0c29a478b')
