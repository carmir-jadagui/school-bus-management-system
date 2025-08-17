-- Script para crear las tablas maestro
-- Tabla chicos
CREATE TABLE IF NOT EXISTS `sbms`.`boys` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Dni` INT NOT NULL,
  `Apellidos` VARCHAR(45) NOT NULL,
  `Nombres` VARCHAR(45) NOT NULL,
  `Sexo` CHAR(1) NOT NULL,
  `Edad` INT NOT NULL,
  PRIMARY KEY (`Id`, `Dni`),
  UNIQUE INDEX `Dni_UNIQUE` (`Dni` ASC) VISIBLE
  );

  -- Tabla de micros
  CREATE TABLE IF NOT EXISTS `sbms`.`buses` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Patente` VARCHAR(7) NOT NULL,
  PRIMARY KEY (`Id`, `Patente`),
  UNIQUE INDEX `Patente_UNIQUE` (`Patente` ASC) VISIBLE
  );

  -- Tabla de choferes
  CREATE TABLE IF NOT EXISTS `sbms`.`drivers` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Dni` INT NOT NULL,
  `Apellidos` VARCHAR(45) NOT NULL,
  `Nombres` VARCHAR(45) NOT NULL,
  `Telefono` INT NULL,
  PRIMARY KEY (`Id`, `Dni`),
  UNIQUE INDEX `Dni_UNIQUE` (`Dni` ASC) VISIBLE
  );