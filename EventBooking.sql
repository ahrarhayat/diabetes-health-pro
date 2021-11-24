CREATE TABLE [dbo].[EventBooking] (
[Id] INT IDENTITY (1, 1) NOT NULL,
[Event_Id] nvarchar(128) NOT NULL,
[User_Id]  nvarchar(128) NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC),

FOREIGN KEY (User_Id) REFERENCES AspNetUsers(Id),
FOREIGN KEY (Event_Id) REFERENCES Events(Id)
);
