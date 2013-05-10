delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllProduct`()
BEGIN
	select a.Name, a.ID
	from Categories as a
	where Type = 'Product';
END$$

