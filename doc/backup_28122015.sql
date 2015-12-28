-- --------------------------------------------------------
-- Strežnik:                     127.0.0.1
-- Server version:               5.6.28-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Različica:           9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for estudent
CREATE DATABASE IF NOT EXISTS `estudent` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `estudent`;


-- Dumping structure for tabela estudent.izpitnirok
CREATE TABLE IF NOT EXISTS `izpitnirok` (
  `idIzpitniRok` int(11) NOT NULL AUTO_INCREMENT,
  `idPredmeta` int(11) NOT NULL,
  `stRoka` int(11) NOT NULL,
  `datum` date NOT NULL,
  `prostor` varchar(45) NOT NULL,
  `komentar` varchar(256) NOT NULL,
  `zakljucen` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idIzpitniRok`),
  KEY `fk_IzpitniRok_Predmet1_idx` (`idPredmeta`),
  CONSTRAINT `fk_IzpitniRok_Predmet1` FOREIGN KEY (`idPredmeta`) REFERENCES `predmet` (`idPredmet`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.izpitnirok: ~7 rows (približno)
DELETE FROM `izpitnirok`;
/*!40000 ALTER TABLE `izpitnirok` DISABLE KEYS */;
INSERT INTO `izpitnirok` (`idIzpitniRok`, `idPredmeta`, `stRoka`, `datum`, `prostor`, `komentar`, `zakljucen`) VALUES
	(1, 1, 1, '2015-12-15', 'P22', '8:00', 1),
	(2, 1, 2, '2015-12-15', 'P22', '9:00', 1),
	(4, 3, 1, '2015-12-15', 'P01', '12:00', 1),
	(5, 4, 1, '2015-12-15', 'PA', '12:00', 1),
	(8, 20, 1, '2015-12-15', 'P04', '20:00', 1),
	(9, 6, 1, '2015-12-31', 'P22', '19:00', 1),
	(10, 21, 1, '2015-12-28', 'PA', '12:00', 1),
	(11, 11, 1, '2016-01-28', 'P04', 'prva skupina', 1);
/*!40000 ALTER TABLE `izpitnirok` ENABLE KEYS */;


-- Dumping structure for tabela estudent.ocena
CREATE TABLE IF NOT EXISTS `ocena` (
  `idOcena` int(11) NOT NULL AUTO_INCREMENT,
  `idStudenta` int(11) NOT NULL,
  `idPredmeta` int(11) NOT NULL,
  `idIzpitnegaRoka` int(11) NOT NULL,
  `sTock` int(11) NOT NULL,
  `ocena` int(11) NOT NULL,
  PRIMARY KEY (`idOcena`),
  KEY `fk_Ocena_Uporabnik1_idx` (`idStudenta`),
  KEY `fk_Ocena_Predmet1_idx` (`idPredmeta`),
  KEY `fk_Ocena_IzpitniRok1_idx` (`idIzpitnegaRoka`),
  CONSTRAINT `fk_Ocena_IzpitniRok1` FOREIGN KEY (`idIzpitnegaRoka`) REFERENCES `izpitnirok` (`idIzpitniRok`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ocena_Predmet1` FOREIGN KEY (`idPredmeta`) REFERENCES `predmet` (`idPredmet`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ocena_Uporabnik1` FOREIGN KEY (`idStudenta`) REFERENCES `uporabnik` (`idUporabnik`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.ocena: ~22 rows (približno)
DELETE FROM `ocena`;
/*!40000 ALTER TABLE `ocena` DISABLE KEYS */;
INSERT INTO `ocena` (`idOcena`, `idStudenta`, `idPredmeta`, `idIzpitnegaRoka`, `sTock`, `ocena`) VALUES
	(1, 1, 1, 1, 96, 10),
	(2, 1, 3, 4, 85, 9),
	(3, 1, 4, 5, 40, 5),
	(4, 2, 4, 5, 30, 5),
	(5, 3, 4, 5, 50, 6),
	(9, 1, 20, 8, 92, 10),
	(10, 2, 20, 8, 96, 10),
	(11, 3, 20, 8, 20, 3),
	(12, 2, 3, 4, 63, 7),
	(13, 3, 3, 4, 81, 9),
	(14, 2, 1, 1, 15, 2),
	(15, 3, 1, 1, 20, 3),
	(16, 1, 1, 2, 0, 0),
	(17, 2, 1, 2, 20, 0),
	(18, 3, 1, 2, 0, 0),
	(19, 1, 6, 9, 99, 10),
	(20, 2, 6, 9, 79, 8),
	(21, 3, 6, 9, 22, 4),
	(22, 1, 21, 10, 54, 6),
	(23, 2, 21, 10, 100, 10),
	(24, 3, 21, 10, 82, 9),
	(25, 1, 11, 11, 82, 9),
	(26, 2, 11, 11, 74, 8),
	(27, 3, 11, 11, 63, 7);
/*!40000 ALTER TABLE `ocena` ENABLE KEYS */;


-- Dumping structure for tabela estudent.predmet
CREATE TABLE IF NOT EXISTS `predmet` (
  `idPredmet` int(11) NOT NULL AUTO_INCREMENT,
  `imePredmeta` varchar(45) NOT NULL,
  `idIzvajalca` int(11) NOT NULL,
  `stKreditnih` int(11) NOT NULL,
  PRIMARY KEY (`idPredmet`),
  KEY `fk_Predmet_Uporabnik_idx` (`idIzvajalca`),
  CONSTRAINT `idIzvajalca` FOREIGN KEY (`idIzvajalca`) REFERENCES `uporabnik` (`idUporabnik`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.predmet: ~10 rows (približno)
DELETE FROM `predmet`;
/*!40000 ALTER TABLE `predmet` DISABLE KEYS */;
INSERT INTO `predmet` (`idPredmet`, `imePredmeta`, `idIzvajalca`, `stKreditnih`) VALUES
	(1, 'Diskretne strukture', 4, 6),
	(2, 'Diskretne strukture 2', 4, 6),
	(3, 'Osnove matematične analize', 5, 6),
	(4, 'Matematično modeliranje', 5, 6),
	(5, 'Matematika MG', 5, 6),
	(6, 'Spletno programiranje', 5, 6),
	(11, 'PRPO', 7, 6),
	(12, 'Elektronsko poslovanje', 5, 6),
	(19, 'Poslovna inteligenca', 7, 6),
	(20, 'Osnove umetne inteligence', 7, 6),
	(21, 'Ruščina', 5, 6),
	(22, 'Logistika in transport', 4, 6);
/*!40000 ALTER TABLE `predmet` ENABLE KEYS */;


-- Dumping structure for tabela estudent.studentpredmet
CREATE TABLE IF NOT EXISTS `studentpredmet` (
  `idStudentPredmet` int(11) NOT NULL AUTO_INCREMENT,
  `idStudenta` int(11) NOT NULL,
  `idPredmeta` int(11) NOT NULL,
  `opravljen` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idStudentPredmet`),
  KEY `fk_StudentPredmet_Uporabnik1_idx` (`idStudenta`),
  KEY `fk_StudentPredmet_Predmet1_idx` (`idPredmeta`),
  CONSTRAINT `fk_StudentPredmet_Predmet1` FOREIGN KEY (`idPredmeta`) REFERENCES `predmet` (`idPredmet`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_StudentPredmet_Uporabnik1` FOREIGN KEY (`idStudenta`) REFERENCES `uporabnik` (`idUporabnik`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.studentpredmet: ~0 rows (približno)
DELETE FROM `studentpredmet`;
/*!40000 ALTER TABLE `studentpredmet` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentpredmet` ENABLE KEYS */;


-- Dumping structure for tabela estudent.uporabnik
CREATE TABLE IF NOT EXISTS `uporabnik` (
  `idUporabnik` int(11) NOT NULL AUTO_INCREMENT,
  `vpisnaStevilka` int(11) DEFAULT NULL,
  `ime` varchar(45) NOT NULL,
  `priimek` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `geslo` varchar(45) NOT NULL,
  `mobi` varchar(45) NOT NULL,
  `spol` char(1) NOT NULL,
  `letnikStudija` int(11) DEFAULT NULL,
  `datumRegistracije` datetime NOT NULL,
  `zadnjiDostop` datetime NOT NULL,
  `idVloge` int(11) NOT NULL,
  PRIMARY KEY (`idUporabnik`),
  UNIQUE KEY `idUporabnik_UNIQUE` (`idUporabnik`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `vpisnaStevilka_UNIQUE` (`vpisnaStevilka`),
  KEY `fk_Uporabnik_Vloga1_idx` (`idVloge`),
  CONSTRAINT `fk_Uporabnik_Vloga1` FOREIGN KEY (`idVloge`) REFERENCES `vloga` (`idVloga`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.uporabnik: ~7 rows (približno)
DELETE FROM `uporabnik`;
/*!40000 ALTER TABLE `uporabnik` DISABLE KEYS */;
INSERT INTO `uporabnik` (`idUporabnik`, `vpisnaStevilka`, `ime`, `priimek`, `email`, `geslo`, `mobi`, `spol`, `letnikStudija`, `datumRegistracije`, `zadnjiDostop`, `idVloge`) VALUES
	(1, 63130164, 'Matic', 'Novak', 'student@gmail.com', 'geslo', '041 721 924', 'M', 3, '2015-12-15 11:27:29', '2015-12-15 11:27:33', 1),
	(2, 63130098, 'Tina', 'Klancar', 'klancar.tina@gmail.com', 'geslo2', '070 000 000', 'Z', 2, '2015-12-15 15:55:39', '2015-12-15 15:55:39', 1),
	(3, 63130167, 'Rok', 'Novosel', 'rok@novosel.metlika', 'gesloNesto5', '07 30 88 958', 'M', 3, '2015-12-15 15:56:37', '2015-12-15 15:56:38', 1),
	(4, NULL, 'Gašper', 'Fijavž', 'profesor@gmail.com', 'nestoGeslo', '01 45 67 415', 'M', NULL, '2015-12-15 15:57:21', '2015-12-15 15:57:27', 2),
	(5, NULL, 'Polona', 'Oblak', 'polona.oblak@fri.uni-lj.si', 'soncek', '01 58 74 614', 'Z', NULL, '2015-12-15 16:08:14', '2015-12-15 16:08:14', 2),
	(6, NULL, 'Studentski', 'referat', 'referat@gmail.com', 'referat5', '041 987 654', 'Z', NULL, '2015-12-15 16:09:05', '2015-12-15 16:09:05', 3),
	(7, NULL, 'Blaž', 'Zupan', 'blaz.zupan@fri.uni-lj.si', 'gesloNeko', '01 77 65 198', 'M', NULL, '2015-12-22 11:36:03', '2015-12-22 11:36:04', 2);
/*!40000 ALTER TABLE `uporabnik` ENABLE KEYS */;


-- Dumping structure for tabela estudent.vloga
CREATE TABLE IF NOT EXISTS `vloga` (
  `idVloga` int(11) NOT NULL AUTO_INCREMENT,
  `nazivVloge` varchar(45) NOT NULL,
  PRIMARY KEY (`idVloga`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table estudent.vloga: ~2 rows (približno)
DELETE FROM `vloga`;
/*!40000 ALTER TABLE `vloga` DISABLE KEYS */;
INSERT INTO `vloga` (`idVloga`, `nazivVloge`) VALUES
	(1, 'student'),
	(2, 'profesor'),
	(3, 'referat');
/*!40000 ALTER TABLE `vloga` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
