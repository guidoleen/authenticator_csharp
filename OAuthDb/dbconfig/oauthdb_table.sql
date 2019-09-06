DROP SCHEMA `oathdb`;
CREATE SCHEMA IF NOT EXISTS `oathdb` DEFAULT CHARACTER SET utf8 ;
USE `oathdb`;

CREATE TABLE IF NOT EXISTS `oathdb`.`action` (
  `atn_action` VARCHAR(1) NOT NULL,
  `atn_name` VARCHAR(45) NULL,
  `atn_descr` VARCHAR(128) NULL,
  PRIMARY KEY (`atn_action`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `oathdb`.`role` (
  `rle_role` VARCHAR(3) NOT NULL,
  `rle_name` VARCHAR(45) NOT NULL,
  `rle_descr` VARCHAR(128) NULL,
  PRIMARY KEY (`rle_role`),
  UNIQUE INDEX `rle_name_UNIQUE` (`rle_name` ASC) )
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `oathdb`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `usr_name` VARCHAR(128) NOT NULL,
  `usr_pwd` VARCHAR(256) NOT NULL,
  `usr_email` VARCHAR(256) unique,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `oathdb`.`roleaction` (
  `fkatn_action` VARCHAR(1) NOT NULL,
  `fkrle_role` VARCHAR(3) NOT NULL,
  INDEX `fk_ran_atn_idx` (`fkatn_action` ASC),
  INDEX `fk_ran_rle_idx` (`fkrle_role` ASC),
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

CREATE TABLE IF NOT EXISTS `oathdb`.`userrole` (
  `fkusr_id` INT NOT NULL,
  `fkrle_role` VARCHAR(3) NOT NULL,
  INDEX `fk_userrole_user_idx` (`fkusr_id` ASC),
  INDEX `fk_userrole_role1_idx` (`fkrle_role` ASC),
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

CREATE TABLE IF NOT EXISTS `oathdb`.`token` (
  `tkn_token` VARCHAR(128) NOT NULL,
  `usr_id` INT NOT NULL,
  `tkn_revoked` INT NOT NULL,
  `tkn_date` DATE NOT NULL,
  PRIMARY KEY (`tkn_token`, `usr_id`),
  INDEX `fk_token_user1_idx` (`usr_id` ASC),
  UNIQUE INDEX `usr_id_UNIQUE` (`usr_id` ASC),
  CONSTRAINT `fk_tkn_usr`
    FOREIGN KEY (`usr_id`)
    REFERENCES `oathdb`.`user` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

GRANT SELECT, INSERT ON oathdb.* TO 'info'@'localhost';