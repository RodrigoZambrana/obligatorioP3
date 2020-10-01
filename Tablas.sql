CREATE TABLE [dbo].[Usuarios] (
    [Cedula]          NVARCHAR (50) NOT NULL,
    [Nombre]          NVARCHAR (50) NOT NULL,
    [Apellido]        NVARCHAR (50) NOT NULL,
    [FechaNacimiento] DATETIME      NOT NULL,
    [Password]        NVARCHAR (50) NOT NULL,
    [Tipo]            NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Cedula] ASC)
);

CREATE TABLE [dbo].[Solicitantes] (
    [Email]   NVARCHAR (50) NOT NULL,
    [Celular] NVARCHAR (50) NOT NULL,
    [Cedula]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Cedula] ASC),
    CONSTRAINT [FK_Usuarios] FOREIGN KEY ([Cedula]) REFERENCES [dbo].[Usuarios] ([Cedula])
);

CREATE TABLE [dbo].[Admin] (
    [Cedula] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Cedula] ASC),
    CONSTRAINT [FK_Usu] FOREIGN KEY ([Cedula]) REFERENCES [dbo].[Usuarios] ([Cedula])
);

CREATE TABLE [dbo].[Proyectos] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]            NVARCHAR (50) NOT NULL,
    [Descripcion]       NVARCHAR (50) NOT NULL,
    [Monto]             DECIMAL (18)  NOT NULL,
    [Coutas]            INT           NOT NULL,
    [NombreImagen]      NVARCHAR (50) NOT NULL,
    [Estado]            NVARCHAR (50) NOT NULL,
    [FechaPresentacion] DATETIME      NOT NULL,
    [Puntaje]           INT           NOT NULL,
    [TasaInteres]       DECIMAL (18)  NOT NULL,
    [Tipo]              NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Personales] (
    [Experiencia] NVARCHAR (50) NOT NULL,
    [Id]          INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Proyectos] FOREIGN KEY ([Id]) REFERENCES [dbo].[Proyectos] ([Id])
);

CREATE TABLE [dbo].[Cooperativos] (
    [Id]              INT NOT NULL,
    [CantIntegrantes] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pro] FOREIGN KEY ([Id]) REFERENCES [dbo].[Proyectos] ([Id])
);


