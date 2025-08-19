-- Script para crear las tablas maestro
-- Tabla chicos
CREATE TABLE IF NOT EXISTS `sbms`.`boys` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Dni` INT NOT NULL,
  `FirstName` VARCHAR(45) NOT NULL,
  `LastName` VARCHAR(45) NOT NULL,
  `Gender` CHAR(1) NOT NULL,
  `Age` INT NOT NULL,
  `CreatedAt` DATETIME NOT NULL,
  `UpdatedAt` DATETIME NOT NULL,
  PRIMARY KEY (`Id`, `Dni`),
  UNIQUE INDEX `Dni_UNIQUE` (`Dni` ASC) VISIBLE
  );

  -- Tabla de micros
  CREATE TABLE IF NOT EXISTS `sbms`.`buses` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Plate` VARCHAR(7) NOT NULL,
  `CreatedAt` DATETIME NOT NULL,
  `UpdatedAt` DATETIME NOT NULL,
  PRIMARY KEY (`Id`, `Plate`),
  UNIQUE INDEX `Patente_UNIQUE` (`Plate` ASC) VISIBLE
  );

  -- Tabla de choferes
  CREATE TABLE IF NOT EXISTS `sbms`.`drivers` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Dni` INT NOT NULL,
  `FirstName` VARCHAR(45) NOT NULL,
  `LastName` VARCHAR(45) NOT NULL,
  `Telephone` INT NULL,
  `CreatedAt` DATETIME NOT NULL,
  `UpdatedAt` DATETIME NOT NULL,
  PRIMARY KEY (`Id`, `Dni`),
  UNIQUE INDEX `Dni_UNIQUE` (`Dni` ASC) VISIBLE
  );