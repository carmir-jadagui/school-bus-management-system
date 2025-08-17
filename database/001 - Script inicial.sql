-- Script inicial para crear la BD y las tablas
-- Crear la base de datos si no existe
CREATE DATABASE IF NOT EXISTS sbms;

-- Usar la base de datos
USE sbms;

-- Crea una tabla con registro de prueba, para validar la conexión entre las diferentes capastest
CREATE TABLE IF NOT EXISTS `sbms`.`test` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Message` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`)
);

INSERT INTO `sbms`.`test` (`Message`) VALUES ('Hola mundo desde la BD');