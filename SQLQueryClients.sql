CREATE TABLE clients(
 id INT NOT NULL PRIMARY KEY IDENTITY,
 name VARCHAR (100) NOT NULL,
 email VARCHAR (150) NOT NULL UNIQUE,
 phone VARCHAR (20) NULL,
 password VARCHAR(100) NOT NULL,
 created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP

);
INSERT INTO clients (name, email, phone, password)
VALUES
('Bill Gates', 'billgates@gmail.com', '+123456789', 'billgates123')