----drop schema if exists
--IF OBJECT_ID('Products_Orders', 'U') IS NOT NULL DROP TABLE Products_Orders; 
--IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products; 
--IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders; 
--IF OBJECT_ID('Customers', 'U') IS NOT NULL DROP TABLE Customers; 
BEGIN TRANSACTION
--declare variables
DECLARE @variable INT = 12

--build schema
CREATE TABLE Products
(
  Id INT IDENTITY(1,1),
  Name NVARCHAR(50) NOT NULL,
  ListPrice DECIMAL(18,2) NOT NULL

  CONSTRAINT PK_Products PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE Customers
(
  Id INT IDENTITY(1,1),
  Name NVARCHAR(50) NOT NULL

  CONSTRAINT PK_Customers PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE Orders
(
  Id INT IDENTITY(1,1),
  CustomerId INT NOT NULL,
  TimeStamp DATETIME DEFAULT GETDATE()

  CONSTRAINT PK_Orders PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)

CREATE TABLE Products_Orders
(
  ProductId INT,
  OrderId INT,
  Quantity INT NOT NULL,
  SalePrice DECIMAL(18,2) NOT NULL,
  LineTotal AS (SalePrice * Quantity)

  CONSTRAINT PK_Products_Orders PRIMARY KEY CLUSTERED (ProductId, OrderId),
  CONSTRAINT FK_PO_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
  CONSTRAINT FK_PO_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id)
)

--insert data
INSERT INTO Customers(Name) VALUES ('Dave'), ('Jeff'), ('Mike')
INSERT INTO Products(Name, ListPrice) VALUES ('Coke', 1.25), ('Bread', 4.75), ('Underwear', 3.45), ('Perrier', 3.00)
INSERT INTO Orders(CustomerId) VALUES (1), (2)
INSERT INTO Products_Orders(ProductId, OrderId, Quantity, SalePrice) VALUES (1, 1, 2, 1.35), (2, 1, 1, 4.00), (3, 1, 10, 3.75)
INSERT INTO Products_Orders(ProductId, OrderId, Quantity, SalePrice) VALUES (1, 2, 5, 1.20), (3, 2, 2, 5.00)

--query
SELECT Products.Id ProductId, Products.Name, PO.Quantity, Products.ListPrice, PO.SalePrice, PO.LineTotal, Orders.Id 'OrderId', Orders.TimeStamp, Customers.Name FROM Products
INNER JOIN Products_Orders PO
ON Products.Id = PO.ProductId
INNER JOIN Orders
ON PO.OrderId = Orders.Id
INNER JOIN Customers
ON Orders.CustomerId = Customers.Id
ORDER BY Orders.Id

ROLLBACK TRANSACTION