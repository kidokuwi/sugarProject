CREATE TABLE [dbo].[users] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [uName]              VARCHAR (20) NOT NULL,
    [lName]              VARCHAR (20) NOT NULL,
    [fName]              VARCHAR (20) NOT NULL,
    [eMail]              VARCHAR (30) NOT NULL,
    [gender]             VARCHAR (6)  NOT NULL,
    [yearBorn]           INT          NOT NULL,
    [prefix]             VARCHAR (3)  NULL,
    [phone]              VARCHAR (7)  NULL,
    [eatsWhiteBread]     BIT          NULL,
    [eatsSugarRegularly] BIT          NULL,
    [doesSports]         BIT          NULL,
    [likesBrownBread]    BIT          NULL,
    [pass]               VARCHAR (10) NOT NULL
);

