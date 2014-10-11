
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 10/11/2014 17:14:32
-- Generated from EDMX file: C:\Developement\IIS Projektas\IISProjektas\IISProjektas\SQLiteDatabase.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comment_User];
GO
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Message_MessageState];
GO
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Message_UserReceiver];
GO
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Message_UserSender];
GO
    ALTER TABLE [User_UserGroup] DROP CONSTRAINT [FK_User_UserGroup_User];
GO
    ALTER TABLE [Users] DROP CONSTRAINT [FK_User_UserState];
GO
    ALTER TABLE [User_UserGroup] DROP CONSTRAINT [FK_UserGroup_User_UserGroup];
GO
    ALTER TABLE [Advertisements] DROP CONSTRAINT [FK_Advertisement_Category];
GO
    ALTER TABLE [Advertisements] DROP CONSTRAINT [FK_Advertisement_User];
GO
    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comment_Advertisement];
GO
    ALTER TABLE [Ratings] DROP CONSTRAINT [FK_Rating_Advertisement];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [Users];
GO
    DROP TABLE [User_UserGroup];
GO
    DROP TABLE [UserGroups];
GO
    DROP TABLE [Categories];
GO
    DROP TABLE [Comments];
GO
    DROP TABLE [Messages];
GO
    DROP TABLE [MessageStates];
GO
    DROP TABLE [UserStates];
GO
    DROP TABLE [Ratings];
GO
    DROP TABLE [Advertisements];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(100)  NOT NULL,
    [email] nvarchar(100)  NOT NULL,
    [password] nvarchar(100)  NOT NULL,
    [state] int  NULL
);
GO

-- Creating table 'User_UserGroup'
CREATE TABLE [User_UserGroup] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NOT NULL,
    [user_group_id] int  NOT NULL
);
GO

-- Creating table 'UserGroups'
CREATE TABLE [UserGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [permisions] ntext  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [text] nvarchar(2000)  NOT NULL,
    [advertisement_id] int  NOT NULL,
    [user_id] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [text] nvarchar(2000)  NOT NULL,
    [state] int  NOT NULL,
    [sender_id] int  NOT NULL,
    [receiver_id] int  NOT NULL
);
GO

-- Creating table 'MessageStates'
CREATE TABLE [MessageStates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'UserStates'
CREATE TABLE [UserStates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [Ratings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [rating1] int  NOT NULL,
    [advertisement_id] int  NOT NULL
);
GO

-- Creating table 'Advertisements'
CREATE TABLE [Advertisements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [description] nvarchar(100)  NOT NULL,
    [category_id] int  NOT NULL,
    [user_id] int  NOT NULL,
    [image] image  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'User_UserGroup'
ALTER TABLE [User_UserGroup]
ADD CONSTRAINT [PK_User_UserGroup]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'UserGroups'
ALTER TABLE [UserGroups]
ADD CONSTRAINT [PK_UserGroups]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'MessageStates'
ALTER TABLE [MessageStates]
ADD CONSTRAINT [PK_MessageStates]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'UserStates'
ALTER TABLE [UserStates]
ADD CONSTRAINT [PK_UserStates]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Ratings'
ALTER TABLE [Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Advertisements'
ALTER TABLE [Advertisements]
ADD CONSTRAINT [PK_Advertisements]
    PRIMARY KEY ([Id] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'Comments'
ALTER TABLE [Comments]
ADD CONSTRAINT [FK_Comment_User]
    FOREIGN KEY ([user_id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comment_User'
CREATE INDEX [IX_FK_Comment_User]
ON [Comments]
    ([user_id]);
GO

-- Creating foreign key on [state] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [FK_Message_MessageState]
    FOREIGN KEY ([state])
    REFERENCES [MessageStates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_MessageState'
CREATE INDEX [IX_FK_Message_MessageState]
ON [Messages]
    ([state]);
GO

-- Creating foreign key on [receiver_id] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [FK_Message_UserReceiver]
    FOREIGN KEY ([receiver_id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_UserReceiver'
CREATE INDEX [IX_FK_Message_UserReceiver]
ON [Messages]
    ([receiver_id]);
GO

-- Creating foreign key on [sender_id] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [FK_Message_UserSender]
    FOREIGN KEY ([sender_id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_UserSender'
CREATE INDEX [IX_FK_Message_UserSender]
ON [Messages]
    ([sender_id]);
GO

-- Creating foreign key on [user_id] in table 'User_UserGroup'
ALTER TABLE [User_UserGroup]
ADD CONSTRAINT [FK_User_UserGroup_User]
    FOREIGN KEY ([user_id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_UserGroup_User'
CREATE INDEX [IX_FK_User_UserGroup_User]
ON [User_UserGroup]
    ([user_id]);
GO

-- Creating foreign key on [state] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [FK_User_UserState]
    FOREIGN KEY ([state])
    REFERENCES [UserStates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_UserState'
CREATE INDEX [IX_FK_User_UserState]
ON [Users]
    ([state]);
GO

-- Creating foreign key on [user_group_id] in table 'User_UserGroup'
ALTER TABLE [User_UserGroup]
ADD CONSTRAINT [FK_UserGroup_User_UserGroup]
    FOREIGN KEY ([user_group_id])
    REFERENCES [UserGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroup_User_UserGroup'
CREATE INDEX [IX_FK_UserGroup_User_UserGroup]
ON [User_UserGroup]
    ([user_group_id]);
GO

-- Creating foreign key on [category_id] in table 'Advertisements'
ALTER TABLE [Advertisements]
ADD CONSTRAINT [FK_Advertisement_Category]
    FOREIGN KEY ([category_id])
    REFERENCES [Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Advertisement_Category'
CREATE INDEX [IX_FK_Advertisement_Category]
ON [Advertisements]
    ([category_id]);
GO

-- Creating foreign key on [user_id] in table 'Advertisements'
ALTER TABLE [Advertisements]
ADD CONSTRAINT [FK_Advertisement_User]
    FOREIGN KEY ([user_id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Advertisement_User'
CREATE INDEX [IX_FK_Advertisement_User]
ON [Advertisements]
    ([user_id]);
GO

-- Creating foreign key on [advertisement_id] in table 'Comments'
ALTER TABLE [Comments]
ADD CONSTRAINT [FK_Comment_Advertisement]
    FOREIGN KEY ([advertisement_id])
    REFERENCES [Advertisements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comment_Advertisement'
CREATE INDEX [IX_FK_Comment_Advertisement]
ON [Comments]
    ([advertisement_id]);
GO

-- Creating foreign key on [advertisement_id] in table 'Ratings'
ALTER TABLE [Ratings]
ADD CONSTRAINT [FK_Rating_Advertisement]
    FOREIGN KEY ([advertisement_id])
    REFERENCES [Advertisements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Rating_Advertisement'
CREATE INDEX [IX_FK_Rating_Advertisement]
ON [Ratings]
    ([advertisement_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------