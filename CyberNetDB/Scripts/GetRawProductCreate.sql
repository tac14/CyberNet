delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetRawProductCreate`(
				argCategoryID int, argOptionsID int, argNumber int, OUT RawID int, OUT ActionID int)
BEGIN

	select ac2.CategoryID, ac2.ActionID
	into RawID, ActionID
	from
			(select ac.CategoryID, ac.ActionID, @s:=@s+1 as i 
				from
					(select a.ActionID, a.OptionsID, a.FromID as CategoryID
					from OptionsReceivingProduct as a
					where a.ToID = argCategoryID and a.FromID <> 0  and
							a.OptionsID = argOptionsID
					union
					select a.ActionID, a.OptionsID, e.CategoryID
					from OptionsReceivingProduct as a
							join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
					where a.ToID = argCategoryID and
							a.OptionsID = argOptionsID) as ac,
					(SELECT @s:= 0) AS s) as ac2
	where ac2.i = argNumber ;



END$$

