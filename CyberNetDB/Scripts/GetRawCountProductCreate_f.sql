delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `GetRawCountProductCreate`(argCategoryID int, argOptionsID int) RETURNS int(11)
BEGIN
	DECLARE locRawCount int;


	set locRawCount = (select COUNT(*)
							from
								(select a.OptionsID, a.ActionID, a.ID, 0 as eID
								from OptionsReceivingProduct as a
								where a.ToID = argCategoryID and a.FromID <> 0  and
										a.OptionsID = argOptionsID
								union
								select a.OptionsID, a.ActionID, a.ID, e.ID as eID
								from OptionsReceivingProduct as a
										join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
								where a.ToID = argCategoryID and
										a.OptionsID = argOptionsID) as ac);


RETURN locRawCount;
END$$

