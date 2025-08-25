-- Script para crear las tablas de asignaciones
  -- Tabla de relación micro-chicos
  CREATE TABLE `sbms`.`buses_boys` (
    `BusId` INT NOT NULL,
    `BoysId` INT NOT NULL,
    PRIMARY KEY (`BusId`, `BoysId`),
    UNIQUE INDEX `BoysId_UNIQUE` (`BoysId` ASC) VISIBLE,
    CONSTRAINT `FK_Buses_Boys`
      FOREIGN KEY (`BusId`)
      REFERENCES `sbms`.`buses` (`Id`),
    CONSTRAINT `FK_Boys_Buses`
      FOREIGN KEY (`BoysId`)
      REFERENCES `sbms`.`boys` (`Id`)
  );

  -- Tabla de relación micro-choferes
  CREATE TABLE `sbms`.`buses_drivers` (
    `BusId` INT NOT NULL,
    `DriversId` INT NOT NULL,
    PRIMARY KEY (`BusId`, `DriversId`),
    UNIQUE INDEX `BusId_UNIQUE` (`BusId` ASC) VISIBLE,
    UNIQUE INDEX `DriversId_UNIQUE` (`DriversId` ASC) VISIBLE,
    CONSTRAINT `FK_Buses_Drivers`
      FOREIGN KEY (`BusId`)
      REFERENCES `sbms`.`buses` (`Id`),
    CONSTRAINT `FK_Drivers_Buses`
      FOREIGN KEY (`DriversId`)
      REFERENCES `sbms`.`drivers` (`Id`)
  );