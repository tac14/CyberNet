delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllProduct`()
BEGIN
	select a.Name
	from Categories as a
	where Type = 'Product';
END$$

