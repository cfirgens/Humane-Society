CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));

INSERT INTO USStates VALUES('Alabama','AL');
INSERT INTO USStates VALUES('Alaska','AK');
INSERT INTO USStates VALUES('Arizona','AZ');
INSERT INTO USStates VALUES('Arkansas','AR');
INSERT INTO USStates VALUES('California','CA');
INSERT INTO USStates VALUES('Colorado','CO');
INSERT INTO USStates VALUES('Connecticut','CT');
INSERT INTO USStates VALUES('Delaware','DE');
INSERT INTO USStates VALUES('Florida','FL');
INSERT INTO USStates VALUES('Georgia','GA');
INSERT INTO USStates VALUES('Hawaii','HI');
INSERT INTO USStates VALUES('Idaho','ID');
INSERT INTO USStates VALUES('Illinois','IL');
INSERT INTO USStates VALUES('Indiana','IN');
INSERT INTO USStates VALUES('Iowa','IA');
INSERT INTO USStates VALUES('Kansas','KS');
INSERT INTO USStates VALUES('Kentucky','KY');
INSERT INTO USStates VALUES('Louisiana','LA');
INSERT INTO USStates VALUES('Maine','ME');
INSERT INTO USStates VALUES('Maryland','MD');
INSERT INTO USStates VALUES('Massachusetts','MA');
INSERT INTO USStates VALUES('Michigan','MI');
INSERT INTO USStates VALUES('Minnesota','MN');
INSERT INTO USStates VALUES('Mississippi','MS');
INSERT INTO USStates VALUES('Missouri','MO');
INSERT INTO USStates VALUES('Montana','MT');
INSERT INTO USStates VALUES('Nebraska','NE');
INSERT INTO USStates VALUES('Nevada','NV');
INSERT INTO USStates VALUES('New Hampshire','NH');
INSERT INTO USStates VALUES('New Jersey','NJ');
INSERT INTO USStates VALUES('New Mexico','NM');
INSERT INTO USStates VALUES('New York','NY');
INSERT INTO USStates VALUES('North Carolina','NC');
INSERT INTO USStates VALUES('North Dakota','ND');
INSERT INTO USStates VALUES('Ohio','OH');
INSERT INTO USStates VALUES('Oklahoma','OK');
INSERT INTO USStates VALUES('Oregon','OR');
INSERT INTO USStates VALUES('Pennsylvania','PA');
INSERT INTO USStates VALUES('Rhode Island','RI');
INSERT INTO USStates VALUES('South Carolina','SC');
INSERT INTO USStates VALUES('South Dakota','SD');
INSERT INTO USStates VALUES('Tennessee','TN');
INSERT INTO USStates VALUES('Texas','TX');
INSERT INTO USStates VALUES('Utah','UT');
INSERT INTO USStates VALUES('Vermont','VT');
INSERT INTO USStates VALUES('Virginia','VA');
INSERT INTO USStates VALUES('Washington','WA');
INSERT INTO USStates VALUES('West Virgina','WV');
INSERT INTO USStates VALUES('Wisconsin','WI');
INSERT INTO USStates VALUES('Wyoming','WY');

SELECT * FROM USStates;

INSERT INTO DietPlans VALUES('Large dog', 'dry', 4);
INSERT INTO DietPlans VALUES('Medium dog', 'dry', 2);
INSERT INTO DietPlans VALUES('Small dog', 'wet', 1);
INSERT INTO DietPlans VALUES('Large cat', 'dry', 2);
INSERT INTO DietPlans VALUES('Small cat', 'dry', 1);
INSERT INTO DietPlans VALUES('Indoor rabbit', 'pellets', 1);
INSERT INTO DietPlans VALUES('Outdoor rabbit', 'hay', 4);

SELECT * FROM DietPlans;

INSERT INTO Categories VALUES('dog');
INSERT INTO Categories VALUES('cat');
INSERT INTO Categories VALUES('rabbit');
INSERT INTO Categories VALUES('snake');
INSERT INTO Categories VALUES('guinea pig');

SELECT * FROM Categories;

INSERT INTO Shots VALUES('rabies');
INSERT INTO Shots VALUES('kennel cough');

SELECT * FROM Shots;

INSERT INTO Employees VALUES('Chris', 'Evans', 'cevans', 'admin', 10000, 'cevans@gmail.com');
INSERT INTO Employees VALUES('Scarlett', 'Johansson', 'sjohansson', 'admin', 10001, 'sjohansson@gmail.com');
INSERT INTO Employees VALUES('Robert', 'Downey Jr.', 'rdowney', 'admin', 10002, 'rdowney@gmail.com');
INSERT INTO Employees VALUES('Mark', 'Ruffalo', 'mruffalo', 'admin', 10003, 'mruffalo@gmail.com');
INSERT INTO Employees VALUES('Jeremy', 'Renner', 'jrenner', 'admin', 10004, 'jrenner@gmail.com');

SELECT * FROM Employees;

INSERT INTO Animals VALUES('Banjo', 60, 2, 'goofball', 1, 1, 'm', 'adopted', 1, 2, 1);
INSERT INTO Animals VALUES('Glenn', 10, 1, 'busy bee', 1, 1, 'm', 'available', 2, 5, 3);
INSERT INTO Animals VALUES('Crackers', 24, 3, 'wallflower', 0, 1, 'm', 'adopted', 2, 4, 2);
INSERT INTO Animals VALUES('Daisy', 15, 1, 'life of the party', 0, 1, 'f', 'available', 1, 3, 5);
INSERT INTO Animals VALUES('Bill', 13, 14, 'wallflower', 1, 1, 'm', 'available', 2, 4, 4);
INSERT INTO Animals VALUES('Bob', 12, 15, 'busy bee', 0, 1, 'm', 'available', 2, 5, 1);
INSERT INTO Animals VALUES('Eddie', 6, 21, 'wallflower', 1, 0, 'm', 'available', 2, 5, 2);
INSERT INTO Animals VALUES('Bumby', 4, 9, 'busy bee', 0, 0, 'm', 'available', 3, 6, 2);
INSERT INTO Animals VALUES('Ziggy', 8, 3, 'life of the party', 1, 0, 'm', 'adopted', 1, 3, 3);
INSERT INTO Animals VALUES('Banana', 85, 12, 'goofball', 1, 0, 'f', 'available', 4, 1, 4);

SELECT * FROM Animals;

INSERT INTO Rooms VALUES(1, 2);
INSERT INTO Rooms VALUES(2, 8);
INSERT INTO Rooms VALUES(3, 3);
INSERT INTO Rooms VALUES(4, 9);
INSERT INTO Rooms VALUES(5, 1);
INSERT INTO Rooms VALUES(6, 5);
INSERT INTO Rooms VALUES(7, 6);
INSERT INTO Rooms VALUES(8, 4);
INSERT INTO Rooms VALUES(9, 7);
INSERT INTO Rooms VALUES(10, 10);

SELECT * FROM Rooms;

INSERT INTO Addresses VALUES('123 Main St', 'Pawnee', 14, 46001);
INSERT INTO Addresses VALUES('2957 3rd Ave', 'Greendale', 23, 55110);
INSERT INTO Addresses VALUES('927 Oak St', 'Springfield', 13, 60064);
INSERT INTO Addresses VALUES('1753 N Cedar Ln', 'Los Angeles', 5, 90201);
INSERT INTO Addresses VALUES('23 1st St', 'New York', 32, 10001);

SELECT * FROM Addresses;

INSERT INTO Clients VALUES('Alison', 'Brie', 'abrie', 'admin', 2, 'abrie@gmail.com');
INSERT INTO Clients VALUES('Joel', 'McHale', 'jmchale', 'admin', 1, 'jmchale@gmail.com');
INSERT INTO Clients VALUES('Gillian', 'Jacobs', 'gjacobs', 'admin', 3, 'gjacobs@gmail.com');
INSERT INTO Clients VALUES('Donald', 'Glover', 'dglover', 'admin', 5, 'cgambino@gmail.com');
INSERT INTO Clients VALUES('Danny', 'Pudi', 'dpudi', 'admin', 4, 'dpudi@gmail.com');

SELECT * FROM Clients;

INSERT INTO AnimalShots VALUES(1, 1, '6/1/2018');
INSERT INTO AnimalShots VALUES(1, 2, '6/1/2018');
INSERT INTO AnimalShots VALUES(2, 1, '2/15/2017');
INSERT INTO AnimalShots VALUES(2, 2, '2/15/2017');
INSERT INTO AnimalShots VALUES(3, 1, '8/4/2018');
INSERT INTO AnimalShots VALUES(3, 2, '8/5/2018');
INSERT INTO AnimalShots VALUES(4, 1, '7/14/2019');
INSERT INTO AnimalShots VALUES(4, 2, '7/14/2019');
INSERT INTO AnimalShots VALUES(5, 1, '6/1/2018');
INSERT INTO AnimalShots VALUES(5, 2, '6/1/2018');
INSERT INTO AnimalShots VALUES(6, 1, '1/23/2016');
INSERT INTO AnimalShots VALUES(6, 2, '1/23/2016');
INSERT INTO AnimalShots VALUES(7, 1, '10/1/2018');
INSERT INTO AnimalShots VALUES(7, 2, '10/1/2018');
INSERT INTO AnimalShots VALUES(8, 1, '4/8/2017');
INSERT INTO AnimalShots VALUES(8, 2, '4/8/2017');
INSERT INTO AnimalShots VALUES(9, 1, '11/19/2018');
INSERT INTO AnimalShots VALUES(9, 2, '11/19/2018');
INSERT INTO AnimalShots VALUES(10, 1, '8/19/2018');
INSERT INTO AnimalShots VALUES(10, 2, '8/19/2018');

SELECT * FROM AnimalShots;

INSERT INTO Adoptions VALUES(2, 1, 'approved', 300, 1);
INSERT INTO Adoptions VALUES(1, 3, 'approved', 50, 0);
INSERT INTO Adoptions VALUES(3, 9, 'approved', 350, 1);

SELECT * FROM Adoptions;