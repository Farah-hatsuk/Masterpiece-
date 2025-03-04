create database FreeSweet;

CREATE TABLE Users
(
  Id INT PRIMARY KEY identity ,
  UNIQUE (Email),
  Password VARCHAR(100) NOT NULL,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  Email VARCHAR(50) NOT NULL,
  Role VARCHAR(50) NOT NULL,
);


CREATE TABLE Category_
(
  Id INT PRIMARY KEY identity ,
  Name VARCHAR(100) NOT NULL,
  
);

CREATE TABLE Secial_Order
(
);

CREATE TABLE Payment
(
  Id INT PRIMARY KEY identity ,
  Total INT NOT NULL,
  UsersId INT NOT NULL,
  FOREIGN KEY (Id) REFERENCES Users(Id)
);

CREATE TABLE Contect
(
  Id INT PRIMARY KEY identity ,
  Name VARCHAR(100) NOT NULL,
  Email VARCHAR(100) NOT NULL,
  Msg VARCHAR(500) NOT NULL,
  UNIQUE (Email)
);

CREATE TABLE Recipes
(
  Id INT PRIMARY KEY identity ,
  Name VARCHAR(200) NOT NULL,
  Description VARCHAR(500) NOT NULL,
  PrepTime	 INT NOT NULL,
  CookTime INT NOT NULL,
  TotalTime INT NOT NULL,
  Ingredient VARCHAR(500) NOT NULL,
  Instructions VARCHAR(500) NOT NULL,
  Notes VARCHAR(500) ,
  Img1 VARCHAR(300) ,
  Img2 VARCHAR(300) ,
  Img3 VARCHAR(300) ,
  Img4 VARCHAR(300) ,
  Img5 VARCHAR(300) ,
 
);

CREATE TABLE Product
(
  Id INT PRIMARY KEY identity ,
  Name VARCHAR(100) NOT NULL,
  Price INT NOT NULL,
  Size VARCHAR(50) NOT NULL,
  Description VARCHAR(250) NOT NULL,
  Type VARCHAR(100) NOT NULL,
  CategoryId int ,
  FOREIGN KEY (Id) REFERENCES Category_(Id)
);

CREATE TABLE Cart
(
  Id INT PRIMARY KEY identity ,
  Quantity INT NOT NULL,
  Total INT NOT NULL,
  UsersId INT NOT NULL,
  ProdectId INT NOT NULL,
  FOREIGN KEY (Id) REFERENCES Users(Id),
  FOREIGN KEY (Id) REFERENCES Product(Id)
);

CREATE TABLE Hisory
(
  Id INT PRIMARY KEY identity ,
  Quantity INT NOT NULL,
  Total INT NOT NULL,
  UsersId INT NOT NULL,
  ProdectId INT NOT NULL,
  
  FOREIGN KEY (Id) REFERENCES Users(Id),
  FOREIGN KEY (Id) REFERENCES Product(Id)
);