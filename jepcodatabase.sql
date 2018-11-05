-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 09, 2018 at 09:49 PM
-- Server version: 10.1.26-MariaDB
-- PHP Version: 7.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `jepcodatabase`
--

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `ID` int(11) NOT NULL,
  `Category` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`ID`, `Category`) VALUES
(8, 'Belts'),
(11, 'Cables'),
(9, 'Fans'),
(6, 'Gas'),
(15, 'new'),
(5, 'Nuts'),
(12, 'Tubes'),
(3, 'Tunel'),
(10, 'Washers'),
(4, 'Wire'),
(16, 'xxxxxxxxxxxxxxxxxxxxxxxx');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ID` int(4) NOT NULL,
  `Item_Code` varchar(50) NOT NULL,
  `Item_Name` varchar(50) NOT NULL,
  `Qty` int(50) NOT NULL,
  `Price` int(50) NOT NULL,
  `Category` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ID`, `Item_Code`, `Item_Name`, `Qty`, `Price`, `Category`) VALUES
(4, 'AC1205', 'Copper Cable 1/4', 69, 300, 'Cables'),
(5, 'AC1206', 'Copper Cable 3/8', -187, 260, 'Cables'),
(6, 'AC1207', 'Copper Cable 1/2', -251, 800, 'Cables'),
(7, 'AC1301', 'Arma Flex', 20, 1500, 'Tunel'),
(8, 'AC1302', 'Cool Box', -5, 5200, 'Tunel'),
(9, 'AC1002', 'Aluminium Tube', 2, 450, 'Tubes'),
(10, 'AC1003', 'Metel Tube', 54, 225, 'Tubes'),
(11, 'AC1503', 'Bend 1/2', 7, 130, 'PITINES'),
(12, 'AC1504', 'Copper Joints 3/4', 15, 250, 'PITINES'),
(13, 'AC1800', 'Belts 15mm', 3, 610, 'Belts'),
(14, 'AC1801', 'Belts 25mm', 39, 800, 'Belts'),
(15, 'AC1689', 'Z12 Fan 5 Wings', -7, 3000, 'Fans'),
(17, 'AC1688', 'Z12 Fan 4 Wings', 6, 2800, 'Fans'),
(18, 'AC1755', 'Copper Nut 12mm', 166, 25, 'Nuts');

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `ReEnterPassword` varchar(50) NOT NULL,
  `SecurityQuestion` varchar(100) NOT NULL,
  `Answer` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`ID`, `FirstName`, `LastName`, `UserName`, `Password`, `ReEnterPassword`, `SecurityQuestion`, `Answer`) VALUES
(2, 'admin', 'admin', 'admin', '1234', '1234', 'What is Your Favorite Color?', 'red'),
(3, 'a', 'a', 'a', 'a', 'a', 'What is Your Favorite Color?', 'a'),
(4, 'asd', 'asd', 'aa', 'aa', 'aa', 'What is Your First School ?', 'aa');

-- --------------------------------------------------------

--
-- Table structure for table `total`
--

CREATE TABLE `total` (
  `ID` int(11) NOT NULL,
  `Invoice` varchar(10) NOT NULL,
  `Number` varchar(10) NOT NULL,
  `date` varchar(20) NOT NULL,
  `Total` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `total`
--

INSERT INTO `total` (`ID`, `Invoice`, `Number`, `date`, `Total`) VALUES
(25, '#00001', 'GC-9641', '10/10/2018', 'Rs: 26500.00');

-- --------------------------------------------------------

--
-- Table structure for table `total2`
--

CREATE TABLE `total2` (
  `ID` int(11) NOT NULL,
  `Invoice` varchar(10) NOT NULL,
  `Number` varchar(10) NOT NULL,
  `date` varchar(20) NOT NULL,
  `Total` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `total2`
--

INSERT INTO `total2` (`ID`, `Invoice`, `Number`, `date`, `Total`) VALUES
(25, '#00001', 'GC-9641', '10/10/2018', 26500);

-- --------------------------------------------------------

--
-- Table structure for table `vehicle`
--

CREATE TABLE `vehicle` (
  `ID` int(5) NOT NULL,
  `Invoice` varchar(10) NOT NULL,
  `Number` varchar(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Catagory` varchar(50) NOT NULL,
  `Unit_Price` int(20) NOT NULL,
  `Qty` int(10) NOT NULL,
  `Price` varchar(10) NOT NULL,
  `date` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `vehicle`
--

INSERT INTO `vehicle` (`ID`, `Invoice`, `Number`, `Name`, `Catagory`, `Unit_Price`, `Qty`, `Price`, `date`) VALUES
(82, '#00001', 'GC-9641', 'Service Charge', '', 0, 0, '500', '10/10/2018'),
(83, '#00001', 'GC-9641', 'Cool Box', 'Tunel', 5200, 5, '26000', '10/10/2018');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Category` (`Category`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Item_Code` (`Item_Code`),
  ADD UNIQUE KEY `Item_Name` (`Item_Name`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `total`
--
ALTER TABLE `total`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `total2`
--
ALTER TABLE `total2`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `vehicle`
--
ALTER TABLE `vehicle`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `ID` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `login`
--
ALTER TABLE `login`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `total`
--
ALTER TABLE `total`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `total2`
--
ALTER TABLE `total2`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `vehicle`
--
ALTER TABLE `vehicle`
  MODIFY `ID` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=84;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
