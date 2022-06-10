-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               8.0.29 - MySQL Community Server - GPL
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных cssoftr
CREATE DATABASE IF NOT EXISTS `cssoftr` /*!40100 DEFAULT CHARACTER SET ascii */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `cssoftr`;

-- Дамп структуры для таблица cssoftr.employee
CREATE TABLE IF NOT EXISTS `employee` (
  `user_id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL DEFAULT '',
  `dob` date DEFAULT NULL,
  `gender` enum('M','W') DEFAULT NULL,
  `position` varchar(20) NOT NULL DEFAULT '',
  `position_info` varchar(100) NOT NULL DEFAULT '',
  `other_info` varchar(200) NOT NULL DEFAULT '',
  PRIMARY KEY (`user_id`),
  CONSTRAINT `id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=ascii;

-- Дамп данных таблицы cssoftr.employee: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` (`user_id`, `name`, `dob`, `gender`, `position`, `position_info`, `other_info`) VALUES
	(2, 'nick', '2022-06-06', 'M', 'higher being', 'observs people', 'random things'),
	(3, 'al', '2022-06-06', 'M', 'employee', 'does nothing', 'sdflnjdfg');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;

-- Дамп структуры для таблица cssoftr.user
CREATE TABLE IF NOT EXISTS `user` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `login` varchar(16) DEFAULT NULL,
  `salt` varchar(8) DEFAULT NULL,
  `password` varchar(256) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `privilege` tinyint unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=ascii;

-- Дамп данных таблицы cssoftr.user: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `login`, `salt`, `password`, `privilege`) VALUES
	(1, 'admin', 'aaaaaaaa', '9e174834d621b5435ebe55054a87939df4121874bb47927bb195e4675dd5cdbc', 100),
	(2, 'user', '93284cce', 'cd08e17f0c709417ccdbbc2bec219c7b8f44c3422c939131e7fb58bba5246e68', 0),
	(3, 'user1', 'fd4b81f3', '0ed994e00ac7810cd4ceaba370b09d1d86333237ef1ddc157f3c267cc13a4bff', 0),
	(4, 'werylongnik', '9b615aa0', '367cec38436707703dc18ec1c5a15c04161ab6a05fda65bd6f47e1563e2bdafc', 0);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
