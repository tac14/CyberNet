delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `GetCurrentTime`() RETURNS datetime
BEGIN
	DECLARE locDate datetime;	
	set locDate = (select CurrentDate
						from world
						where ID = 1);
	RETURN locDate;
END$$

