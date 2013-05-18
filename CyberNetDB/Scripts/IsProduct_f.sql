delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `IsProduct`(argCategoryID int) RETURNS int(11)
BEGIN
	DECLARE IsProduct int;
	
	set IsProduct = 0;
	IF exists(select * from Categories as a where a.ID = argCategoryID and a.Type = 'Product') THEN
		set IsProduct = 1;
	END IF;

RETURN IsProduct;
END$$

