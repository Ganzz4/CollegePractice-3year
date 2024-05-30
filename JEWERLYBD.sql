--Создание контейнера базы данных
create database JEWERLYBD
go

--Активация контейнера базы данных
use JEWERLYBD
go


 -- Создание доменов
create type sAddress from varchar(40)
go
create type code from varchar(13)
go
create type providerJ from varchar(20)
go

-- таблица с пользователями
CREATE TABLE UserData
(
	UserLogin VARCHAR(20) PRIMARY KEY,
	UserPassword NVARCHAR(255) NOT NULL,
	UserRole VARCHAR(10) NOT NULL
)

Select * from UserData

--блок создания родительских таблиц

create table Countries(country_code smallint primary key,
country_name sAddress not NULL UNIQUE)
go
	
select * from Countries

create table StoreAddress(store_address_code smallint primary key,
store_city sAddress not NULL,
store_street sAddress not NULL,
CONSTRAINT UC_StoreAddress UNIQUE (store_city, store_street))
go

select * from StoreAddress

create table VAT(vat_code smallint primary key,
vat_procent smallint check(vat_procent>=0 and vat_procent<=100) UNIQUE)
go

select * from VAT

create table ProductType(product_type_code smallint primary key,
product_type_name varchar(15) not NULL UNIQUE)
go

select * from ProductType

create table ProductSample(sample_code smallint primary key,
sample_name smallint check (sample_name>=375 and sample_name<=999) UNIQUE)
go


select * from ProductSample

create table ProviderAddress(provider_address_code smallint primary key,
provider_city providerJ not NULL,
provider_street sAddress not NULL,
CONSTRAINT UC_ProviderAddress UNIQUE (provider_city, provider_street))
go

select * from ProviderAddress

create table ProviderName(provider_code smallint primary key,
provider_name providerJ not NULL UNIQUE)
go

select * from ProviderName

-- блок создания родительско-дочерних таблиц

create table Cheque( number_in_order smallint primary key,
date_and_time_of_sale DATETIME,
country_code smallint foreign key references Countries(country_code) NOT NULL,
store_adress_code smallint foreign key references StoreAddress(store_address_code) NOT NULL,
product_type_code smallint foreign key references ProductType(product_type_code) NOT NULL,
weight_grams real check(weight_grams>0.0) not NULL,
price_per_gram real check(price_per_gram>0.0) not NULL,
discount_percentage smallint check(discount_percentage>=0 and discount_percentage<=100),
vat_code smallint foreign key references VAT(vat_code) NOT NULL)
go

select * from Cheque

--блок создания дочерних таблиц

create table TaxInvoice(tax_invoice_number int not NULL,
delivery_date date not NULL,
provider_code smallint foreign key references ProviderName(provider_code) not NULL,
product_type_code smallint foreign key references ProductType(product_type_code) not NULL,
sample_code smallint foreign key references ProductSample(sample_code) not NULL,
quantity smallint check(quantity>0) not NULL,
weight_grams real check(weight_grams>0) not NULL,
price_per_gramm real check(price_per_gramm>0) not NULL,
vat_code smallint foreign key references VAT(vat_code) not NULL,
CONSTRAINT UC_TaxInvoice UNIQUE (tax_invoice_number, product_type_code))
go

select * from TaxInvoice

create table ProductProvider( provider_code smallint UNIQUE foreign key references ProviderName(provider_code) NOT NULL,
fiscal_code code UNIQUE,
country_code smallint foreign key references Countries(country_code) NOT NULL,
provider_address_code smallint foreign key references ProviderAddress(provider_address_code) NOT NULL,
phone_number code UNIQUE,
payment_account code UNIQUE)
go

select * from ProductProvider

-- Блок ввода информации в таблицы(заполнение)

EXEC CreateUser @userLogin = 'admin', @userPassword = 'a159753', @userRole = 'admin';
EXEC CreateUser @userLogin = 'user1', @userPassword = 'user11', @userRole = 'user';
EXEC CreateUser @userLogin = 'user2', @userPassword = 'user12', @userRole = 'user';

insert into Countries(country_code,country_name)
values (1,'Молдова'),
(2,'Украина'),
(3,'Россия'),
(4,'Италия'),
(5,'Турция'),
(6,'Испания'),
(7,'Франция'),
(8,'Румыния'),
(9,'Польша'),
(10,'Словакия'),
(11,'Великобритания'),
(12,'Чехия')
go



insert into StoreAddress(store_address_code,store_city,store_street)
values (1,'Кишинёв','Узинелор 12/6'),
(2,'Орхей','Новая 12/6'),
(3,'Кишинёв','Василе Лупу 8'),
(4,'Кишинёв','Константин Негруззи 2'),
(5,'Кайсери','Бозанти 22/4'),
(6,'Кишинёв','Штефан Чел Маре 89'),
(7,'Париж','Шанз-Элизе 1'),
(8,'Кишинёв','Измаил 26'),
(9,'Братислава','Штура 16'),
(10,'Кишинёв','Узинелор 22/9')
go


insert into VAT(vat_code,vat_procent)
values (1,10),
(2,13),
(3,17),
(4,24),
(5,11),
(6,25),
(7,50),
(8,75),
(9,90),
(10,85)
go

insert into ProductType(product_type_code,product_type_name)
values (1,'Кольцо'),
(2,'Серёжки'),
(3,'Цепочка'),
(4,'Браслет'),
(5,'Колье'),
(6,'Бусы'),
(7,'Брошь'),
(8,'Локет'),
(9,'Запонки'),
(10,'Подвеска')
go

insert into ProductSample(sample_code,sample_name)
values (1,585),
(2,750),
(3,375),
(4,500),
(5,999),
(6,950),
(7,975),
(8,860),
(9,550),
(10,400)
go



insert into ProviderAddress(provider_address_code,provider_city,provider_street)
values (1,'Кишинёв','Бульвар Штефан Чел Маре 128'),
(2,'Кишинёв','Бульвар Штефан Чел Маре 68'),
(3,'Кишинёв','Армянская 51А'),
(4,'Кишинёв','Армянская 42'),
(5,'Кишинёв','Бульвар Штефан Чел Маре 51'),
(6,'Кишинёв','Измаил 15'),
(7,'Кишинёв','Гренобле 21'),
(8,'Кишинёв','Узинелор 16'),
(9,'Кишинёв','Алексей Матеевич 59'),
(10,'Кишинёв','Узинелор 99')
go

insert into ProviderName(provider_code,provider_name)
values (1,'Paradis'),
(2,'Sapfir'),
(3,'Milenium'),
(4,'AURCENTRU'),
(5,'AURARIE'),
(6,'Harry Winston'),
(7,'Cartier'),
(8,'Chopard'),
(9,'Van Cleef & Arpels'),
(10,'Tiffany & Co')
go

insert into ProductProvider(provider_code,fiscal_code,country_code,provider_address_code,phone_number,payment_account)
values (1,'1234567891023',1,1,'022221232','5432167891023'),
(2,'1226282191123',1,2,'022369213','5422561699122'),
(3,'1234997891023',1,3,'028125264','2488187881921'),
(4,'1234122891023',1,4,'022152225','3452147841468'),
(5,'1234567541023',1,5,'022196936','1252157851555'),
(6,'4434567891023',1,6,'022911237','5552157851524'),
(7,'5634567891023',1,7,'022913238','6652157851599'),
(8,'1234567891072',1,8,'022912932','4952157851567'),
(9,'1234567816023',1,9,'022932932','9952157851555'),
(10,'4234564410244',1,10,'0229336932','1152157851524')
go

insert into TaxInvoice(tax_invoice_number,delivery_date,provider_code,product_type_code,
sample_code,quantity,weight_grams,price_per_gramm,vat_code)
values (1,'2023-01-01',1,2,1,3,2.09,650,1),
(1,'2023-01-01',1,1,1,3,2.085,650,1),
(1,'2023-01-01',1,5,1,3,2.095,650,1),
(2,'2023-03-01',2,1,2,2,4.89,900,2),
(2,'2023-03-01',2,3,2,2,4.91,950,2),
(3,'2023-03-19',3,6,3,1,8.29,700,3),
(3,'2023-03-19',3,1,4,2,5.16,600,3),
(3,'2023-03-19',3,4,5,2,22.11,900,3),
(4,'2023-05-16',4,3,6,2,59.1,900,4),
(5,'2023-07-14',5,2,7,2,44.52,1150,5)
go

insert into Cheque(number_in_order,date_and_time_of_sale,country_code,store_adress_code,
product_type_code,weight_grams,price_per_gram,discount_percentage,vat_code)
values (1,'2022-02-16T13:23:16',1,1,2,2.09,650,0,1),
(2,'2022-02-19T14:21:28',1,2,2,2.085,650,10,1),
(3,'2022-03-21T16:25:21',1,3,2,2.095,650,0,1),
(4,'2022-04-30T17:01:16',1,4,1,4.89,900,15,2),
(5,'2022-05-02T11:54:16',1,5,1,4.91,750,20,3),
(6,'2022-01-11T10:29:29',2,6,2,8.29,700,0,3),
(7,'2022-04-18T08:55:55',2,7,1,4.91,600,0,3),
(8,'2022-07-17T09:49:51',2,8,4,4.91,900,30,4),
(9,'2022-11-17T16:15:14',3,9,3,4.91,900,0,1),
(10,'2022-05-16T18:33:16',3,10,2,44.52,1150,5,2)
go


CREATE VIEW CountriesView AS
SELECT
country_name AS 'Название страны',
country_code as 'PRIVATE'
FROM Countries;


CREATE VIEW StoreAddressView AS
SELECT 
store_city AS 'Город', 
store_street AS 'Улица',
store_address_code as 'PRIVATE'
FROM StoreAddress;

CREATE VIEW VATView AS
SELECT 
vat_procent AS 'Процент НДС',
vat_code as 'PRIVATE'
FROM VAT;

CREATE VIEW ProductTypeView AS
SELECT 
product_type_name AS 'Тип продукта',
product_type_code as 'PRIVATE'
FROM ProductType;

CREATE VIEW ProductSampleView AS
SELECT 
sample_name AS 'Проба',
sample_code as 'PRIVATE'
FROM ProductSample;


CREATE VIEW ProviderAddressView AS
SELECT 
provider_city AS 'Город', provider_street AS 'Улица',
provider_address_code as 'PRIVATE'
FROM ProviderAddress;

CREATE VIEW ProviderNameView AS
SELECT 
provider_name AS 'Название поставщика',
provider_code as 'PRIVATE'
FROM ProviderName;



-- Представление для ProductProvider
CREATE VIEW ProductProviderView AS
SELECT 
	provider_name as 'Название поставщика',
    fiscal_code AS 'Фискальный код',
    country_name AS 'Страна',
    provider_city AS 'Город',
	provider_street  AS 'Улица',
    phone_number AS 'Номер телефона',
    payment_account AS 'Платежный счет'
FROM 
    ProductProviderViewReport;
	go

	-- Представление для TaxInvoice	
CREATE VIEW TaxInvoiceView AS
SELECT 
    tax_invoice_number AS 'Номер накладной',
    delivery_date AS 'Дата поставки',
    provider_name AS 'Имя поставщика',
    product_type_name AS 'Тип',
    sample_name  AS 'Проба',
    quantity AS 'Количество',
    weight_grams AS 'Вес',
    price_per_gramm AS 'Цена за грамм',
    vat_procent AS 'НДС',
	price_no_NDS AS 'Цена(Без НДС)',
	price_with_NDS AS 'Цена(С НДС)'
FROM 
    TaxInvoiceReportView;
	go
	
	SELECT * FROM TaxInvoiceView
	
	
	-- Представление для Cheque
CREATE VIEW ChequeView AS
SELECT 
    date_and_time_of_sale AS 'Дата',
    country_name AS 'Страна',
    store_city AS 'Город',
    store_street AS 'Улица',
    product_type_name AS 'Тип',
    weight_grams AS 'Вес',
    price_per_gram AS 'Цена за грамм',
    discount_percentage AS 'Процент скидки',
    vat_procent AS 'НДС',
    -- Цена без НДС и без скидки
    ROUND(price_per_gram * weight_grams, 2) AS 'Цена(Без НДС)',
    
    -- Цена со скидкой, но без НДС
    ROUND(price_per_gram * weight_grams - 
          (CASE WHEN discount_percentage = 0 THEN 0 ELSE (price_per_gram * weight_grams * discount_percentage / 100) END), 2) AS 'Цена со скидкой(Без НДС)',
    
    -- Цена с НДС, но без скидки
    ROUND(price_per_gram * weight_grams +
          (price_per_gram * weight_grams * vat_procent / 100), 2) AS 'Цена(С НДС)',
    
    -- Цена со скидкой и НДС
    ROUND((price_per_gram * weight_grams +
          (price_per_gram * weight_grams * vat_procent / 100)) - 
          (CASE WHEN discount_percentage = 0 THEN 0 ELSE (price_per_gram * weight_grams * discount_percentage / 100) END), 2) AS 'Цена со скидкой и НДС',
          
    ChequeReportView.PRIVATE AS 'PRIVATE'
FROM ChequeReportView

	SELECT * FROM ChequeView
	
	-- Блок создания индексов, для самых используемых полей
create index product_type_name_idx on ProductType(product_type_name)
go
create index sample_name_idx on ProductSample(sample_name)
go
create index provider_name_idx on ProviderName(provider_name)
go


	CREATE VIEW ProductProviderViewReport AS
SELECT 
	ProviderName.provider_name,
    ProductProvider.fiscal_code,
    Countries.country_name,
    ProviderAddress.provider_city,
	ProviderAddress.provider_street,
    ProductProvider.phone_number,
    ProductProvider.payment_account
FROM 
    ProductProvider
JOIN 
    Countries ON ProductProvider.country_code = Countries.country_code
JOIN 
    ProviderAddress ON ProductProvider.provider_address_code = ProviderAddress.provider_address_code
JOIN 
	ProviderName ON ProductProvider.provider_code = ProviderName.provider_code;
	

	select * from ProductProviderViewReport
	-- Представление для TaxInvoice


CREATE VIEW TaxInvoiceReportView AS
SELECT 
    TaxInvoice.tax_invoice_number,
    TaxInvoice.delivery_date,
    ProviderName.provider_name,
    ProductType.product_type_name,
    ProductSample.sample_name,
    TaxInvoice.quantity,
    TaxInvoice.weight_grams,
    TaxInvoice.price_per_gramm,
    VAT.vat_procent,
		    ROUND(price_per_gramm * weight_grams, 2) AS 'price_no_NDS',
    ROUND(price_per_gramm * weight_grams +
          (price_per_gramm * weight_grams * vat_procent / 100), 2) AS 'price_with_NDS'
FROM 
    TaxInvoice
JOIN 
    ProductType ON TaxInvoice.product_type_code = ProductType.product_type_code
JOIN 
    ProductSample ON TaxInvoice.sample_code = ProductSample.sample_code
JOIN 
    VAT ON TaxInvoice.vat_code = VAT.vat_code
JOIN 
    ProviderName ON TaxInvoice.provider_code = ProviderName.provider_code;

	SELECT * FROM TaxInvoiceReportView
	
	-- Представление для Cheque
CREATE VIEW ChequeReportView AS
SELECT 
    Cheque.date_and_time_of_sale,
    Countries.country_name,
    StoreAddress.store_city,
	StoreAddress.store_street,
    ProductType.product_type_name,
    Cheque.weight_grams,
    Cheque.price_per_gram,
    Cheque.discount_percentage,
    VAT.vat_procent,
	    -- Цена без НДС и без скидки
    ROUND(price_per_gram * weight_grams, 2) AS 'price_no_NDS',
    
    -- Цена со скидкой, но без НДС
    ROUND(price_per_gram * weight_grams - 
          (CASE WHEN discount_percentage = 0 THEN 0 ELSE (price_per_gram * weight_grams * discount_percentage / 100) END), 2) AS 'price_discount_no_NDS',
    
    -- Цена с НДС, но без скидки
    ROUND(price_per_gram * weight_grams +
          (price_per_gram * weight_grams * vat_procent / 100), 2) AS 'price_with_NDS',
    
    -- Цена со скидкой и НДС
    ROUND((price_per_gram * weight_grams +
          (price_per_gram * weight_grams * vat_procent / 100)) - 
          (CASE WHEN discount_percentage = 0 THEN 0 ELSE (price_per_gram * weight_grams * discount_percentage / 100) END), 2) AS 'price_discount_with_NDS',        
    Cheque.number_in_order AS 'PRIVATE'
FROM 
    Cheque
JOIN 
    Countries ON Cheque.country_code = Countries.country_code
JOIN 
    StoreAddress ON Cheque.store_adress_code = StoreAddress.store_address_code
JOIN 
    ProductType ON Cheque.product_type_code = ProductType.product_type_code
JOIN 
    VAT ON Cheque.vat_code = VAT.vat_code;

	SELECT * FROM ChequeView


	
CREATE VIEW InfoForClientView AS
SELECT 
    provider_name AS 'Фирма',
    product_type_name AS 'Тип',
    sample_name  AS 'Проба',
    weight_grams AS 'Вес',
    ROUND(price_per_gramm*weight_grams+(price_per_gramm*weight_grams * vat_procent / 100),2) AS 'Цена'
FROM 
    TaxInvoiceReportView
	go
	SELECT * FROM InfoForClientView
	

	CREATE VIEW UserDataView AS
SELECT 
	UserLogin AS 'Логин',
	UserRole AS 'Роль'
FROM 
    UserData
	go

	SELECT * FROM UserDataView



-- Запрос находит сумму всех продаж по стране магазина и позже группирует их по стране магазина:
SELECT Countries.country_name, SUM(weight_grams * price_per_gram * (100 + VAT.vat_procent)/100) AS sales
FROM Cheque
INNER JOIN Countries ON 
Cheque.country_code = Countries.country_code
INNER JOIN VAT ON 
Cheque.vat_code = VAT.vat_code
GROUP BY Countries.country_name
go


-- Транзакции и процедуры


CREATE FUNCTION dbo.GenerateCountryID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_country SMALLINT
 SET @lNid_country = (SELECT MAX(country_code) + 1
           FROM Countries)
 RETURN @lNid_country
END
GO

CREATE PROCEDURE InsertCountry
    @country_name NVARCHAR(100)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @country_code SMALLINT; 
        SELECT @country_code = dbo.GenerateCountryID();

        IF EXISTS (SELECT 1 FROM Countries WHERE country_name = @country_name)
        BEGIN
            RAISERROR('Такая страна уже существует.', 16, 1);
        END
        ELSE
        BEGIN
            INSERT INTO Countries(country_code, country_name)
            VALUES (@country_code, @country_name);
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateStoreAddressID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_storeAddress SMALLINT
 SET @lNid_storeAddress = (SELECT MAX(store_address_code) + 1
           FROM StoreAddress)
 RETURN @lNid_storeAddress
END
GO

CREATE PROCEDURE InsertStoreAddress
        @store_city sAddress,
		@store_street sAddress
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @store_address_code SMALLINT; 
        SELECT @store_address_code = dbo.GenerateCountryID();

        IF EXISTS (SELECT 1 FROM StoreAddress WHERE store_city = @store_city AND store_street = @store_street)
        BEGIN
            RAISERROR('Такой адрес магазина уже существует.', 16, 1);
        END
        ELSE
        BEGIN
                INSERT INTO StoreAddress (store_address_code,store_city, store_street)
					 VALUES (@store_address_code,@store_city, @store_street);
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateVATID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_vat SMALLINT
 SET @lNid_vat = (SELECT MAX(vat_code) + 1
           FROM VAT)
 RETURN @lNid_vat
END
GO

CREATE PROCEDURE InsertVAT
    @vat_procent smallint
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @vat_code SMALLINT; 
        SELECT @vat_code = dbo.GenerateVATID();

        IF EXISTS (SELECT 1 FROM VAT WHERE vat_procent = @vat_procent)
        BEGIN
            RAISERROR('Такой НДС уже существует.', 16, 1);
        END
        ELSE
        BEGIN
                INSERT INTO VAT (vat_code,vat_procent)
    VALUES (@vat_code ,@vat_procent)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateProductTypeID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_product_type SMALLINT
 SET @lNid_product_type = (SELECT MAX(product_type_code) + 1
           FROM ProductType)
 RETURN @lNid_product_type
END
GO

CREATE PROCEDURE InsertProductType
    @product_type_name varchar(15)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @product_type_code SMALLINT; 
        SELECT @product_type_code = dbo.GenerateProductTypeID();

        IF EXISTS (SELECT 1 FROM ProductType WHERE product_type_name = @product_type_name)
        BEGIN
            RAISERROR('Такой тип продукта уже существует.', 16, 1);
        END
        ELSE
        BEGIN
        INSERT INTO ProductType (product_type_code,product_type_name)
    VALUES (@product_type_code,@product_type_name)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateProductSampleID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_product_sample SMALLINT
 SET @lNid_product_sample = (SELECT MAX(sample_code) + 1
           FROM ProductSample)
 RETURN @lNid_product_sample
END
GO

CREATE PROCEDURE InsertProductSample
    @sample_name smallint
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @sample_code SMALLINT; 
        SELECT @sample_code = dbo.GenerateProductSampleID();

        IF EXISTS (SELECT 1 FROM ProductSample WHERE sample_name = @sample_name)
        BEGIN
            RAISERROR('Такая проба уже существует.', 16, 1);
        END
        ELSE
        BEGIN
        INSERT INTO ProductSample (sample_code,sample_name)
    VALUES (@sample_code,@sample_name)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateProviderAddressID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_providerAddress SMALLINT
 SET @lNid_providerAddress = (SELECT MAX(provider_address_code) + 1
           FROM ProviderAddress)
 RETURN @lNid_providerAddress
END
GO

CREATE PROCEDURE InsertProviderAddress
    @provider_city sAddress,
    @provider_street sAddress
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @provider_address_code SMALLINT; 
        SELECT @provider_address_code = dbo.GenerateProviderAddressID();

        IF EXISTS (SELECT 1 FROM ProviderAddress WHERE provider_city = @provider_city AND provider_street = @provider_street)
        BEGIN
            RAISERROR('Такой адрес поставщика уже существует.', 16, 1);
        END
        ELSE
        BEGIN
                    INSERT INTO ProviderAddress (provider_address_code,provider_city, provider_street)
				VALUES (@provider_address_code,@provider_city, @provider_street)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;




CREATE FUNCTION dbo.GenerateProviderNameID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_provider SMALLINT
 SET @lNid_provider = (SELECT MAX(provider_code) + 1
           FROM ProviderName)
 RETURN @lNid_provider
END
GO

CREATE PROCEDURE InsertProviderName
    @provider_name providerJ
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @provider_code SMALLINT; 
        SELECT @provider_code = dbo.GenerateProviderNameID();

        IF EXISTS (SELECT 1 FROM ProviderName WHERE provider_name = @provider_name)
        BEGIN
            RAISERROR('Такое имя поставщика уже существует.', 16, 1);
        END
        ELSE
        BEGIN
        INSERT INTO ProviderName (provider_code,provider_name)
    VALUES (@provider_code,@provider_name)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE FUNCTION dbo.GenerateChequeID()
RETURNS SMALLINT
AS
BEGIN
 DECLARE @lNid_cheque SMALLINT
 SET @lNid_cheque = (SELECT MAX(number_in_order) + 1
           FROM Cheque)
 RETURN @lNid_cheque
END
GO

CREATE PROCEDURE InsertCheque
        @date_and_time_of_sale DATETIME,
    @store_country_code smallint,
    @store_adress_code smallint,
    @product_type_code smallint,
    @weight_grams real,
    @price_per_gram real,
    @discount_percentage smallint,
    @vat_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @number_in_order SMALLINT; 
        SELECT @number_in_order = dbo.GenerateChequeID();

        BEGIN

		 INSERT INTO Cheque (number_in_order,date_and_time_of_sale, country_code, store_adress_code, product_type_code, weight_grams, price_per_gram, discount_percentage, vat_code)
    VALUES (@number_in_order,@date_and_time_of_sale, @store_country_code, @store_adress_code, @product_type_code, @weight_grams, @price_per_gram, @discount_percentage, @vat_code)
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO

CREATE PROCEDURE InsertTaxInvoice
    @tax_invoice_number int,
    @delivery_date date,
    @provider_code smallint,
    @product_type_code smallint,
    @sample_code smallint,
    @quantity smallint,
    @weight_grams real,
    @price_per_gramm real,
    @vat_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
	        IF EXISTS (SELECT 1 FROM TaxInvoice WHERE tax_invoice_number = @tax_invoice_number AND product_type_code = @product_type_code)
        BEGIN
            RAISERROR('В этой накладной уже есть этот тип продукта.', 16, 1);
        END
        ELSE
		BEGIN
    INSERT INTO TaxInvoice (tax_invoice_number, delivery_date, provider_code, product_type_code, sample_code, quantity, weight_grams, price_per_gramm, vat_code)
    VALUES (@tax_invoice_number, @delivery_date, @provider_code, @product_type_code, @sample_code, @quantity, @weight_grams, @price_per_gramm, @vat_code)
					COMMIT TRANSACTION;
	END	
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE InsertProductProvider
    @provider_code smallint,
    @fiscal_code code,
    @provider_country_code smallint,
    @provider_address_code smallint,
    @phone_number code,
    @payment_account code
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
		IF EXISTS (SELECT 1 FROM ProductProvider WHERE fiscal_code = @fiscal_code)
        BEGIN
            RAISERROR('Такой фискальный код уже существует.', 16, 1);
        END
		ELSE IF EXISTS (SELECT 1 FROM ProductProvider WHERE provider_code = @provider_code)
        BEGIN
            RAISERROR('Такой поставщик уже существует.', 16, 1);
        END
		ELSE IF EXISTS (SELECT 1 FROM ProductProvider WHERE phone_number = @phone_number)
        BEGIN
            RAISERROR('Такой номер телефона уже существует.', 16, 1);
        END
		ELSE IF EXISTS (SELECT 1 FROM ProductProvider WHERE payment_account = @payment_account)
        BEGIN
            RAISERROR('Такой платёжный счёт уже существует.', 16, 1);
        END

        ELSE
		BEGIN
    INSERT INTO ProductProvider (provider_code, fiscal_code, country_code, provider_address_code, phone_number, payment_account)
    VALUES (@provider_code, @fiscal_code, @provider_country_code, @provider_address_code, @phone_number, @payment_account)
	END	
	COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO
-- Удаление


CREATE PROCEDURE DeleteCountry
    @country_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
	                IF EXISTS (SELECT 1 FROM Cheque WHERE country_code = @country_code) OR
           EXISTS (SELECT 1 FROM ProductProvider WHERE country_code = @country_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM Countries
        WHERE country_code = @country_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE DeleteStoreAddress
    @store_address_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
		        IF EXISTS (SELECT 1 FROM Cheque WHERE store_adress_code = @store_address_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM StoreAddress
        WHERE store_address_code = @store_address_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE DeleteVAT
    @vat_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
		        IF EXISTS (SELECT 1 FROM Cheque WHERE vat_code = @vat_code)OR
           EXISTS (SELECT 1 FROM TaxInvoice WHERE vat_code = @vat_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM VAT
        WHERE vat_code = @vat_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


	CREATE PROCEDURE DeleteProductType
    @product_type_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

		   IF EXISTS (SELECT 1 FROM Cheque WHERE product_type_code = @product_type_code)OR
           EXISTS (SELECT 1 FROM TaxInvoice WHERE product_type_code = @product_type_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END
        DELETE FROM ProductType
        WHERE product_type_code = @product_type_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

	CREATE PROCEDURE DeleteProductSample
    @sample_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
			        IF EXISTS (SELECT 1 FROM TaxInvoice WHERE sample_code = @sample_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM ProductSample
        WHERE sample_code = @sample_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE DeleteProviderAddress
    @provider_address_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
			        IF EXISTS (SELECT 1 FROM ProductProvider WHERE provider_address_code = @provider_address_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM ProviderAddress
        WHERE provider_address_code = @provider_address_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE DeleteProviderName
    @provider_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	
			 IF EXISTS (SELECT 1 FROM ProductProvider WHERE provider_code = @provider_code)OR
           EXISTS (SELECT 1 FROM TaxInvoice WHERE provider_code = @provider_code)
        BEGIN
            RAISERROR('Удаление невозможно! Поле используется в других таблицах.', 16, 1);
        END

        DELETE FROM ProviderName
        WHERE provider_code = @provider_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE DeleteTaxInvoice
    @tax_invoice_number int,
	@product_type_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DELETE FROM TaxInvoice
        WHERE tax_invoice_number = @tax_invoice_number AND
		product_type_code = @product_type_code
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE DeleteProductProvider
    @fiscal_code code
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DELETE FROM ProductProvider
        WHERE fiscal_code = @fiscal_code;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE DeleteCheque
    @number_in_order smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DELETE FROM Cheque
        WHERE number_in_order = @number_in_order;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


-- Обновление

CREATE PROCEDURE UpdateCountry
    @country_code smallint,
    @country_name sAddress
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM Countries WHERE country_name = @country_name)
        BEGIN
            RAISERROR('Такая страна уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE Countries
        SET country_name = @country_name
        WHERE country_code = @country_code;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE UpdateStoreAddress
    @store_address_code smallint,
    @store_city sAddress,
    @store_street sAddress
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
	        IF EXISTS (SELECT 1 FROM StoreAddress WHERE store_city = @store_city AND store_street = @store_street)
        BEGIN
            RAISERROR('Такой адрес магазина уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE StoreAddress
        SET store_city = @store_city,
            store_street = @store_street
        WHERE store_address_code = @store_address_code;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateVAT
    @vat_code smallint,
    @vat_procent smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM VAT WHERE vat_procent = @vat_procent)
        BEGIN
            RAISERROR('Такой НДС уже существует.', 16, 1);
        END
        ELSE
        BEGIN
        UPDATE VAT
        SET vat_procent = @vat_procent
        WHERE vat_code = @vat_code;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateProductType
    @product_type_code smallint,
    @product_type_name varchar(15)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM ProductType WHERE product_type_name = @product_type_name)
        BEGIN
            RAISERROR('Такой тип продукта уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE ProductType
        SET product_type_name = @product_type_name
        WHERE product_type_code = @product_type_code;
        

		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateProductSample
    @sample_code smallint,
    @sample_name smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM ProductSample WHERE sample_name = @sample_name)
        BEGIN
            RAISERROR('Такая проба уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE ProductSample
        SET sample_name = @sample_name
        WHERE sample_code = @sample_code;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateProviderAddress
    @provider_address_code smallint,
    @provider_city providerJ,
    @provider_street sAddress
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM ProviderAddress WHERE provider_city = @provider_city AND provider_street = @provider_street)
        BEGIN
            RAISERROR('Такой адрес поставщика уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE ProviderAddress
        SET provider_city = @provider_city,
            provider_street = @provider_street
        WHERE provider_address_code = @provider_address_code;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateProviderName
    @provider_code smallint,
    @provider_name providerJ
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

	        IF EXISTS (SELECT 1 FROM ProviderName WHERE provider_name = @provider_name)
        BEGIN
            RAISERROR('Такое имя поставщика уже существует.', 16, 1);
        END
        ELSE
        BEGIN

        UPDATE ProviderName
        SET provider_name = @provider_name
        WHERE provider_code = @provider_code;
        
		END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE UpdateTaxInvoice
    @tax_invoice_numberOLD int,
	@tax_invoice_number int,
    @delivery_date date,
    @provider_code smallint,
    @product_type_codeOLD smallint,
	@product_type_code smallint,
    @sample_code smallint,
    @quantity smallint,
    @weight_grams real,
    @price_per_gramm real,
    @vat_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

		        IF EXISTS (SELECT 1 FROM TaxInvoice WHERE tax_invoice_number = @tax_invoice_number AND product_type_code = @product_type_code)
        BEGIN
            RAISERROR('В этой накладной уже есть этот тип продукта.', 16, 1);
        END
        ELSE
		BEGIN

        UPDATE TaxInvoice
        SET 
			tax_invoice_number = @tax_invoice_number,
			delivery_date = @delivery_date,
            provider_code = @provider_code,
            product_type_code = @product_type_code,
            sample_code = @sample_code,
            quantity = @quantity,
            weight_grams = @weight_grams,
            price_per_gramm = @price_per_gramm,
            vat_code = @vat_code
        WHERE tax_invoice_number = @tax_invoice_numberOLD AND
		product_type_code = @product_type_codeOLD

		END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

CREATE PROCEDURE UpdateProductProvider
    @provider_code smallint,
    @fiscal_code code,
	@fiscal_codeOLD code,
    @country_code smallint,
    @provider_address_code smallint,
    @phone_number code,
    @payment_account code
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
			IF EXISTS (SELECT 1 FROM ProductProvider WHERE fiscal_code = @fiscal_code)
        BEGIN
            RAISERROR('Такой фискальный код уже существует.', 16, 1);
        END
		ELSE IF EXISTS (SELECT 1 FROM ProductProvider WHERE phone_number = @phone_number)
        BEGIN
            RAISERROR('Такой номер телефона уже существует.', 16, 1);
        END
		ELSE IF EXISTS (SELECT 1 FROM ProductProvider WHERE payment_account = @payment_account)
        BEGIN
            RAISERROR('Такой платёжный счёт уже существует.', 16, 1);
        END

        ELSE
		BEGIN

        UPDATE ProductProvider
        SET 
			fiscal_code = @fiscal_code,
			provider_code = @provider_code,
            country_code = @country_code,
            provider_address_code = @provider_address_code,
            phone_number = @phone_number,
            payment_account = @payment_account
        WHERE fiscal_code = @fiscal_codeOLD;
        
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE UpdateCheque
    @number_in_order smallint,
    @date_and_time_of_sale DATETIME,
    @country_code smallint,
    @store_adress_code smallint,
    @product_type_code smallint,
    @weight_grams real,
    @price_per_gram real,
    @discount_percentage smallint,
    @vat_code smallint
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY

        UPDATE Cheque
        SET date_and_time_of_sale = @date_and_time_of_sale,
            country_code = @country_code,
            store_adress_code = @store_adress_code,
            product_type_code = @product_type_code,
            weight_grams = @weight_grams,
            price_per_gram = @price_per_gram,
            discount_percentage = @discount_percentage,
            vat_code = @vat_code
        WHERE number_in_order = @number_in_order;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO




CREATE PROCEDURE UpdateUserRole
    @userLogin VARCHAR(20),
    @userRole VARCHAR(10)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
					IF NOT EXISTS (SELECT 1 FROM UserData WHERE UserLogin = @userLogin)
        BEGIN
            RAISERROR('Пользователя с таким логином не существует!', 16, 1);
        END

        UPDATE UserData
        SET UserRole = @userRole
        WHERE UserLogin = @userLogin;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO


CREATE PROCEDURE DeleteUser
    @userLogin VARCHAR(20)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM UserData WHERE UserLogin = @userLogin)
        BEGIN
            RAISERROR('Пользователя с таким логином не существует!', 16, 1);
        END

		DELETE FROM UserData 
		WHERE UserLogin = @userLogin;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO




CREATE PROCEDURE CreateUser
        @userLogin VARCHAR(20),
		@userPassword NVARCHAR(255),
		@userRole VARCHAR(10)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

          DECLARE @password_hash NVARCHAR(255)
			SET @password_hash = HASHBYTES('SHA2_256', @userPassword)

        IF EXISTS (SELECT 1 FROM UserData WHERE userLogin = @userLogin)
        BEGIN
            RAISERROR('Пользователь с таким логином уже существует!', 16, 1);
        END
        ELSE
        BEGIN
                INSERT INTO UserData (userLogin,userPassword, userRole)
					 VALUES (@userLogin,@password_hash, @userRole);
        END
        
        COMMIT TRANSACTION;
    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END;
GO


CREATE FUNCTION dbo.ValidateUser
(
    @login VARCHAR(20),
    @password NVARCHAR(255)
)
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @role VARCHAR(10)

    SELECT @role = userRole
    FROM UserData 
    WHERE userLogin = @login
    AND userPassword = HASHBYTES('SHA2_256', @password)


    RETURN @role
END;
go

CREATE PROCEDURE RestoreDatabase
    @FilePath NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY

        -- Переводим базу данных в однопользовательский режим с немедленным откатом транзакций
        ALTER DATABASE JEWERLYBD SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        
        -- Восстанавливаем базу данных из резервной копии

        RESTORE DATABASE JEWERLYBD 
        FROM DISK = @FilePath 
        WITH REPLACE, FILE = 1, NOUNLOAD, STATS = 5;
        
        -- Переводим базу данных обратно в многопользовательский режим
        ALTER DATABASE JEWERLYBD SET MULTI_USER;

    END TRY
BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK
		END

		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = ERROR_MESSAGE(),
			   @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE()

			    ALTER DATABASE JEWERLYBD SET MULTI_USER;

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
