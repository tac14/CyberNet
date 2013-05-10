delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetVariantNumber`(argProductID int, argOptionsID int)
BEGIN

	select a.i, a.OptionsID
	from 
		(select OptionsID, @s:=@s+1 as i 
		from optionsreceivingproduct,
				(SELECT @s:= 0) AS s
		where ToId = argProductID 
		group by OptionsID) as a
	where OptionsID = IF(argOptionsID = 0, OptionsID, argOptionsID);

END$$

