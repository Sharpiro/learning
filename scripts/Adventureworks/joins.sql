BEGIN TRANSACTION

CREATE TABLE Customers
(
	Id INT IDENTITY(1,1),
	Name NVARCHAR(50)

	CONSTRAINT PK_Customers PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE Orders
(
	Id INT IDENTITY(1,1),
	CustomerId INT,
	OrderDate DATETIME

	CONSTRAINT PK_Orders PRIMARY KEY CLUSTERED (Id)
	CONSTRAINT FK_Customers FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
)

INSERT INTO Customers (Name) VALUES ('David'), ('Jack'), ('Jake'), ('John'), ('Greg'), ('Bart')
INSERT INTO Orders (CustomerId) VALUES (1), (2), (2), (5), (5), (1), (NULL)

SELECT Orders.Id AS 'OrderId'
	, Customers.Id AS'CustomerId'
	, Customers.Name AS 'CustomerName'
FROM Orders FULL JOIN Customers ON Orders.CustomerId = Customers.Id
ORDER BY OrderId

SELECT Orders.Id 'OrderId', Customers.Id 'CustomerId', Customers.Name 'CustomerName'
FROM Customers FULL JOIN Orders ON Customers.Id = Orders.CustomerId
ORDER BY OrderId

ROLLBACK TRANSACTION