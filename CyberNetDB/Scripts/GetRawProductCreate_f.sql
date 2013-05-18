delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `GetRawProductCreate`(argCategoryID int, argOptionsID int, argNumber int) RETURNS int(11)
BEGIN
	DECLARE locRawID int;

	set locRawID = (select ac2.CategoryID
						from
						(select ac.CategoryID, @s:=@s+1 as i 
							from
								(select a.OptionsID, a.FromID as CategoryID
								from OptionsReceivingProduct as a
								where a.ToID = argCategoryID and a.FromID <> 0  and
										a.OptionsID = argOptionsID
								union
								select a.OptionsID, e.CategoryID
								from OptionsReceivingProduct as a
										join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
								where a.ToID = argCategoryID and
										a.OptionsID = argOptionsID) as ac,
								(SELECT @s:= 0) AS s) as ac2
							where ac2.i = argNumber) ;
	

RETURN locRawID;
END$$

