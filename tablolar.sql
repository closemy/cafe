create database cafe
use cafe
create table signup(
kullaniciadi varchar(30),
mail varchar(100),
sifre varchar(35)
)
insert into signup values('closer','iicloser49@hotmail.com','05373174081')
select * from signup

create table urunler(
id INT PRIMARY KEY IDENTITY(1,1),
urunadi varchar(50),
fiyat int
)
select * from urunler

create table odenecekler(
masaadi varchar(25),
urunadi varchar(50),
adet int,
topfiyat int
)
select * from odenecekler
update urunler set fiyat=45 where id=2
select sum(topfiyat) from odenecekler