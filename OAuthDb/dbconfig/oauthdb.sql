DROP SCHEMA `oathdb`;
-- -----------------------------------------------------
-- Schema oathdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `oathdb` DEFAULT CHARACTER SET utf8 ;
-- -----------------------------------------------------
-- Schema new_schema1
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Table `oathdb`.`action`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `oathdb`.`action` ;

CREATE TABLE IF NOT EXISTS `oathdb`.`action` (
  `atn_action` VARCHAR(1) NOT NULL,
  `atn_name` VARCHAR(45) NULL,
  `atn_descr` VARCHAR(128) NULL,
  PRIMARY KEY (`atn_action`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `oathdb`.`role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `oathdb`.`role` ;

CREATE TABLE IF NOT EXISTS `oathdb`.`role` (
  `rle_role` VARCHAR(3) NOT NULL,
  `rle_name` VARCHAR(45) NOT NULL,
  `rle_descr` VARCHAR(128) NULL,
  PRIMARY KEY (`rle_role`),
  UNIQUE INDEX `rle_name_UNIQUE` (`rle_name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `oathdb`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `oathdb`.`user` ;

CREATE TABLE IF NOT EXISTS `oathdb`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `usr_name` VARCHAR(128) NULL,
  `usr_pwd` VARCHAR(256) NULL,
  `usr_email` VARCHAR(256) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `oathdb`.`roleaction`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `oathdb`.`roleaction` ;

CREATE TABLE IF NOT EXISTS `oathdb`.`roleaction` (
  `fkatn_action` VARCHAR(1) NOT NULL,
  `fkrle_role` VARCHAR(3) NOT NULL,
  INDEX `fk_ran_atn_idx` (`fkatn_action` ASC) VISIBLE,
  INDEX `fk_ran_rle_idx` (`fkrle_role` ASC) VISIBLE,
  CONSTRAINT `fk_ran_atn`
    FOREIGN KEY (`fkatn_action`)
    REFERENCES `oathdb`.`action` (`atn_action`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_ran_rle`
    FOREIGN KEY (`fkrle_role`)
    REFERENCES `oathdb`.`role` (`rle_role`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `new_schema1` ;

-- -----------------------------------------------------
-- Table `new_schema1`.`userrole`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `oathdb`.`userrole` ;

CREATE TABLE IF NOT EXISTS `oathdb`.`userrole` (
  `fkusr_id` INT NOT NULL,
  `fkrle_role` VARCHAR(3) NOT NULL,
  INDEX `fk_userrole_user_idx` (`fkusr_id` ASC) VISIBLE,
  INDEX `fk_userrole_role1_idx` (`fkrle_role` ASC) VISIBLE,
  CONSTRAINT `fk_userrole_user`
    FOREIGN KEY (`fkusr_id`)
    REFERENCES `oathdb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_userrole_role1`
    FOREIGN KEY (`fkrle_role`)
    REFERENCES `oathdb`.`role` (`rle_role`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

--
GRANT SELECT, INSERT ON oathdb.* TO 'info'@'localhost';