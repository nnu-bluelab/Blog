/*
Navicat MySQL Data Transfer

Source Server         : localhost_mysql
Source Server Version : 50715
Source Host           : localhost:3306
Source Database       : blog_core

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2019-04-05 00:49:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for admin
-- ----------------------------
DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `createtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `username` varchar(64) DEFAULT NULL,
  `password` varchar(64) DEFAULT NULL,
  `remark` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for blog
-- ----------------------------
DROP TABLE IF EXISTS `blog`;
CREATE TABLE `blog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `createtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `title` varchar(128) DEFAULT NULL,
  `body` text,
  `body_md` text,
  `visitnum` int(11) NOT NULL DEFAULT '0',
  `cabh` varchar(64) DEFAULT NULL,
  `caname` varchar(64) DEFAULT NULL,
  `remark` varchar(2048) DEFAULT NULL,
  `sort` int(11) NOT NULL,
  `updatetime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=1294 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `createtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `caname` varchar(64) DEFAULT NULL,
  `bh` varchar(64) DEFAULT NULL,
  `pbh` varchar(64) DEFAULT NULL,
  `remark` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
