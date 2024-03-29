USE [TouristGuider]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[CarID] [bigint] IDENTITY(1,1) NOT NULL,
	[RtID] [bigint] NULL,
	[CarNm] [varchar](50) NULL,
	[CarRt] [varchar](50) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credential](
	[CredID] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[RoleID] [bigint] NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[CredID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[FdID] [bigint] IDENTITY(1,1) NOT NULL,
	[RstID] [bigint] NULL,
	[FdNm] [varchar](50) NULL,
	[FdRt] [varchar](50) NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[FdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodOrder]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodOrder](
	[FdOdrID] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[isPaid] [bit] NULL,
	[UserID] [bigint] NULL,
	[Ttl] [varchar](50) NULL,
 CONSTRAINT [PK_FoodOrder] PRIMARY KEY CLUSTERED 
(
	[FdOdrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodOrderDetails]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodOrderDetails](
	[FdOdrDtID] [bigint] IDENTITY(1,1) NOT NULL,
	[FdID] [bigint] NULL,
	[Qty] [varchar](50) NULL,
 CONSTRAINT [PK_FoodOrderDetails] PRIMARY KEY CLUSTERED 
(
	[FdOdrDtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[HtlID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[HtlNm] [varchar](50) NULL,
	[HtlLctn] [varchar](50) NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[HtlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentBooking]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentBooking](
	[RtBkID] [bigint] IDENTITY(1,1) NOT NULL,
	[CarID] [bigint] NULL,
	[DateBooked] [date] NULL,
	[DateReturned] [date] NULL,
	[NoofDays] [varchar](50) NULL,
	[TtlRt] [varchar](50) NULL,
	[isReturned] [bit] NULL,
	[UserID] [bigint] NULL,
 CONSTRAINT [PK_RentBooking] PRIMARY KEY CLUSTERED 
(
	[RtBkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentCar]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentCar](
	[RtID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[RtNm] [varchar](50) NULL,
 CONSTRAINT [PK_RentCar] PRIMARY KEY CLUSTERED 
(
	[RtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurant](
	[RstID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[RstNm] [varchar](50) NULL,
	[RstLctn] [varchar](50) NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[RstID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleNm] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RmID] [bigint] IDENTITY(1,1) NOT NULL,
	[HtlID] [bigint] NULL,
	[RmNm] [varchar](50) NULL,
	[RmRt] [varchar](50) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomBooking]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomBooking](
	[RmBkID] [bigint] IDENTITY(1,1) NOT NULL,
	[RmID] [bigint] NULL,
	[TtlRt] [varchar](50) NULL,
	[ChkInDt] [date] NULL,
	[ChlOtDt] [date] NULL,
	[isCheckout] [bit] NULL,
	[UserID] [bigint] NULL,
 CONSTRAINT [PK_RoomBooking] PRIMARY KEY CLUSTERED 
(
	[RmBkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13-04-2020 16:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_RentCar] FOREIGN KEY([RtID])
REFERENCES [dbo].[RentCar] ([RtID])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_RentCar]
GO
ALTER TABLE [dbo].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Credential] CHECK CONSTRAINT [FK_Credential_Role]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Restaurant] FOREIGN KEY([RstID])
REFERENCES [dbo].[Restaurant] ([RstID])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Restaurant]
GO
ALTER TABLE [dbo].[FoodOrder]  WITH CHECK ADD  CONSTRAINT [FK_FoodOrder_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[FoodOrder] CHECK CONSTRAINT [FK_FoodOrder_User]
GO
ALTER TABLE [dbo].[FoodOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_FoodOrderDetails_Food] FOREIGN KEY([FdID])
REFERENCES [dbo].[Food] ([FdID])
GO
ALTER TABLE [dbo].[FoodOrderDetails] CHECK CONSTRAINT [FK_FoodOrderDetails_Food]
GO
ALTER TABLE [dbo].[Hotel]  WITH CHECK ADD  CONSTRAINT [FK_Hotel_Credential] FOREIGN KEY([CredID])
REFERENCES [dbo].[Credential] ([CredID])
GO
ALTER TABLE [dbo].[Hotel] CHECK CONSTRAINT [FK_Hotel_Credential]
GO
ALTER TABLE [dbo].[RentBooking]  WITH CHECK ADD  CONSTRAINT [FK_RentBooking_Car] FOREIGN KEY([CarID])
REFERENCES [dbo].[Car] ([CarID])
GO
ALTER TABLE [dbo].[RentBooking] CHECK CONSTRAINT [FK_RentBooking_Car]
GO
ALTER TABLE [dbo].[RentBooking]  WITH CHECK ADD  CONSTRAINT [FK_RentBooking_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[RentBooking] CHECK CONSTRAINT [FK_RentBooking_User]
GO
ALTER TABLE [dbo].[RentCar]  WITH CHECK ADD  CONSTRAINT [FK_RentCar_Credential] FOREIGN KEY([CredID])
REFERENCES [dbo].[Credential] ([CredID])
GO
ALTER TABLE [dbo].[RentCar] CHECK CONSTRAINT [FK_RentCar_Credential]
GO
ALTER TABLE [dbo].[Restaurant]  WITH CHECK ADD  CONSTRAINT [FK_Restaurant_Credential] FOREIGN KEY([CredID])
REFERENCES [dbo].[Credential] ([CredID])
GO
ALTER TABLE [dbo].[Restaurant] CHECK CONSTRAINT [FK_Restaurant_Credential]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotel] FOREIGN KEY([HtlID])
REFERENCES [dbo].[Hotel] ([HtlID])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotel]
GO
ALTER TABLE [dbo].[RoomBooking]  WITH CHECK ADD  CONSTRAINT [FK_RoomBooking_Room] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[RoomBooking] CHECK CONSTRAINT [FK_RoomBooking_Room]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Credential] FOREIGN KEY([CredID])
REFERENCES [dbo].[Credential] ([CredID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Credential]
GO
