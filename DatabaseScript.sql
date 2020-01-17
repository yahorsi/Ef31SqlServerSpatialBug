CREATE DATABASE Ef31SqlServerSpatialBug
GO

USE Ef31SqlServerSpatialBug
GO

CREATE TABLE FooTable
(
    [Id] INT PRIMARY KEY,
	[Location] [geometry] NULL
)
GO