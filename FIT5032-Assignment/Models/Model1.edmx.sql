
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/06/2021 03:22:02
-- Generated from EDMX file: C:\Users\ahr_a\source\repos\FIT5032-Assignment\FIT5032-Assignment\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HealthProDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MealPlans'
CREATE TABLE [dbo].[MealPlans] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sunday] nvarchar(max)  NOT NULL,
    [Monday] nvarchar(max)  NOT NULL,
    [Tuesday] nvarchar(max)  NOT NULL,
    [Wednesday] nvarchar(max)  NOT NULL,
    [Thursday] nvarchar(max)  NOT NULL,
    [Friday] nvarchar(max)  NOT NULL,
    [Saturday] nvarchar(max)  NOT NULL,
    [Date_added] datetime  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Full_Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MealPlans'
ALTER TABLE [dbo].[MealPlans]
ADD CONSTRAINT [PK_MealPlans]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'MealPlans'
ALTER TABLE [dbo].[MealPlans]
ADD CONSTRAINT [FK_MealPlanUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealPlanUser'
CREATE INDEX [IX_FK_MealPlanUser]
ON [dbo].[MealPlans]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------