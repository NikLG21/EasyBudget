Requester



Approver

        private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("6a875efe-05ef-4137-889a-137df8c67ab2"),
            Name = "Кирил Цибулькин",
            CurrentRoleId = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            CurrentRoleName = "Approver",
            UnitId = Guid.Parse("35f0579d-a8d6-4c6a-a241-2f4726b6a9d1"),
            UnitName = "2 Клініка"
        };

        private Role role = new Role()
        {
            Id = Guid.Parse("3dfeb0d5-cbb7-4855-b882-760b3a912dcd"),
            Name = "Approver",
        };

Executor

		private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("7bb4db6d-2072-4258-809b-c7a5bbe2d392"),
            Name = "Евгений Красный",
            CurrentRoleId = Guid.Parse("6594a73c-6bed-4a07-badd-6c32e730083e"),
            CurrentRoleName = "Executor",
            DepartmentId = Guid.Parse("22946ba4-b06c-4d9e-a0d3-2e03b62afb5c"),
            DepartmentName = "Хозчасть"
        };

        private Role role = new Role()
        {
            Department = new Department()
            {
                Id = Guid.Parse("22946ba4-b06c-4d9e-a0d3-2e03b62afb5c"),
                Name = "Хозчасть"
            },
            Id = Guid.Parse("6594a73c-6bed-4a07-badd-6c32e730083e"),
            Name = "Executor",
        };

Director



FinDirector

		private UserMainInfoDto userInfo = new UserMainInfoDto()
        {
            Id = Guid.Parse("6a875efe-05ef-4137-889a-137df8c67ab2"),
            CurrentRoleId = Guid.Parse("63193d69-d80d-4d43-bd43-1690e1731626"),
            CurrentRoleName = "FinDirector",

        };

        private Role role = new Role()
        {
            Id = Guid.Parse("63193d69-d80d-4d43-bd43-1690e1731626"),
            Name = "FinDirector",
        };