CREATE TABLE [dbo].[DietPlan] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sunday] nvarchar(max)  NOT NULL,
    [Monday] nvarchar(max)  NOT NULL,
    [Tuesday] nvarchar(max)  NOT NULL,
    [Wednesday] nvarchar(max)  NOT NULL,
    [Thursday] nvarchar(max)  NOT NULL,
    [Friday] nvarchar(max)  NOT NULL,
    [Saturday] nvarchar(max)  NOT NULL,
    [Date_added] datetime  NOT NULL,
    [User_Id] nvarchar(128) NOT NULL,
    [Rating] int,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    
   FOREIGN KEY (User_Id) REFERENCES AspNetUsers(Id)
);
GO