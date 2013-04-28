SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `cybernetdb` ;
CREATE SCHEMA IF NOT EXISTS `cybernetdb` DEFAULT CHARACTER SET utf8 ;
USE `cybernetdb` ;

-- -----------------------------------------------------
-- Table `cybernetdb`.`Actions`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`Actions` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`Actions` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `Type` INT NOT NULL DEFAULT 0 ,
  `EnergyIndex` INT NOT NULL DEFAULT 0 ,
  `DangerIndex` INT NOT NULL DEFAULT 0 ,
  `ForceIndex` INT NOT NULL DEFAULT 0 ,
  `IntelligenceIndex` INT NOT NULL DEFAULT 0 ,
  PRIMARY KEY (`ID`) ,
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`agents`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`agents` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`agents` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `Login` VARCHAR(45) NOT NULL ,
  `Password` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`ID`) ,
  UNIQUE INDEX `Login_UNIQUE` (`Login` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`categories`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`categories` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`categories` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `Type` VARCHAR(45) NOT NULL ,
  `Measure` VARCHAR(10) NULL ,
  `EnergyIndex` INT NULL DEFAULT 0 ,
  `CollectIndex` INT NULL DEFAULT 0 ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`stock`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`stock` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`stock` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT ,
  `Count` DOUBLE NULL DEFAULT '0' ,
  `Quality` DOUBLE NULL DEFAULT '0' ,
  `AgentID` INT(11) NOT NULL ,
  `CategoryID` INT(11) NOT NULL ,
  PRIMARY KEY (`ID`) ,
  INDEX `fk_stock_agents_idx` (`AgentID` ASC) ,
  INDEX `fk_stock_categories1_idx` (`CategoryID` ASC) ,
  CONSTRAINT `fk_stock_agents`
    FOREIGN KEY (`AgentID` )
    REFERENCES `cybernetdb`.`agents` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_stock_categories1`
    FOREIGN KEY (`CategoryID` )
    REFERENCES `cybernetdb`.`categories` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`test`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`test` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`test` (
  `ID` INT(11) NOT NULL ,
  `Name` VARCHAR(45) NULL DEFAULT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`world`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`world` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`world` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT ,
  `City` VARCHAR(100) NOT NULL ,
  PRIMARY KEY (`ID`) ,
  UNIQUE INDEX `City_UNIQUE` (`City` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cybernetdb`.`CategoriesPrevalence`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`CategoriesPrevalence` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`CategoriesPrevalence` (
  `ID` INT NOT NULL AUTO_INCREMENT ,
  `Prevalence` DOUBLE NOT NULL DEFAULT 0 ,
  `PrevalenceFluctuation` DOUBLE NOT NULL DEFAULT 0 ,
  `QualityNorm` DOUBLE NOT NULL DEFAULT 0 ,
  `QualityFluctuation` DOUBLE NOT NULL DEFAULT 0 ,
  `CategoryID` INT(11) NOT NULL ,
  `CityID` INT(11) NOT NULL ,
  PRIMARY KEY (`ID`) ,
  INDEX `fk_CategoriesPrevalence_categories1_idx` (`CategoryID` ASC) ,
  INDEX `fk_CategoriesPrevalence_world1_idx` (`CityID` ASC) ,
  CONSTRAINT `fk_CategoriesPrevalence_categories1`
    FOREIGN KEY (`CategoryID` )
    REFERENCES `cybernetdb`.`categories` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CategoriesPrevalence_world1`
    FOREIGN KEY (`CityID` )
    REFERENCES `cybernetdb`.`world` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cybernetdb`.`OptionsConditionsActionExe`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`OptionsConditionsActionExe` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`OptionsConditionsActionExe` (
  `OptionID` INT NOT NULL ,
  `ConditionID` INT NOT NULL ,
  `ActionsID` INT(11) NOT NULL ,
  `CategoryID` INT(11) NOT NULL ,
  INDEX `fk_OptionsConditionsActionExe_categories1_idx` (`CategoryID` ASC) ,
  CONSTRAINT `fk_OptionsConditionsActionExe_Actions1`
    FOREIGN KEY (`ActionsID` )
    REFERENCES `cybernetdb`.`Actions` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_OptionsConditionsActionExe_categories1`
    FOREIGN KEY (`CategoryID` )
    REFERENCES `cybernetdb`.`categories` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cybernetdb`.`OptionsReceivingProduct`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cybernetdb`.`OptionsReceivingProduct` ;

CREATE  TABLE IF NOT EXISTS `cybernetdb`.`OptionsReceivingProduct` (
  `OptionsID` INT NOT NULL ,
  `FromID` INT NOT NULL DEFAULT 0 ,
  `FromOptionsID` INT NOT NULL DEFAULT 0 ,
  `ToID` INT NOT NULL ,
  `ActionID` INT NOT NULL ,
  INDEX `fk_OptionsReceivingProduct_Actions1_idx` (`ActionID` ASC) ,
  CONSTRAINT `fk_OptionsReceivingProduct_Actions1`
    FOREIGN KEY (`ActionID` )
    REFERENCES `cybernetdb`.`Actions` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `cybernetdb` ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
