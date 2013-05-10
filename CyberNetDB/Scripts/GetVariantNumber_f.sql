delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `GetVariantNumber`(argProductID int, argOptionsID int) RETURNS int(11)
BEGIN
	DECLARE VariantNumber int;
	set VariantNumber = -1;
	set VariantNumber = (select a.i
						from 
							(select OptionsID, @s:=@s+1 as i 
							from optionsreceivingproduct,
									(SELECT @s:= 0) AS s
							where ToId = argProductID 
							group by OptionsID) as a
						where OptionsID = argOptionsID);

RETURN VariantNumber;
END$$

