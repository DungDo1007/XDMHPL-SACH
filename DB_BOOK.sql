/****** Object:  Database [THU]    Script Date: 10/11/2017 7:25:54 PM ******/
CREATE DATABASE [THU]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'THU', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\THU.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'THU_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\THU_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [THU] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [THU].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [THU] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [THU] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [THU] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [THU] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [THU] SET ARITHABORT OFF 
GO
ALTER DATABASE [THU] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [THU] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [THU] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [THU] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [THU] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [THU] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [THU] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [THU] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [THU] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [THU] SET  DISABLE_BROKER 
GO
ALTER DATABASE [THU] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [THU] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [THU] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [THU] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [THU] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [THU] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [THU] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [THU] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [THU] SET  MULTI_USER 
GO
ALTER DATABASE [THU] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [THU] SET DB_CHAINING OFF 
GO
ALTER DATABASE [THU] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [THU] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [THU] SET DELAYED_DURABILITY = DISABLED 
GO
/****** Object:  Table [dbo].[DaiLy]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DaiLy](
	[dailyID] [int] IDENTITY(1,1) NOT NULL,
	[daily_ten] [nvarchar](50) NULL,
	[daily_diachi] [nvarchar](100) NULL,
	[daily_sdt] [text] NULL,
	[daily_user] [varchar](50) NULL,
	[daily_pass] [varchar](50) NULL,
	[daily_debtmax] [float] NULL,
 CONSTRAINT [PK_DaiLy] PRIMARY KEY CLUSTERED 
(
	[dailyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DaiLyDebt]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLyDebt](
	[dailydebtID] [int] IDENTITY(1,1) NOT NULL,
	[fk_dailyID] [int] NULL,
	[fk_sachID] [int] NULL,
	[dailydebt_soluong] [int] NULL,
 CONSTRAINT [PK_DaiLyDebt] PRIMARY KEY CLUSTERED 
(
	[dailydebtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DaiLyDebtTien]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLyDebtTien](
	[dailydebttienID] [int] IDENTITY(1,1) NOT NULL,
	[fk_dailyID] [int] NULL,
	[dailydebttien_tien] [float] NULL,
	[dailydebttien_ngaycapnhat] [datetime] NULL,
	[dailydebttien_sach] [int] NULL,
 CONSTRAINT [PK_DaiLyDebtTien] PRIMARY KEY CLUSTERED 
(
	[dailydebttienID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoanhThu]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoanhThu](
	[doanhthuID] [int] IDENTITY(1,1) NOT NULL,
	[doanhthu_tien] [float] NULL,
	[doanhthu_ngaycapnhat] [datetime] NULL,
 CONSTRAINT [PK_DoanhThu] PRIMARY KEY CLUSTERED 
(
	[doanhthuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhapSachDetails]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapSachDetails](
	[ctnhapsachID] [int] IDENTITY(1,1) NOT NULL,
	[nhapsachID] [int] NULL,
	[fk_sachID] [int] NULL,
	[soluong] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[ctnhapsachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhapSachMaster]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapSachMaster](
	[nhapsachID] [int] IDENTITY(1,1) NOT NULL,
	[fk_nxbID] [int] NULL,
	[nhapsach_ngaygiao] [datetime] NULL,
	[nhapsach_nguoigiao] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrderMaster] PRIMARY KEY CLUSTERED 
(
	[nhapsachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NXB]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NXB](
	[nxbID] [int] IDENTITY(1,1) NOT NULL,
	[nxb_ten] [nvarchar](100) NULL,
	[nxb_diachi] [nvarchar](100) NULL,
	[nxb_sdt] [varchar](50) NULL,
 CONSTRAINT [PK_publishing] PRIMARY KEY CLUSTERED 
(
	[nxbID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NXBDebt]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NXBDebt](
	[nxbdebtID] [int] IDENTITY(1,1) NOT NULL,
	[fk_nxbID] [int] NULL,
	[fk_sachID] [int] NULL,
	[nxbdebt_soluong] [int] NULL,
	[nxbdebt_banduoc] [int] NULL,
 CONSTRAINT [PK_NXBDebt] PRIMARY KEY CLUSTERED 
(
	[nxbdebtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NXBDebtTien]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NXBDebtTien](
	[nxbdebttienID] [int] IDENTITY(1,1) NOT NULL,
	[nxbdebttien_tien] [float] NULL,
	[nxbdebttien_ngaycapnhat] [datetime] NULL,
	[fk_nxbID] [int] NULL,
 CONSTRAINT [PK_NXBDebtTien] PRIMARY KEY CLUSTERED 
(
	[nxbdebttienID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sach]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[sachID] [int] IDENTITY(1,1) NOT NULL,
	[sach_ten] [nvarchar](50) NULL,
	[sach_tacgia] [nvarchar](50) NULL,
	[fk_nxbID] [int] NULL,
	[sach_gianhap] [float] NULL,
	[sach_giaxuat] [float] NULL,
	[sach_soluong] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[sachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TonKho]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TonKho](
	[tonkhoID] [int] IDENTITY(1,1) NOT NULL,
	[fk_sachID] [int] NULL,
	[tonkho_soluong] [int] NULL,
	[tonkho_ngaycapnhat] [datetime] NULL,
 CONSTRAINT [PK_tonkho] PRIMARY KEY CLUSTERED 
(
	[tonkhoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[XuatSachDetails]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatSachDetails](
	[ctxuatsachID] [int] IDENTITY(1,1) NOT NULL,
	[xuatsachID] [int] NULL,
	[fk_sachID] [int] NULL,
	[soluong] [int] NULL,
 CONSTRAINT [PK_XuatSachDetails] PRIMARY KEY CLUSTERED 
(
	[ctxuatsachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[XuatSachMaster]    Script Date: 10/11/2017 7:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatSachMaster](
	[xuatsachID] [int] IDENTITY(1,1) NOT NULL,
	[fk_dailyID] [int] NULL,
	[xuatsach_ngayorder] [datetime] NULL,
	[xuatsach_ngayupdate] [datetime] NULL,
	[xuatsach_nguoinhan] [nvarchar](50) NULL,
	[xuatsach_trangthai] [nvarchar](50) NULL,
	[xuatsach_tongtien] [float] NULL,
 CONSTRAINT [PK_XuatSach] PRIMARY KEY CLUSTERED 
(
	[xuatsachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[DaiLy] ON 

INSERT [dbo].[DaiLy] ([dailyID], [daily_ten], [daily_diachi], [daily_sdt], [daily_user], [daily_pass], [daily_debtmax]) VALUES (4, N'ĐL A', NULL, NULL, N'abc', N'81dc9bdb52d04dc20036dbd8313ed055', 200)
INSERT [dbo].[DaiLy] ([dailyID], [daily_ten], [daily_diachi], [daily_sdt], [daily_user], [daily_pass], [daily_debtmax]) VALUES (5, N'ĐL B', NULL, NULL, N'aaa', N'b59c67bf196a4758191e42f76670ceba', 600)
INSERT [dbo].[DaiLy] ([dailyID], [daily_ten], [daily_diachi], [daily_sdt], [daily_user], [daily_pass], [daily_debtmax]) VALUES (6, N'ĐL C', NULL, NULL, N'bbb', N'934b535800b1cba8f96a5d72f72f1611', 400)
INSERT [dbo].[DaiLy] ([dailyID], [daily_ten], [daily_diachi], [daily_sdt], [daily_user], [daily_pass], [daily_debtmax]) VALUES (7, N'ĐL D', NULL, NULL, N'ccc', N'2be9bd7a3434f7038ca27d1918de58bd', 500)
SET IDENTITY_INSERT [dbo].[DaiLy] OFF
SET IDENTITY_INSERT [dbo].[NXB] ON 

INSERT [dbo].[NXB] ([nxbID], [nxb_ten], [nxb_diachi], [nxb_sdt]) VALUES (4, N'NXB A', N'Quận 1', N'11111')
INSERT [dbo].[NXB] ([nxbID], [nxb_ten], [nxb_diachi], [nxb_sdt]) VALUES (5, N'NXB B', N'Quận 2', N'22222')
INSERT [dbo].[NXB] ([nxbID], [nxb_ten], [nxb_diachi], [nxb_sdt]) VALUES (6, N'NXB C', N'Quận 3', N'33333')
SET IDENTITY_INSERT [dbo].[NXB] OFF
SET IDENTITY_INSERT [dbo].[Sach] ON 

INSERT [dbo].[Sach] ([sachID], [sach_ten], [sach_tacgia], [fk_nxbID], [sach_gianhap], [sach_giaxuat], [sach_soluong]) VALUES (13, N'Sách A', N'Tố Hữu', 5, 12, 13, 162)
INSERT [dbo].[Sach] ([sachID], [sach_ten], [sach_tacgia], [fk_nxbID], [sach_gianhap], [sach_giaxuat], [sach_soluong]) VALUES (14, N'Sách B', N'Nam Cao', 4, 14, 15, 87)
INSERT [dbo].[Sach] ([sachID], [sach_ten], [sach_tacgia], [fk_nxbID], [sach_gianhap], [sach_giaxuat], [sach_soluong]) VALUES (15, N'Sách C', N'Lệ Quyên', 6, 15, 16, 99)
INSERT [dbo].[Sach] ([sachID], [sach_ten], [sach_tacgia], [fk_nxbID], [sach_gianhap], [sach_giaxuat], [sach_soluong]) VALUES (16, N'Sách D', N'Tuấn Hưng', 4, 17, 18, 100)
INSERT [dbo].[Sach] ([sachID], [sach_ten], [sach_tacgia], [fk_nxbID], [sach_gianhap], [sach_giaxuat], [sach_soluong]) VALUES (17, N'Sách E', N'Quang Dũng', 5, 19, 20, 135)
SET IDENTITY_INSERT [dbo].[Sach] OFF
ALTER TABLE [dbo].[DaiLyDebt]  WITH CHECK ADD  CONSTRAINT [FK_DaiLyDebt_DaiLy] FOREIGN KEY([fk_dailyID])
REFERENCES [dbo].[DaiLy] ([dailyID])
GO
ALTER TABLE [dbo].[DaiLyDebt] CHECK CONSTRAINT [FK_DaiLyDebt_DaiLy]
GO
ALTER TABLE [dbo].[DaiLyDebt]  WITH CHECK ADD  CONSTRAINT [FK_DaiLyDebt_Sach] FOREIGN KEY([fk_sachID])
REFERENCES [dbo].[Sach] ([sachID])
GO
ALTER TABLE [dbo].[DaiLyDebt] CHECK CONSTRAINT [FK_DaiLyDebt_Sach]
GO
ALTER TABLE [dbo].[DaiLyDebtTien]  WITH CHECK ADD  CONSTRAINT [FK_DaiLyDebtTien_DaiLy] FOREIGN KEY([fk_dailyID])
REFERENCES [dbo].[DaiLy] ([dailyID])
GO
ALTER TABLE [dbo].[DaiLyDebtTien] CHECK CONSTRAINT [FK_DaiLyDebtTien_DaiLy]
GO
ALTER TABLE [dbo].[DaiLyDebtTien]  WITH CHECK ADD  CONSTRAINT [FK_DaiLyDebtTien_DaiLy1] FOREIGN KEY([fk_dailyID])
REFERENCES [dbo].[DaiLy] ([dailyID])
GO
ALTER TABLE [dbo].[DaiLyDebtTien] CHECK CONSTRAINT [FK_DaiLyDebtTien_DaiLy1]
GO
ALTER TABLE [dbo].[NhapSachDetails]  WITH CHECK ADD  CONSTRAINT [FK_NhapSachDetails_Sach] FOREIGN KEY([fk_sachID])
REFERENCES [dbo].[Sach] ([sachID])
GO
ALTER TABLE [dbo].[NhapSachDetails] CHECK CONSTRAINT [FK_NhapSachDetails_Sach]
GO
ALTER TABLE [dbo].[NhapSachDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_OrderMaster] FOREIGN KEY([nhapsachID])
REFERENCES [dbo].[NhapSachMaster] ([nhapsachID])
GO
ALTER TABLE [dbo].[NhapSachDetails] CHECK CONSTRAINT [FK_OrderDetails_OrderMaster]
GO
ALTER TABLE [dbo].[NhapSachMaster]  WITH CHECK ADD  CONSTRAINT [FK_NhapSachMaster_NXB] FOREIGN KEY([fk_nxbID])
REFERENCES [dbo].[NXB] ([nxbID])
GO
ALTER TABLE [dbo].[NhapSachMaster] CHECK CONSTRAINT [FK_NhapSachMaster_NXB]
GO
ALTER TABLE [dbo].[NXBDebt]  WITH CHECK ADD  CONSTRAINT [FK_NXBDebt_NXB] FOREIGN KEY([fk_nxbID])
REFERENCES [dbo].[NXB] ([nxbID])
GO
ALTER TABLE [dbo].[NXBDebt] CHECK CONSTRAINT [FK_NXBDebt_NXB]
GO
ALTER TABLE [dbo].[NXBDebt]  WITH CHECK ADD  CONSTRAINT [FK_NXBDebt_Sach] FOREIGN KEY([fk_sachID])
REFERENCES [dbo].[Sach] ([sachID])
GO
ALTER TABLE [dbo].[NXBDebt] CHECK CONSTRAINT [FK_NXBDebt_Sach]
GO
ALTER TABLE [dbo].[NXBDebtTien]  WITH CHECK ADD  CONSTRAINT [FK_NXBDebtTien_NXB] FOREIGN KEY([fk_nxbID])
REFERENCES [dbo].[NXB] ([nxbID])
GO
ALTER TABLE [dbo].[NXBDebtTien] CHECK CONSTRAINT [FK_NXBDebtTien_NXB]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_NXB] FOREIGN KEY([fk_nxbID])
REFERENCES [dbo].[NXB] ([nxbID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_NXB]
GO
ALTER TABLE [dbo].[TonKho]  WITH CHECK ADD  CONSTRAINT [FK_tonkho_Sach] FOREIGN KEY([fk_sachID])
REFERENCES [dbo].[Sach] ([sachID])
GO
ALTER TABLE [dbo].[TonKho] CHECK CONSTRAINT [FK_tonkho_Sach]
GO
ALTER TABLE [dbo].[XuatSachDetails]  WITH CHECK ADD  CONSTRAINT [FK_XuatSachDetails_Sach] FOREIGN KEY([fk_sachID])
REFERENCES [dbo].[Sach] ([sachID])
GO
ALTER TABLE [dbo].[XuatSachDetails] CHECK CONSTRAINT [FK_XuatSachDetails_Sach]
GO
ALTER TABLE [dbo].[XuatSachDetails]  WITH CHECK ADD  CONSTRAINT [FK_XuatSachDetails_XuatSachMaster] FOREIGN KEY([xuatsachID])
REFERENCES [dbo].[XuatSachMaster] ([xuatsachID])
GO
ALTER TABLE [dbo].[XuatSachDetails] CHECK CONSTRAINT [FK_XuatSachDetails_XuatSachMaster]
GO
ALTER TABLE [dbo].[XuatSachMaster]  WITH CHECK ADD  CONSTRAINT [FK_XuatSachMaster_DaiLy] FOREIGN KEY([fk_dailyID])
REFERENCES [dbo].[DaiLy] ([dailyID])
GO
ALTER TABLE [dbo].[XuatSachMaster] CHECK CONSTRAINT [FK_XuatSachMaster_DaiLy]
GO
ALTER DATABASE [THU] SET  READ_WRITE 
GO
