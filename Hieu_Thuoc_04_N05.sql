create database HieuThuoc04_N05
go
use HieuThuoc04_N05
go

create table NhaCungCap
(
	mancc int identity(1,1) primary key,
	tenncc nvarchar(50) not null,
	sdt varchar(12),
	diachi nvarchar(50)
)
go

create table DonVi
(
	madonvi int identity(1,1) primary key,
	tendonvi nvarchar(50) not null,
)
go

create table Thuoc
(
	mathuoc int identity primary key,
	tenthuoc nvarchar(50) not null,
	mancc int not null,
	hamluong varchar(15),
	donggoi nvarchar(50) not null,
	giaban float not null,
	gianhap float not null,
	madv int not null,
	soluongton int not null
	constraint FK_NT foreign key (mancc) references NhaCungCap(mancc)on delete cascade on update cascade,
	constraint FK_DT foreign key (madv) references DonVi(madonvi)on delete cascade on update cascade
)
go

create table HoaDon
(
	mahd int identity(1,1) primary key,
	thoigian date not null,
	mathuoc int not null,
	tenthuoc nvarchar(50) not null,
	tenkh nvarchar(50) not null,
	soluongban int not null,
	donvi int not null,
	giaban float not null,
	tongtien float not null,
	constraint FK_HT foreign key (mathuoc) references Thuoc(mathuoc) on delete cascade on update cascade,
	constraint FK_HD foreign key (donvi) references DonVi(madonvi),
)
go

/* Trigger */

create trigger updateSoLuongBan on HoaDon
for delete, insert
as
	begin
	declare @slb int
	declare @slt int
	select @slb = HoaDon.soluongban from HoaDon inner join inserted on HoaDon.mathuoc = inserted.mathuoc

	select @slt = soluongton from Thuoc inner join inserted on Thuoc.mathuoc = inserted.mathuoc
	if(@slt = 0)
		begin
			raiserror(N'Thuoc trong kho da het',16,1)
			rollback transaction
		end
	else if(@slt < @slb)
	begin
		raiserror(N'Kho khong du thuoc de ban',16,1)
		rollback transaction
	end
	else
		begin
			update Thuoc set soluongton = soluongton - @slb from Thuoc, inserted where Thuoc.mathuoc = inserted.mathuoc
		end

	end
go