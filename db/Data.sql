USE [master]
GO
/****** Object:  Database [DB_CLIENT]    Script Date: 6/05/2024 00:46:11 ******/
CREATE DATABASE [DB_CLIENT]
GO

USE [DB_CLIENT]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/05/2024 00:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 6/05/2024 00:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [nvarchar](450) NOT NULL,
	[Filename] [nvarchar](max) NOT NULL,
	[ContentType] [nvarchar](max) NOT NULL,
	[Section] [nvarchar](max) NOT NULL,
	[ReferenceId] [nvarchar](max) NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 6/05/2024 00:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Lastname] [nvarchar](max) NOT NULL,
	[BirthDate] [datetime2](7) NOT NULL,
	[DocumentType] [nvarchar](max) NOT NULL,
	[DocumentNumber] [nvarchar](max) NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240506045355_InitialCreate', N'8.0.4')
GO
INSERT [dbo].[Attachments] ([Id], [Filename], [ContentType], [Section], [ReferenceId], [State]) VALUES (N'074b81f3-0642-445e-a721-8457a72a8a60', N'voucher.png', N'image/png', N'PROFILE', N'1', 1)
INSERT [dbo].[Attachments] ([Id], [Filename], [ContentType], [Section], [ReferenceId], [State]) VALUES (N'406fe656-9f02-49a7-9a79-61fdf232e692', N'Horario-para-imprimir-de-clase-semanal.pdf', N'application/pdf', N'CV', N'1', 0)
INSERT [dbo].[Attachments] ([Id], [Filename], [ContentType], [Section], [ReferenceId], [State]) VALUES (N'88a9410e-4f80-4ac4-b5e9-cc78d9a6b1c1', N'Plataforma-EDItran-Manual-de-usuario-Windows.pdf', N'application/pdf', N'CV', N'1', 1)
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [Name], [Lastname], [BirthDate], [DocumentType], [DocumentNumber], [State]) VALUES (1, N'Yoliston', N'Herrera', CAST(N'1999-11-06T00:00:00.0000000' AS DateTime2), N'DNI', N'76272866', 1)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
USE [master]
GO
ALTER DATABASE [DB_CLIENT] SET  READ_WRITE 
GO
