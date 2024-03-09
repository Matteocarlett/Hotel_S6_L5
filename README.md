# Tabella Admin

USE [Hotel]

GO

/****** Object:  Table [dbo].[Admin]    Script Date: 09/03/2024 11:40:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

# Tabella Customers

USE [Hotel]
GO

/****** Object:  Table [dbo].[Customers]    Script Date: 09/03/2024 11:41:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Cognome] [nvarchar](50) NOT NULL,
	[CodiceFiscale] [nvarchar](16) NOT NULL,
	[Città] [nvarchar](50) NOT NULL,
	[Provincia] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Cellulare] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


# Tabella Prenotazione

USE [Hotel]
GO

/****** Object:  Table [dbo].[Prenotazione]    Script Date: 09/03/2024 11:43:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prenotazione](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataPrenotazione] [date] NOT NULL,
	[Anno] [int] NOT NULL,
	[CheckIn] [date] NOT NULL,
	[CheckOut] [date] NOT NULL,
	[Caparra] [decimal](10, 2) NOT NULL,
	[Tariffa] [decimal](10, 2) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[SelezionePensione] [bit] NOT NULL,
	[Colazione] [bit] NOT NULL,
	[NumeroStanza] [int] NOT NULL,
 CONSTRAINT [PK_Prenotazione] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazione_Customers] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Customers] ([Id])
GO

ALTER TABLE [dbo].[Prenotazione] CHECK CONSTRAINT [FK_Prenotazione_Customers]
GO

ALTER TABLE [dbo].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazione_Rooms] FOREIGN KEY([NumeroStanza])
REFERENCES [dbo].[Rooms] ([Id])
GO

ALTER TABLE [dbo].[Prenotazione] CHECK CONSTRAINT [FK_Prenotazione_Rooms]
GO


# Tabella Rooms
USE [Hotel]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 09/03/2024 11:43:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](150) NOT NULL,
	[Tipologia] [bit] NOT NULL,
	[Numero] [int] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



# Tabella ServiziAggiuntivi

USE [Hotel]
GO

/****** Object:  Table [dbo].[ServiziAggiuntivi]    Script Date: 09/03/2024 11:43:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiziAggiuntivi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NOT NULL,
	[Quantità] [int] NOT NULL,
	[Prezzo] [decimal](10, 2) NOT NULL,
	[IdPrenotazione] [int] NOT NULL,
	[Servizio] [nvarchar](50) NULL,
 CONSTRAINT [PK_ServiziAggiuntivi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ServiziAggiuntivi]  WITH CHECK ADD  CONSTRAINT [FK_ServiziAggiuntivi_Prenotazione] FOREIGN KEY([IdPrenotazione])
REFERENCES [dbo].[Prenotazione] ([Id])
GO

ALTER TABLE [dbo].[ServiziAggiuntivi] CHECK CONSTRAINT [FK_ServiziAggiuntivi_Prenotazione]
GO



