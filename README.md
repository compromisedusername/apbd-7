# apbd-7 API application
## Other DBs
For working on your local machine change property `ConnectionStrings` in `appsettings.json`. 
See more on: https://www.connectionstrings.com/

## Using DB in Docker 
1. Install Docker
3. Pull Image
```bash
docker pull mcr.microsoft.com/mssql/server:latest
```
3. Start container
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Adminxyz22#' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest -n sql1
```
4. Run mssqli CLI
```bash
sudo docker exec -it sql1 "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Adminxyz22#"
```
5.Create database in mssql CLI:
```bash
CREATE DATABASE apbd
GO

```
6. Create table in mssqli CLI:
```bash
USE apbd
GO
REATE TABLE [Order] (IdOrder int  NOT NULL IDENTITY,IdProduct int  NOT NULL,Amount int  NOT NULL,CreatedAt datetime  NOT NULL,FulfilledAt datetime  NULL,CONSTRAINT Order_pk PRIMARY KEY  (IdOrder));

CREATE TABLE Product (IdProduct int  NOT NULL IDENTITY,Name nvarchar(200)  NOT NULL,Description nvarchar(200)  NOT NULL,Price numeric(25,2)  NOT NULL,CONSTRAINT Product_pk PRIMARY KEY  (IdProduct));

CREATE TABLE Product_Warehouse (IdProductWarehouse int  NOT NULL IDENTITY,IdWarehouse int  NOT NULL,IdProduct int  NOT NULL,IdOrder int  NOT NULL,Amount int  NOT NULL,Price numeric(25,2)  NOT NULL,CreatedAt datetime  NOT NULL,CONSTRAINT Product_Warehouse_pk PRIMARY KEY  (IdProductWarehouse));

CREATE TABLE Warehouse (IdWarehouse int  NOT NULL IDENTITY,Name nvarchar(200)  NOT NULL,Address nvarchar(200)  NOT NULL,CONSTRAINT Warehouse_pk PRIMARY KEY  (IdWarehouse));

ALTER TABLE Product_Warehouse ADD CONSTRAINT Product_Warehouse_Order FOREIGN KEY (IdOrder)REFERENCES [Order] (IdOrder);

ALTER TABLE [Order] ADD CONSTRAINT Receipt_Product FOREIGN KEY (IdProduct) REFERENCES Product (IdProduct);

ALTER TABLE Product_Warehouse ADD CONSTRAINT _Product FOREIGN KEY (IdProduct) REFERENCES Product (IdProduct);

ALTER TABLE Product_Warehouse ADD CONSTRAINT _Warehouse FOREIGN KEY (IdWarehouse) REFERENCES Warehouse (IdWarehouse);

GO
```
7. Fetch your own data
  in mssqli CLI:
```bash
INSERT INTO Warehouse(Name, Address)VALUES('ExampleN', 'ExampleA');

GO

INSERT INTO Product(Name, Description, Price)VALUES ('ExampleN', 'None', 1),('ExampleN2', 'None', 2),('ExampleN3', 'None', 3);

GO

INSERT INTO [Order] (IdProduct, Amount, CreatedAt)VALUES((SELECT IdProduct FROM Product WHERE Name='Example'), 1, GETDATE());
GO
```
8. Run API application



