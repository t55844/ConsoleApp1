/*
--------------------------------------------------------------------
© 2017 sqlservertutorial.net All Rights Reserved
--------------------------------------------------------------------
Name   : BikeStores
Link   : http://www.sqlservertutorial.net/load-sample-database/
Version: 1.0
--------------------------------------------------------------------
*/
USE BikeStores
-- create tables
CREATE TABLE CATEGORIES (
	category_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	category_name VARCHAR (255) NOT NULL
) ;

CREATE TABLE BRANDS (
	brand_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	brand_name VARCHAR (255) NOT NULL
) ;

CREATE TABLE PRODUCTS (
	product_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	product_name VARCHAR (255) NOT NULL,
	brand_id INT NOT NULL,
	category_id INT NOT NULL,
	model_year SMALLINT NOT NULL,
	list_price DECIMAL (10, 2) NOT NULL,
	FOREIGN KEY (category_id) REFERENCES CATEGORIES (category_id)  ,
	FOREIGN KEY (brand_id) REFERENCES BRANDS (brand_id)  
) ;

CREATE TABLE CUSTOMERS (
	customer_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	phone VARCHAR (25),
	email VARCHAR (255) NOT NULL,
	street VARCHAR (255),
	city VARCHAR (50),
	state VARCHAR (25),
	zip_code VARCHAR (5)
) ;

CREATE TABLE STORES (
	store_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	store_name VARCHAR (255) NOT NULL,
	phone VARCHAR (25),
	email VARCHAR (255),
	street VARCHAR (255),
	city VARCHAR (255),
	state VARCHAR (10),
	zip_code VARCHAR (5)
) ;

CREATE TABLE STAFFS (
	staff_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	first_name VARCHAR (50) NOT NULL,
	last_name VARCHAR (50) NOT NULL,
	email VARCHAR (255) NOT NULL UNIQUE,
	phone VARCHAR (25),
	active tinyint NOT NULL,
	store_id INT NOT NULL,
	manager_id INT,
	FOREIGN KEY (store_id) REFERENCES STORES (store_id)  ,
	FOREIGN KEY (manager_id) REFERENCES STAFFS (staff_id) ON DELETE NO ACTION ON UPDATE NO ACTION
) ;

CREATE TABLE ORDERS (
	order_id INT IDENTITY (1, 1) PRIMARY KEY NONCLUSTERED,
	customer_id INT,
	order_status VARCHAR(2) NOT NULL,
	-- Order status: PE = Pending; PR = Processing; RE = Rejected; CO = Completed
	order_date DATE NOT NULL,
	required_date DATE NOT NULL,
	shipped_date DATE,
	store_id INT NOT NULL,
	staff_id INT NOT NULL,
	FOREIGN KEY (customer_id) REFERENCES CUSTOMERS (customer_id)  ,
	FOREIGN KEY (store_id) REFERENCES STORES (store_id)  ,
	FOREIGN KEY (staff_id) REFERENCES STAFFS (staff_id) ON DELETE NO ACTION ON UPDATE NO ACTION
) ;

CREATE TABLE ORDER_ITEMS (
	order_id INT,
	item_id INT,
	product_id INT NOT NULL,
	quantity INT NOT NULL,
	list_price DECIMAL (10, 2) NOT NULL,
	discount DECIMAL (4, 2) NOT NULL DEFAULT 0,
	PRIMARY KEY NONCLUSTERED (order_id, item_id),
	FOREIGN KEY (order_id) REFERENCES ORDERS (order_id)  ,
	FOREIGN KEY (product_id) REFERENCES PRODUCTS (product_id)  
) ;

CREATE TABLE STOCKS (
	store_id INT,
	product_id INT,
	quantity INT,
	PRIMARY KEY NONCLUSTERED (store_id, product_id),
	FOREIGN KEY (store_id) REFERENCES STORES (store_id)  ,
	FOREIGN KEY (product_id) REFERENCES PRODUCTS (product_id)  
) ;
