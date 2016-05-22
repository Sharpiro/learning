USE AdventureWorks;

--SELECT line subquery
--SELECT Name, ListPrice, (SELECT AVG(ListPrice) AS Average FROM Production.Product)
--FROM Production.Product

--FROM Line subquery
--;WITH subquery AS
--(
--	SELECT ProductSubcategoryID, AVG(Product.ListPrice) AS Avg
--	FROM Production.Product
--	GROUP BY ProductSubcategoryID
--)
--SELECT p.Name, p.ListPrice, subquery.Avg, p.ListPrice - subquery.Avg AS Diff
--FROM Production.Product p
--INNER JOIN subquery ON p.ProductSubcategoryID = subquery.ProductSubcategoryID

--WHERE Line subquery
--SELECT FirstName, LastName, EmailAddress
--FROM Person.Contact p
--WHERE p.ContactID IN (SELECT soh.ContactID FROM Sales.SalesOrderHeader soh)

--EXISTS
--SELECT FirstName, LastName, EmailAddress
--FROM Person.Contact p
--WHERE EXISTS (SELECT soh.ContactID FROM Sales.SalesOrderHeader soh WHERE soh.ContactID = p.ContactID)

--Any/All
SELECT Name
FROM Production.Product
WHERE ListPrice >= ANY
    (SELECT MAX (ListPrice)
     FROM Production.Product
     GROUP BY ProductSubcategoryID) ;

--UNIONS
--SELECT FirstName + ' ' + LastName AS 'FullName', EmailAddress
--FROM Person.Contact p
--UNION ALL
--SELECT ReviewerName, EmailAddress
--FROM Production.ProductReview