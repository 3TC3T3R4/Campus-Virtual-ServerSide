CREATE DATABASE CampusVirtual;
GO

-- Seleccionar la base de datos
USE CampusVirtual;
GO

CREATE TABLE LearningPaths (
    pathID UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    coachID VARCHAR(40) NOT NULL,
    title VARCHAR(80) NOT NULL,
    description VARCHAR(150) NOT NULL,
    duration DECIMAL NOT NULL,
    statePath INT NOT NULL,
);

CREATE TABLE Courses (
    courseID UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    pathID UNIQUEIDENTIFIER NOT NULL,
    title VARCHAR(80) NOT NULL,
    description VARCHAR(150) NOT NULL,
    duration DECIMAL NOT NULL,
    stateCourse INT NOT NULL,
    CONSTRAINT FK_Courses_LearningPaths FOREIGN KEY (pathID) 
	REFERENCES LearningPaths (pathID) ON DELETE CASCADE
);

CREATE TABLE Contents (
    contentID UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    courseID UNIQUEIDENTIFIER NOT NULL,
    title VARCHAR(50) NOT NULL,
    description VARCHAR(MAX) NOT NULL,
    deliveryField VARCHAR(500),
    type INT NOT NULL,
    duration DECIMAL NOT NULL,
    stateContent INT NOT NULL,
    CONSTRAINT FK_Contents_Courses FOREIGN KEY (courseID) 
	REFERENCES Courses (courseID) ON DELETE CASCADE
);

CREATE TABLE Deliveries (
    deliveryID INT IDENTITY NOT NULL PRIMARY KEY,
    contentID UNIQUEIDENTIFIER NOT NULL,
    uidUser VARCHAR(40) NOT NULL,
    deliveryAt DATETIME NOT NULL,
    rating DECIMAL,
    comment VARCHAR(100),
    ratedAt DATETIME,
    stateDelivery INT NOT NULL,
    CONSTRAINT FK_Deliveries_Contents FOREIGN KEY (contentID) 
	REFERENCES Contents (contentID) ON DELETE CASCADE
);

CREATE TABLE Registrations (
    registrationID INT IDENTITY NOT NULL PRIMARY KEY,
    uidUser VARCHAR(40) NOT NULL,
    pathID UNIQUEIDENTIFIER NOT NULL,
    createdAt DATETIME NOT NULL,
	finalRating DECIMAL,
    stateRegistration INT NOT NULL,
    CONSTRAINT FK_Registrations_LearningPaths FOREIGN KEY (pathID) 
	REFERENCES LearningPaths (pathID) ON DELETE CASCADE
);

-- Insertar datos de prueba en la tabla Learning paths
INSERT INTO [dbo].[LearningPaths] (pathID, coachID, title, description, duration, statePath) 
VALUES 
    ('3F5AC9AA-1B09-4C39-9584-5AEAD893D302', 'coach1', 'Path 1', 'Description for Path 1', 10, 1),
    ('CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F59', 'coach2', 'Path 2', 'Description for Path 2', 15, 1),
    ('D483BF47-0C91-42F4-BE40-FF7D29ACA763', 'coach3', 'Path 3', 'Description for Path 3', 20, 0);

-- Insertar datos de prueba en la tabla Courses
INSERT INTO [dbo].[Courses] (courseID, pathID, title, description, duration, stateCourse) 
VALUES 
    ('3F5AC9AA-1B09-4C39-9584-5AEAD893D301', '3F5AC9AA-1B09-4C39-9584-5AEAD893D302', 'Course 1', 'Description for Course 1', 5, 1),
    ('CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F52', 'CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F59', 'Course 2', 'Description for Course 2', 5, 1),
    ('D483BF47-0C91-42F4-BE40-FF7D29ACA764', 'D483BF47-0C91-42F4-BE40-FF7D29ACA763', 'Course 3', 'Description for Course 3', 10, 0);

-- Insertar datos de prueba en la tabla Contents
INSERT INTO [dbo].[Contents] (contentID, courseID, title, description, deliveryField, type, duration, stateContent) 
VALUES 
    ('3F5AC9AA-1B09-4C39-9584-5AEAD893D303', '3F5AC9AA-1B09-4C39-9584-5AEAD893D301', 'Content 1', 'Description for Content 1', 'Video URL for Content 1', 1, 2, 1),
    ('CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F54', 'CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F52', 'Content 2', 'Description for Content 2', 'PDF URL for Content 2', 2, 3, 1),
    ('D483BF47-0C91-42F4-BE40-FF7D29ACA765', 'D483BF47-0C91-42F4-BE40-FF7D29ACA764', 'Content 3', 'Description for Content 3', 'Quiz URL for Content 3', 3, 4, 0);

-- Insertar datos de prueba en la tabla Registrations
INSERT INTO [dbo].[Registrations] (uidUser, pathID, createdAt,finalRating, stateRegistration) 
VALUES 
    ('user1', '3F5AC9AA-1B09-4C39-9584-5AEAD893D302', '2023-04-21 09:00:00',NUll, 1),
    ('user2', 'CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F59', '2023-04-22 10:00:00',NULL, 1),
    ('user3', 'D483BF47-0C91-42F4-BE40-FF7D29ACA763', '2023-04-23 11:00:00',NULL, 0);

-- Insertar datos de prueba en la tabla Deliveries
INSERT INTO [dbo].[Deliveries] (contentID, uidUser, deliveryAt, rating, comment, ratedAt, stateDelivery) 
VALUES 
    ('3F5AC9AA-1B09-4C39-9584-5AEAD893D303', 'user1', '2023-04-21 10:00:00', 4.5, 'Good job!', '2023-04-22 14:30:00', 1),
    ('CE79D5C8-BEE5-41E3-8BF4-C79DED2E0F54', 'user2', '2023-04-22 12:00:00', NULL, NULL, NULL, 0),
    ('D483BF47-0C91-42F4-BE40-FF7D29ACA765', 'user3', '2023-04-23 09:00:00', NULL, NULL, NULL, 1);
